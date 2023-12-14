using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Drink;

public class ResultPrecentDrink : MonoBehaviour
{
    private LiquidManager liquidManager;
    private Dictionary<Drink.TypeOfDrink, int> lastDrink;

    [SerializeField] private DrinkState shakeState;

    private TypeOfCocktail result;

    #region Drinks
    private int rum;
    private int gin;
    private int vodka;
    private int whiskey;
    private int tequila;
    private int orangeJuice;
    private int lemonJuice;
    private int cola;
    private int soda;
    private int tonic;
    #endregion

    private void Awake()
    {
        liquidManager = GetComponent<LiquidManager>();
    }

    private void Update()
    {
        if (lastDrink != liquidManager.GetTypeOfDrinkInside())
        {
            UpdateDrinks();
            Result();
        }
    }

    private void UpdateDrinks()
    {
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Rum))
        {
            rum = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Rum] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Gin))
        {
            gin = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Gin] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Vodka))
        {
            vodka = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Vodka] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Whiskey))
        {
            whiskey = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Whiskey] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Tequila))
        {
            tequila = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Tequila] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.OrangeJuice))
        {
            orangeJuice = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.OrangeJuice] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.LemonJuice))
        {
            lemonJuice = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.LemonJuice] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Cola))
        {
            cola = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Cola] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Soda))
        {
            soda = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Soda] * 100 / liquidManager.GetMaxCapacity();
        }
        if (liquidManager.GetTypeOfDrinkInside().ContainsKey(TypeOfDrink.Tonic))
        {
            tonic = liquidManager.GetTypeOfDrinkInside()[TypeOfDrink.Tonic] * 100 / liquidManager.GetMaxCapacity();
        }

        lastDrink = liquidManager.GetTypeOfDrinkInside();
    }

    private void Result()
    {
        if (shakeState == DrinkState.Shaked)
        {
            if ((rum >= 30.0f && rum <= 40.0f) && (cola >= 60.0f && cola <= 70.0f))
            {
                result = TypeOfCocktail.RonCola;
            }
        }
    }

    public TypeOfCocktail GetResult()
    {
        return result;
    }

    public void SetShakerState(DrinkState shakeState)
    {
        this.shakeState = shakeState;
    }
}
