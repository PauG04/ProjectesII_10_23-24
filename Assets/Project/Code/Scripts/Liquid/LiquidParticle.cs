using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    [SerializeField] private DrinkNode drink;
    [SerializeField] private CocktailNode.State state;

    [SerializeField] private float destroyTime = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    public void SetCocktailState(CocktailNode.State cocktail)
    {
        this.state = cocktail;
    }
    public void SetDrink(DrinkNode drink)
    {
        this.drink = drink;
    }
    public DrinkNode GetDrink()
    {
        return drink;
    }
    public CocktailNode.State GetState()
    {
        return state;
    }
}
