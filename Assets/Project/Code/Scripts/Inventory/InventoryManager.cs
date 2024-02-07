using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    private Dictionary<Item, int> items;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        items = new Dictionary<Item, int>();
    }

    public Dictionary<Item, int> GetItems()
    {
        return items;
    }

    public void AddItem(Item item)
	{
        if (items.ContainsKey(item))
        {
            if (items[item] < item.maxAmount)
                items[item]++;
        }
        else
        {
            items.Add(item, 1);
        }

        ChangeItemSprite((ItemGroup)item);
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

        ChangeItemSprite((ItemGroup)item);
    }

    public bool DoesKeyExist(Item item)
    {
        return items.ContainsKey(item);
    }

    public void ChangeItemSprite(ItemGroup item)
    {
        if (items[item] < item.maxAmount * 0.25)
            item.sprite = item.sprite25;
        else if(items[item] < item.maxAmount * 0.5)
            item.sprite = item.sprite50;
        else if (items[item] < item.maxAmount * 0.75)
            item.sprite = item.sprite75;
        else
            item.sprite = item.sprite100;
    }
}
