using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class ItemNode : ScriptableObject
{
    public string itemName;
    public string pluralName;
    public float price;
    public float maxAmount;
    public Sprite sprite;
}
