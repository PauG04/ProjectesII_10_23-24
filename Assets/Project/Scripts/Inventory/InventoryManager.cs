using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<ItemObject, int> inventory;
    
    private void Awake()
    {
        inventory = new Dictionary<ItemObject, int>();
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

    public Dictionary<ItemObject, int> GetInventory()
    {
        return inventory;
    }
}
