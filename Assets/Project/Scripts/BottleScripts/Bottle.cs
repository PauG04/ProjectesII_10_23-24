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
        currentOunces--;
        liquidAnimation.SetAnimation(currentOunces);
        switch (Random.Range(1, 5))
        {
            case 1:
                AudioManager.instance.Play("pourOunce1");
                break;
            case 2:
                AudioManager.instance.Play("pourOunce2");
                break;
            case 3:
                AudioManager.instance.Play("pourOunce3");
                break;
            case 4:
                AudioManager.instance.Play("pourOunce4");
                break;
            case 5:
                AudioManager.instance.Play("pourOunce5");
                break;
        }
    }
}
