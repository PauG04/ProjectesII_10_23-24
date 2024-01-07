using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Item bagOfLemons;
    [SerializeField] private Item bagOfIce;
    [SerializeField] private Item springMint;
    public static InventoryManager instance { get; private set; }
    private Dictionary<Item, int> items;
    private bool listItems;

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
        listItems = false;

        for(int i = 0; i<30; i++)
        {
            AddItem(bagOfLemons);
            AddItem(bagOfIce);
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

    public bool DoesKeyExist(Item item)
    {
        return items.ContainsKey(item);
    }
}
