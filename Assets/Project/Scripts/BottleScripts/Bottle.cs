using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Drink
{
    private void Start()
    {
        for (int i = 0; i < maxOunces; i++)
        {
            ouncesInDrink.Add(typeOfDrink);
        }
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
