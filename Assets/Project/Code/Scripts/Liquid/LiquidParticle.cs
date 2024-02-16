using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    private DrinkNode.Type drinkType;
    private CocktailNode.Type cocktailType;
    private CocktailNode.State cocktailState;    

    public void SetDrinkType(DrinkNode.Type drinkType)
    {
        this.drinkType = drinkType;
    }

    public DrinkNode.Type GetDrinkType()
    {
        return drinkType;
    }
}
