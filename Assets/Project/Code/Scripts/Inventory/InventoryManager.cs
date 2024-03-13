using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    private Dictionary<ItemNode, int> items;

    [SerializeField] private ItemGroupNode groupOfLemmons;
    [SerializeField] private ItemGroupNode groupOfIce;
    [SerializeField] private ItemNode iceCube;
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
        
        items = new Dictionary<ItemNode, int>();
        InitItems();
    }

    private void InitItems()
    {
        items.Add(groupOfLemmons, 10);
        items.Add(groupOfIce, 10);
        items.Add(iceCube, 0);
        //items.Add(groupOfMint, 0);
    }

    public Dictionary<ItemNode, int> GetItems()
    {
        return items;
    }

    public void AddItem(ItemNode item)
	{
        if (items[item] < item.maxAmount)
            items[item]++;

    }

    public bool UseItem(ItemNode item)
    {
        if (items[item] > 0)
        {
            items[item]--;
            return true;
        }
        return false;
    }

}
