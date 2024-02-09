using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemGroup", menuName = "ItemGroup")]
public class ItemGroupNode : ItemNode
{
    public Sprite sprite100;
    public Sprite sprite75;
    public Sprite sprite50;
    public Sprite sprite25;


    public void UpdateSprite()
    {
        if (InventoryManager.instance.GetItems()[this] < this.maxAmount * 0.25)
            sprite = sprite25;
        else if (InventoryManager.instance.GetItems()[this] < this.maxAmount * 0.5)
            sprite = sprite50;
        else if (InventoryManager.instance.GetItems()[this] < this.maxAmount * 0.75)
            sprite = sprite75;
        else
            sprite = sprite100;
    }
}
