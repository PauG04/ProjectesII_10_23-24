using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOunces : MonoBehaviour
{
    private Bottle bottle;

    [SerializeField]
    private OuncesCounter ouncesCounter;

    [SerializeField]
    private Drink shaker;

    private BoxCollider2D boxCollider;

    [SerializeField]
    private BoxCollider2D shakerCollider;

    private void Awake()
    {
        bottle = GetComponent<Bottle>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseUp()
    {
        if (boxCollider.IsTouching(shakerCollider) && bottle.GetCurrentOunces() > 0)
        {
            shaker.AddOunce(bottle.GetTypeOfOunces()[0]);
            bottle.SubstractOneOunce();
            ouncesCounter.AddOneToCounter();
        }
    }
}
