using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CalculateDrink : MonoBehaviour
{
    [SerializeField] private TypeOfCocktailsDictionary serializableCocktails;
    private Dictionary<string, Cocktail> allCocktails;

    [SerializeField] private float errorMargin;

    private void Awake()
    {
        allCocktails = serializableCocktails.ToDictionary();
    }

    public Cocktail.Type CalculateResultDrink(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state)
    {
        Cocktail.Type cocktail = Cocktail.Type.Mierdon;

        switch (typesOfDrink.Keys.Count)
        {
            case 2:
                cocktail = CalculateCase2Drinks(typesOfDrink, state);
                break;

            case 3:
                cocktail = CalculateCase3Drinks(typesOfDrink, state);
                break;

            case 4:
                cocktail = CalculateCase4Drinks(typesOfDrink, state);
                break;

            case 5:
                cocktail = CalculateCase5Drinks(typesOfDrink, state);
                break;

            default:
                break;
        }

        return cocktail;
    }

    #region CHECK_CASES
    private Cocktail.Type CalculateCase2Drinks(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state)
    {
        if (CheckCocktail(typesOfDrink, state, allCocktails["Invade"]))
            return Cocktail.Type.Invade;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Morgana"]))
            return Cocktail.Type.Morgana;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Thresh"]))
            return Cocktail.Type.Thresh;

        return Cocktail.Type.Mierdon;
    }

    private Cocktail.Type CalculateCase3Drinks(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state)
    {
        if (CheckCocktail(typesOfDrink, state, allCocktails["PinkLeibel"]))
            return Cocktail.Type.PinkLeibel;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["DiscoN"]))
            return Cocktail.Type.DiscoN;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["Morgana"]))
            return Cocktail.Type.Morgana;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["Sekiro"]))
            return Cocktail.Type.Sekiro;

        return Cocktail.Type.Mierdon;
    }

    private Cocktail.Type CalculateCase4Drinks(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state)
    {
        if (CheckCocktail(typesOfDrink, state, allCocktails["LobsterCrami"]))
            return Cocktail.Type.LobsterCrami;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["Tiefti"]))
            return Cocktail.Type.Tiefti;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["MoszkowskiFlip"]))
            return Cocktail.Type.MoszkowskiFlip;

        return Cocktail.Type.Mierdon;
    }

    private Cocktail.Type CalculateCase5Drinks(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state)
    {
        if (CheckCocktail(typesOfDrink, state, allCocktails["PipiStrate"]))
            return Cocktail.Type.PipiStrate;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["Razz"]))
            return Cocktail.Type.Razz;

        return Cocktail.Type.Mierdon;
    }
    #endregion

    private bool CheckCocktail(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state, Cocktail cocktail)
    {
        //Check if same state
        if (state != cocktail.state)
            return false;

        foreach (KeyValuePair<Drink.Type, int> ingredient in cocktail.ingredients)
        {
            //Check if same drinkTypes
            if(!typesOfDrink.ContainsKey(ingredient.Key))
                return false;

            //Check if same quantity
            if (typesOfDrink[ingredient.Key] < ingredient.Value * 10 * (errorMargin / 100))
                return false;
        }

        return true;
    }

}
