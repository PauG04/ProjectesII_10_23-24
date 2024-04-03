using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyLiquid : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private GameObject item;
    [SerializeField] private float price;

    [Header("Boolean")]
    [SerializeField] private bool isItem;


    private void Start()
    {
        
    }


    private void OnMouseDown()
    {
       
    }

    public float GetPrice()
    {
        return price;
    }
}
