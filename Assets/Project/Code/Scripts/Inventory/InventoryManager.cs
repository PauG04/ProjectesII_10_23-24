using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    private Dictionary<ItemGroupNode, int> items;

    [SerializeField] private ItemGroupNode groupOfLemmons;
    [SerializeField] private ItemGroupNode groupOfIce;
    //[SerializeField] private ItemGroupNode groupOfMint;

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
        
        items = new Dictionary<ItemGroupNode, int>();
        InitItems();
    }

    private void InitItems()
    {
        items.Add(groupOfLemmons, 10);
        items.Add(groupOfIce, 10);
        //items.Add(groupOfMint, 0);
    }

    public Dictionary<ItemGroupNode, int> GetItems()
    {
        return items;
    }

    public void AddItem(ItemGroupNode item)
	{
        if (items[item] < item.maxAmount)
            items[item]++;

    }

    public bool UseItem(ItemGroupNode item)
    {
        if (items[item] > 0)
        {
            items[item]--;
            return true;
        }
        return false;
    }

}
