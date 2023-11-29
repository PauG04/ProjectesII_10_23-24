using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// posicionar ItemObject.itemName
    /// debajo del nombre printar "x" + itemObject[item], para saber la cantidad
    /// </summary>

    private Dictionary<ItemObject, int> inventory;
    
    #region POSITIONS IN WINDOWS
    [SerializeField] private Vector2 firstPosition;
    [SerializeField] private Vector2 distanceBetweenItems;
    [SerializeField] private Vector2 maxItems;
    private List<Vector2> posInWindows;
    #endregion
    
    private void Awake()
    {
        inventory = new Dictionary<ItemObject, int>();
        posInWindows = new List<Vector2>();
        SetPositionsInWindows();
        SetItemPositions();
    }

    private void SetPositionsInWindows()
    {
        Vector3 newPosition = firstPosition;

        for (int x = 0; x < maxItems.x; x++)
        {
            for (int y = 0; y < maxItems.y; y++)
            {
                posInWindows.Add(newPosition);
                newPosition.y += distanceBetweenItems.y;
            }
            newPosition.y = firstPosition.y;
            newPosition.x += distanceBetweenItems.x;
        }
    }

    private void SetItemPositions()
    {
        int i = 0;

        foreach (KeyValuePair<ItemObject, int> item in inventory)
        {
            item.Key.transform.position = posInWindows[i];
            i++;
        }
    }

    public void AddItem(ItemObject item)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item]++;
        }
        else
        {
            inventory.Add(item, 1);
        }
    }

    public void UseItem(ItemObject item)
    {
        if (inventory.ContainsKey(item) && inventory[item] > 0)
        {
            inventory[item]--;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
    }
}
