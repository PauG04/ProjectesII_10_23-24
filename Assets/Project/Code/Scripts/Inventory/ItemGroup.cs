using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGroup : MonoBehaviour
{
    [SerializeField] private ItemGroupNode item;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public ItemGroupNode GetItem()
    {
        return item;
    }

    public void UpdateSprite()
    {
        if (InventoryManager.instance.GetItems()[item] < item.maxAmount * 0.25)
            spriteRenderer.sprite = item.sprite25;
        else if (InventoryManager.instance.GetItems()[item] < item.maxAmount * 0.5)
            spriteRenderer.sprite = item.sprite50;
        else if (InventoryManager.instance.GetItems()[item] < item.maxAmount * 0.75)
            spriteRenderer.sprite = item.sprite75;
        else
            spriteRenderer.sprite = item.sprite100;
    }

    private void Update()
    {
        spriteRenderer.sprite = item.sprite;
    }
}
