using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Drink
{
    private LiquidAnimation liquidAnimation;

    private void Awake()
    {
        for (int i = 0; i < maxOunces; i++)
        {
            ouncesInDrink.Add(typeOfDrink);
        }
        currentOunces = maxOunces;
        isFull = true;

        liquidAnimation = GetComponentInChildren<LiquidAnimation>();
    }

    public void RefillOunces()
    {
        currentOunces = maxOunces;
    }

    public void SubstractOneOunce()
    {
        AudioManager.instance.Play("ounceInShaker");
        currentOunces--;
        liquidAnimation.SetAnimation(currentOunces);

    }
}
