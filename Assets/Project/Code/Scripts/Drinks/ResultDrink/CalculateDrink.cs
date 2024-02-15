using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CalculateDrink : MonoBehaviour
{
    public static CalculateDrink instance { get; private set; }

    [SerializeField] private TypeOfCocktailsDictionary serializableCocktails;
    private Dictionary<string, CocktailNode> allCocktails;

    [SerializeField] private float errorMargin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        allCocktails = serializableCocktails.ToDictionary();
    }

    public CocktailNode.Type CalculateResultDrink(Dictionary<DrinkNode.Type, int> typesOfDrink, CocktailNode.State state)
    {
        if (CheckCocktail(typesOfDrink, state, allCocktails["DiscoN"]))
            return CocktailNode.Type.DiscoN;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Invade"]))
            return CocktailNode.Type.Invade;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Morgana"]))
            return CocktailNode.Type.Morgana;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Thresh"]))
            return CocktailNode.Type.Thresh;

        if (CheckCocktail(typesOfDrink, state, allCocktails["PinkLeibel"]))
            return CocktailNode.Type.PinkLeibel;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Morgana"]))
            return CocktailNode.Type.Morgana;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Sekiro"]))
            return CocktailNode.Type.Sekiro;

        if (CheckCocktail(typesOfDrink, state, allCocktails["LobsterCrami"]))
            return CocktailNode.Type.LobsterCrami;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Tiefti"]))
            return CocktailNode.Type.Tiefti;

        if (CheckCocktail(typesOfDrink, state, allCocktails["MoszkowskiFlip"]))
            return CocktailNode.Type.MoszkowskiFlip;

        if (CheckCocktail(typesOfDrink, state, allCocktails["PipiStrate"]))
            return CocktailNode.Type.PipiStrate;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Razz"]))
            return CocktailNode.Type.Razz;

        return CocktailNode.Type.Error;
    }

    private bool CheckCocktail(Dictionary<DrinkNode.Type, int> typesOfDrink, CocktailNode.State state, CocktailNode cocktail)
    {
        //Check if same amount of ingredients
        if (typesOfDrink.Count != cocktail.ingredients.Count)
            return false;

        //Check if same state
        if (state != cocktail.state)
            return false;

        foreach (KeyValuePair<DrinkNode.Type, int> ingredient in cocktail.ingredients)
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
