using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Drink;

public class Drink : MonoBehaviour
{
    #region ENUMS
    public enum TypeOfDrink
    {
        //Alcoholic Drinks
        Rum,
        Gin,
        Vodka,
        Whiskey,
        Tequila,
        //Juices
        OrangeJuice,
        LemonJuice,
        //Soft Drinks
        Cola,
        Soda,
        Tonic
    }

    public enum DrinkState
    {
        Idle,
        Shaked,
        Mixed
    }
    #endregion

    protected List<TypeOfDrink> ouncesInDrink;
    protected DrinkState drinkState;

    [SerializeField]
    protected int maxOunces;
    protected int currentOunces;
    protected bool isFull;

    private void Awake()
    {
        ouncesInDrink = new List<TypeOfDrink>();
    }

    #region GETTERS
    public List<TypeOfDrink> GetTypeOfOunces()
    {
        return ouncesInDrink;
    }

    public DrinkState GetDrinkState()
    {
        return drinkState;
    }

    public int GetCurrentOunces()
    {
        return currentOunces;
    }

    public bool GetIsFull()
    {
        return isFull;
    }

    #endregion

    #region SETTERS
    public void AddOunce(TypeOfDrink ounceToAdd)
    {
        ouncesInDrink.Add(ounceToAdd);
    }

    public void SetDrinkState(DrinkState state)
    {
        drinkState = state;
    }
    #endregion
}
