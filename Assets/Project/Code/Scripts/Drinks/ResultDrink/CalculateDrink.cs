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
        if (CheckCocktail(typesOfDrink, state, allCocktails["DiscoN"]))
            return Cocktail.Type.DiscoN;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Invade"]))
            return Cocktail.Type.Invade;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Morgana"]))
            return Cocktail.Type.Morgana;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Thresh"]))
            return Cocktail.Type.Thresh;

        if (CheckCocktail(typesOfDrink, state, allCocktails["PinkLeibel"]))
            return Cocktail.Type.PinkLeibel;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Morgana"]))
            return Cocktail.Type.Morgana;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Sekiro"]))
            return Cocktail.Type.Sekiro;

        if (CheckCocktail(typesOfDrink, state, allCocktails["LobsterCrami"]))
            return Cocktail.Type.LobsterCrami;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Tiefti"]))
            return Cocktail.Type.Tiefti;

        if (CheckCocktail(typesOfDrink, state, allCocktails["MoszkowskiFlip"]))
            return Cocktail.Type.MoszkowskiFlip;

        if (CheckCocktail(typesOfDrink, state, allCocktails["PipiStrate"]))
            return Cocktail.Type.PipiStrate;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Razz"]))
            return Cocktail.Type.Razz;

        return Cocktail.Type.Error;
    }

    private bool CheckCocktail(Dictionary<Drink.Type, int> typesOfDrink, Cocktail.State state, Cocktail cocktail)
    {
        //Check if same amount of ingredients
        if (typesOfDrink.Count != cocktail.ingredients.Count)
            return false;

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
