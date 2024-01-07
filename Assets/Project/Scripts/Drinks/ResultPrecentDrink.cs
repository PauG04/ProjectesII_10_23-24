using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LiquidManager;

public class ResultPrecentDrink : MonoBehaviour
{
    public enum TypeOfCocktail
    {
        Empty,
        Sekiro,
        Morgana,
        Thresh,
        PipiStrate,
        MoszkowskiFlip,
        LobsterCrami,
        PinkLeibel,
        Tiefti,
        Razz,
        Invade,
        DiscoN,
        Mierdon
    }

    [SerializeField] private LiquidManager liquidManager;
    [SerializeField] private Dictionary<LiquidManager.TypeOfDrink, int> lastDrink;

    [SerializeField] private LiquidManager.DrinkState shakeState;

    [SerializeField] private TypeOfCocktail result;

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
        lastDrink = new Dictionary<LiquidManager.TypeOfDrink, int>();
    }

    private void Update()
    {
        if (CompareDrinks(lastDrink, liquidManager.GetTypeOfDrinkInside()))
        {
            UpdateDrinks();
	        Result();
	        Debug.Log("GlacierSpirit: " + rum);
        }
    }

    private void UpdateDrinks()
	{
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Rum))
        //{
        //    rum = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Rum] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Gin))
        //{
        //    gin = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Gin] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Vodka))
        //{
        //    vodka = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Vodka] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Whiskey))
        //{
        //    whiskey = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Whiskey] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Tequila))
        //{
        //    tequila = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Tequila] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.OrangeJuice))
        //{
        //    orangeJuice = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.OrangeJuice] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.LemonJuice))
        //{
        //    lemonJuice = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.LemonJuice] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Cola))
        //{
        //    cola = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Cola] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Soda))
        //{
        //    soda = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Soda] * 100 / liquidManager.GetMaxCapacity();
        //}
        //if (liquidManager.GetTypeOfDrinkInside().ContainsKey(LiquidManager.TypeOfDrink.Tonic))
        //{
        //    tonic = liquidManager.GetTypeOfDrinkInside()[LiquidManager.TypeOfDrink.Tonic] * 100 / liquidManager.GetMaxCapacity();
        //}

        //lastDrink = liquidManager.GetTypeOfDrinkInside();
    }

    private void Result()
    {
        //if (shakeState == LiquidManager.DrinkState.Shaked)
        //{
        //    if ((rum >= 30.0f && rum <= 40.0f) && (cola >= 60.0f && cola <= 70.0f))
        //    {
        //        result = TypeOfCocktail.RonCola;
        //    }
        //}
    }

    public bool CompareDrinks(Dictionary<TypeOfDrink, int> drink1, Dictionary<TypeOfDrink, int> drink2)
    {
        if (drink1 == drink2)
        {
            return true;
        }

        if ((drink1 == null) || (drink2 == null))
        {
            return false;
        }

        if (drink1.Count != drink2.Count)
        {
            return false;
        }

        EqualityComparer<int> valueComparer = EqualityComparer<int>.Default;

        foreach (KeyValuePair<TypeOfDrink, int> kvp in drink1)
        {
            int value2;
            if (!drink2.TryGetValue(kvp.Key, out value2))
            {
                return false;
            }
            if (!valueComparer.Equals(kvp.Value, value2))
            {
                return false;
            }
        }
        return true;
    }

    public TypeOfCocktail GetResult()
    {
        return result;
    }

    public void SetShakerState(LiquidManager.DrinkState shakeState)
    {
        this.shakeState = shakeState;
    }
}