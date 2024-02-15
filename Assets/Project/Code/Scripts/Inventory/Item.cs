using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemNode item;

    public ItemNode GetItem()
    {
        return item;
    }    
}
