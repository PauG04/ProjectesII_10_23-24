using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InsideDecorations : MonoBehaviour
{
    private Dictionary<ItemNode, int> insideDecorations;

    private void Awake()
    {
        insideDecorations = new Dictionary<ItemNode, int>();
    }

    public void AddItem(ItemNode item)
    {
        if(insideDecorations.ContainsKey(item))
        {
            insideDecorations[item]++;
        }
        else
        {
            insideDecorations.Add(item, 1);
        }
    }

    public void SubstractItem(ItemNode item)
    {
        insideDecorations[item]--;
        if (insideDecorations[item] <= 0)
        {
            insideDecorations.Remove(item);
        }
    }

}
