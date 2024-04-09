using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButLiquid : MonoBehaviour
{
    [Header("item")]
    [SerializeField] private GameObject item;
    [SerializeField] private float price;
    [SerializeField] private GameObject recreateObject;

    private Vector3 position;
   

    private void Start()
    {
        position = item.transform.localPosition;
    }


    private void OnMouseDown()
    {
        
        if(item != null)
        {
            item.GetComponentInChildren<LiquidManager>().SetCurrentLiquid(item.GetComponentInChildren<LiquidManager>().GetMaxLiquid());
        }
        else
        {
            
        }
    }

    public float GetPrice()
    {
        return price;
    }
}
