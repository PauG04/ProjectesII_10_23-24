using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOunces : MonoBehaviour
{
    private Bottle bottle;

    //[SerializeField]
    //private OuncesCounter ouncesCounter;

    private Drink drinkInsideShaker;

    private BoxCollider2D boxCollider;

    private GameObject shaker;

    [SerializeField]
    private BoxCollider2D shakerCollider;

    private void Awake()
    {
        bottle = GetComponent<Bottle>();
        boxCollider = GetComponent<BoxCollider2D>();     
    }

    private void OnMouseDown()
    {
        if(GameObject.Find("Shaker") != null) 
        {
            shaker = GameObject.Find("Shaker");
            shakerCollider = shaker.GetComponent<BoxCollider2D>();
            drinkInsideShaker = shaker.GetComponent<Drink>();
        }
    }

    private void OnMouseUp()
    {
        if (shaker != null)
        {
            if (boxCollider.IsTouching(shakerCollider) && bottle.GetCurrentOunces() > 0 && bottle.GetCurrentOunces() < bottle.GetMaxOunces())
            {
                drinkInsideShaker.AddOunce(bottle.GetTypeOfOunces()[0]);
                bottle.SubstractOneOunce();
                //ouncesCounter.AddOneToCounter();
            }
        }
        
    }
}
