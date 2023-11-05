using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using static Drink;

public class ResultDrink : MonoBehaviour
{
    [Header("Result")]
    public TypeOfCocktail result;

    [Header("Shaker")]
    public DrinkState shakeState;
    [SerializeField] private TextMeshPro textMeshPro;
    private int shakerPreviousDrinks;

    [Header("Drinks Inside")]
    public List<TypeOfDrink> drinksInside;
    private bool listChanged;

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

    private void Update()
    {
        listChanged = shakerPreviousDrinks != drinksInside.Count();

        if (listChanged)
        {
            UpdateDrinks();
        }

        MakeResult();
    }
    private void MakeResult()
    {
        if (shakeState == DrinkState.Idle)
        {
            if (gin == 2 && tonic == 4)
            {
                result = TypeOfCocktail.GinTonic;
            }
            else if (rum == 2 && cola == 4)
            {
                result = TypeOfCocktail.RonCola;
            }
            else if (whiskey == 2)
            {
                result = TypeOfCocktail.OldFashioned;
            }
        }
        if (shakeState == DrinkState.Mixed)
        {
            if (rum == 2 && lemonJuice == 2 && soda == 4)
            {
                result = TypeOfCocktail.Mojito;
            }
            else if (tequila == 2 && orangeJuice == 2)
            {
                result = TypeOfCocktail.TequilaSunrise;
            }
        }
        if (shakeState == DrinkState.Shaked)
        {
            if (tequila == 2 && lemonJuice == 2 && vodka == 2)
            {
                result = TypeOfCocktail.Margarita;
            }
            else if (vodka == 2 && lemonJuice == 2 && orangeJuice == 2)
            {
                result = TypeOfCocktail.SexOnTheBeach;
            }
            else if (orangeJuice == 4 && whiskey == 2)
            {
                result = TypeOfCocktail.FuzzyNavel;
            }

        }
        textMeshPro.text = result.ToString();
    }
    private void UpdateDrinks()
    {
        rum = drinksInside.Where(drink => drink == TypeOfDrink.Rum).Count();
        gin = drinksInside.Where(drink => drink == TypeOfDrink.Gin).Count();
        vodka = drinksInside.Where(drink => drink == TypeOfDrink.Vodka).Count();
        whiskey = drinksInside.Where(drink => drink == TypeOfDrink.Whiskey).Count();
        tequila = drinksInside.Where(drink => drink == TypeOfDrink.Tequila).Count();
        orangeJuice = drinksInside.Where(drink => drink == TypeOfDrink.OrangeJuice).Count();
        lemonJuice = drinksInside.Where(drink => drink == TypeOfDrink.LemonJuice).Count();
        cola = drinksInside.Where(drink => drink == TypeOfDrink.Cola).Count();
        soda = drinksInside.Where(drink => drink == TypeOfDrink.Soda).Count();
        tonic = drinksInside.Where(drink => drink == TypeOfDrink.Tonic).Count();

        shakerPreviousDrinks = drinksInside.Count();
    }
}

public enum TypeOfCocktail
{
    Empty,
    GinTonic,
    RonCola,
    OldFashioned,
    Mojito,
    Margarita,
    SexOnTheBeach,
    TequilaSunrise,
    FuzzyNavel
}