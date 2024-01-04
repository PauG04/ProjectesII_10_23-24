using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private Dictionary<Item, int> items;

    private Transform itemContent;
    private GameObject inventoryItem;


    private void Awake()
    {
        instance = this;

        items = new Dictionary<Item, int>();
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
    }

    public void ListItems()
    {
        foreach (KeyValuePair<Item, int> item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itermName = obj.transform.Find("Item/itemName").GetComponent<Text>();
        }
    }
}
