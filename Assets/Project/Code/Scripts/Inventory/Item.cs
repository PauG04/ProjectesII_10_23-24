using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject
{
    public string itemName;
    public float price;
    public float maxAmount;

    public Sprite sprite100;
    public Sprite sprite75;
    public Sprite sprite50;
    public Sprite sprite25;

    public Sprite individualSprite;

    public Sprite currentSprite;

}
