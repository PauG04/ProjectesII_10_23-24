using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    private DrinkNode.Type drinkType;
    private CocktailNode.Type cocktailType;
    private CocktailNode.State cocktailState;

    private float destroyTime = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    public void SetDrinkType(DrinkNode.Type drinkType)
    {
        this.drinkType = drinkType;
    }

    public DrinkNode.Type GetDrinkType()
    {
        return drinkType;
    }
}
