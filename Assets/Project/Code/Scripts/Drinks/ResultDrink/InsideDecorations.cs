using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InsideDecorations : MonoBehaviour
{
    private Dictionary<ItemNode, int> insideDecorations;

    [SerializeField] private LiquidManager liquidManager;

    private ItemNode _item;
    private bool hasDrug;

    private void Awake()
    {
        insideDecorations = new Dictionary<ItemNode, int>();
    }

    public Dictionary<ItemNode, int> GetDecorations()
    {
        return insideDecorations;
    }

    public void AddItem(ItemNode item)
    {

        if (item.itemName == "Cubo de Hielo")
        {
            
            if (liquidManager.GetCurrentLiquid() > 0)
                AudioManager.instance.PlaySFX("DropIceInLiquid");
            else
                AudioManager.instance.PlaySFX("DropIce");
        }

        if(item.itemName == "Droga")
        {
            hasDrug = true;
        }

        if (insideDecorations.ContainsKey(item))
        {
            insideDecorations[item]++;
            _item = item;
        }
        else if(item.itemName != "Droga")
        {
            insideDecorations.Add(item, 1);
        }
    }

    public void SubstractItem(ItemNode item)
    {
        if (item.itemName == "Droga")
        {
            hasDrug = false;
            Debug.Log("si");
        }
        if (insideDecorations.ContainsKey(item))
        {
            insideDecorations[item]--;
            if (insideDecorations[item] <= 0 && item.itemName != "Droga")
            {
                insideDecorations.Remove(item);
            }
        }
    }

    public int GetIceInside()
    {
        if(_item != null && insideDecorations.ContainsKey(_item))
        {
            return insideDecorations[_item];
        }
        return 0;
    }

    public bool GetHasDrug()
    {
        return hasDrug;
    }
}
