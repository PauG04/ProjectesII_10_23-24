using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindows : MonoBehaviour
{
    [SerializeField] private Vector2 firstPosition;
    [SerializeField] private Vector2 distanceBetweenItems;
    [SerializeField] private Vector2 maxItems;
    private List<Vector2> posInWindows;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        posInWindows = new List<Vector2>();
        SetPositionsInWindows();
        SetItemPositions(inventoryManager.GetInventory());
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

    private void SetItemPositions(Dictionary<ItemObject, int> inventory)
    {
        int i = 0;

        foreach (KeyValuePair<ItemObject, int> item in inventory)
        {
            item.Key.transform.position = posInWindows[i];
            i++;
        }
    }
}
