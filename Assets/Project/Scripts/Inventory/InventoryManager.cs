using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    public void AddItem(ItemObject item)
    {
        if (gameManager.GetInventory().ContainsKey(item))
        {
            gameManager.GetInventory()[item]++;
        }
        else
        {
            gameManager.GetInventory().Add(item, 1);
        }
    }

    public void UseItem(ItemObject item)
    {
        if (gameManager.GetInventory().ContainsKey(item) && gameManager.GetInventory()[item] > 0)
        {
            gameManager.GetInventory()[item]--;
            if (gameManager.GetInventory()[item] <= 0)
            {
                gameManager.GetInventory().Remove(item);
            }
        }
    }
}
