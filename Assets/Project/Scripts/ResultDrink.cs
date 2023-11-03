using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using static Drink;

public class ResultDrink : MonoBehaviour
{

    [Header("Shaker")]
    public List<TypeOfDrink> shakerResult;
    public DrinkState shake;
    private int shakerPreviousDrinks;

    [Header("Drinks Inside")]
    [SerializeField] private TextMeshPro text;

    [SerializeField] private bool listChanged;

    #region Drinks
    [SerializeField] private int rum;
    [SerializeField] private int gin;
    [SerializeField] private int vodka;
    [SerializeField] private int whiskey;
    [SerializeField] private int tequila;
    [SerializeField] private int orangeJuice;
    [SerializeField] private int lemonJuice;
    [SerializeField] private int cola;
    [SerializeField] private int soda;
    [SerializeField] private int tonic;
    #endregion

    private void Update()
    {
        listChanged = shakerPreviousDrinks != shakerResult.Count();

        if (listChanged)
        {
            UpdateDrinks();
        }

        if (shake == DrinkState.Idle)
        {
            if (gin == 2 && tonic == 4)
            {
                text.text = "Gin Tonic";
            }
            else if (rum == 2 && cola == 4)
            {
                text.text = "Ron Cola";
            }
            else if (whiskey == 2)
            {
                text.text = "Old Fashioned";
            }
        }
        if (shake == DrinkState.Mixed)
        {
            if (gin == 2 && tonic == 4)
            {
                text.text = "Gin Tonic Mixed";
            }
            if (rum == 2 && lemonJuice == 2 && soda == 4)
            {
                text.text = "Mojito";
            }
        }
        if (shake == DrinkState.Shaked)
        {
            if (gin == 2 && tonic == 4)
            {
                text.text = "Gin Tonic Shaked";
            }
            if (tequila == 2 && lemonJuice == 2 && vodka == 2)
            {
                text.text = "Margarita";
            }
        }
    }

    private void UpdateDrinks()
    {
        rum = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Rum).Count();
        gin = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Gin).Count();
        vodka = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Vodka).Count();
        whiskey = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Whiskey).Count();
        tequila = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Tequila).Count();
        orangeJuice = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.OrangeJuice).Count();
        lemonJuice = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.LemonJuice).Count();
        cola = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Cola).Count();
        soda = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Soda).Count();
        tonic = shakerResult.Where(drink => drink != null && drink == TypeOfDrink.Tonic).Count();

        shakerPreviousDrinks = shakerResult.Count();
    }
}
