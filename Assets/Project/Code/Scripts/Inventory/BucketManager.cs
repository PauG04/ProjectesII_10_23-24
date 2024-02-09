using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketManager : MonoBehaviour
{
    public static BucketManager instance { get; private set; }

    private Dictionary<ItemNode, int> items;

    [SerializeField] private ItemNode lemmonSlice;
    [SerializeField] private ItemNode iceCube;

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
        items.Add(lemmonSlice, 0);
        items.Add(iceCube, 0);
    }

    public Dictionary<ItemNode, int> GetItems()
    {
        return items;
    }

    public void AddItem(ItemNode item)
    {
         items.Add(item, 1);
    }

    public void UseItem(ItemNode item)
    {
        if (items[item] > 0)
        {
            items[item]--;
        }
    }

}
