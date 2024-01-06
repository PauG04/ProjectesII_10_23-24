using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private Dictionary<Item, int> items;
    private bool listItems;

    private void Awake()
    {
        instance = this;
        items = new Dictionary<Item, int>();
        listItems = false;
    }

    public Dictionary<Item, int> GetItems()
    {
        return items;
    }

    public void AddItem(Item item)
    {
        if (items.ContainsKey(item))
        {
            items[item]++;
        }
        else
        {
            items.Add(item, 1);
        }

        listItems = true;
    }

    public void UseItem(Item item)
    {
        if (items.ContainsKey(item) && items[item] > 0)
        {
            items[item]--;
            if (items[item] <= 0)
            {
                items.Remove(item);
            }
        }

        listItems = true;
    }

    public bool GetListItems()
    {
        return listItems;
    }

    public void SetListItems(bool value)
    {
        listItems = value;
    }
}
