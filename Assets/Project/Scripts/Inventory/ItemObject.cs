using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]

public class ItemObject : ScriptableObject
{
    public string itemName;
    public string description;
    public float prize; 
}
