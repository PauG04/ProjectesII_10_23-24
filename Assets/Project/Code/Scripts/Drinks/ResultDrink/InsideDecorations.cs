using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InsideDecorations : MonoBehaviour
{
    private Dictionary<ItemNode, int> insideDecorations;

    [SerializeField] private LiquidManager liquidManager;

    private ItemNode _item;

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

        if (insideDecorations.ContainsKey(item))
        {
            insideDecorations[item]++;
            _item = item;
        }
        else
        {
            insideDecorations.Add(item, 1);
        }
    }

    public void SubstractItem(ItemNode item)
    {
        if(insideDecorations.ContainsKey(item))
        {
            insideDecorations[item]--;
            if (insideDecorations[item] <= 0)
            {
                insideDecorations.Remove(item);
            }
        }
    }

    public int GetIceInside()
    {
        if(_item != null)
        {
            return insideDecorations[_item];
        }
        return 0;
    }
}
