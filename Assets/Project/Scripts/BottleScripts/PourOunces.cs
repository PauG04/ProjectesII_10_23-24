using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOunces : MonoBehaviour
{
    private Bottle bottle;

    //[SerializeField]
    //private OuncesCounter ouncesCounter;

    private Drink shaker;

    private BoxCollider2D boxCollider;

    private GameObject shakerObject;

    [SerializeField]
    private BoxCollider2D shakerCollider;

    private void Awake()
    {
        bottle = GetComponent<Bottle>();
        boxCollider = GetComponent<BoxCollider2D>();     
    }

    private void Update()
    {
        if(shakerObject!= null) 
        {
            shakerObject = GameObject.Find("Shaker");
            shakerCollider = shakerObject.GetComponent<BoxCollider2D>();
            shaker = shakerObject.GetComponent<Drink>();
        }
    }

    private void OnMouseUp()
    {
        if (boxCollider.IsTouching(shakerCollider) && bottle.GetCurrentOunces() > 0)
        {
            shaker.AddOunce(bottle.GetTypeOfOunces()[0]);
            bottle.SubstractOneOunce();
            //ouncesCounter.AddOneToCounter();
        }
    }
}
