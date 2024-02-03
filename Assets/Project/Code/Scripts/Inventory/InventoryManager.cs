using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Item bagOfLemons;
    [SerializeField] private Item blockOfIce;
    [SerializeField] private Item springMint;

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

        for(int i = 0; i<30; i++)
        {
            AddItem(bagOfLemons);
            AddItem(blockOfIce);
            AddItem(springMint);
        }
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

        ChangeItemSprite(item);
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

        ChangeItemSprite(item);
    }

    public bool DoesKeyExist(Item item)
    {
        return items.ContainsKey(item);
    }

    public void ChangeItemSprite(Item item)
    {
        if (items[item] < item.maxAmount * 0.25)
            item.currentSprite = item.sprite25;
        else if(items[item] < item.maxAmount * 0.5)
            item.currentSprite = item.sprite50;
        else if (items[item] < item.maxAmount * 0.75)
            item.currentSprite = item.sprite75;
        else
            item.currentSprite = item.sprite100;
    }
}
