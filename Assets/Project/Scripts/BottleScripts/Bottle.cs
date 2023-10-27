using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Drink
{
    private void Awake()
    {
        currentOunces = maxOunces;
        isFull = true;
    }

    public void RefillOunces()
    {
        currentOunces = maxOunces;
    }

    public void SubstractOneOunce()
    {
        currentOunces--;
    }
}
