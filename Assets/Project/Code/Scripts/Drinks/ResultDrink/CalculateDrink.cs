using System.Collections.Generic;
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

    public string CalculateResultDrink(Dictionary<DrinkNode, int> typesOfDrink, CocktailNode.State state, Sprite sprite, Dictionary<ItemNode, int> decorations, CocktailNode.Type order)
    {
        switch (order)
        {
            case CocktailNode.Type.Roncola:
                return CheckCocktail(typesOfDrink, state, allCocktails["Roncola"], sprite, decorations);

            case CocktailNode.Type.Mojito:
                return CheckCocktail(typesOfDrink, state, allCocktails["Mojito"], sprite, decorations);

            case CocktailNode.Type.Ginlemmon:
                return CheckCocktail(typesOfDrink, state, allCocktails["Ginlemmon"], sprite, decorations);

            case CocktailNode.Type.Gintonic:
                return CheckCocktail(typesOfDrink, state, allCocktails["Gintonic"], sprite, decorations);

            case CocktailNode.Type.AguaDePalencia:
                return CheckCocktail(typesOfDrink, state, allCocktails["AguaDePalencia"], sprite, decorations);

            case CocktailNode.Type.WhiskeySour:
                return CheckCocktail(typesOfDrink, state, allCocktails["WhiskeySour"], sprite, decorations);

            case CocktailNode.Type.Sangria:
                return CheckCocktail(typesOfDrink, state, allCocktails["Sangria"], sprite, decorations);

            case CocktailNode.Type.Kalimotxo:
                return CheckCocktail(typesOfDrink, state, allCocktails["Kalimotxo"], sprite, decorations);

            case CocktailNode.Type.Vodka:
                return CheckCocktail(typesOfDrink, state, allCocktails["Vodka"], sprite, decorations);
            
            case CocktailNode.Type.CocaCola:
                return CheckCocktail(typesOfDrink, state, allCocktails["CocaCola"], sprite, decorations);
            case CocktailNode.Type.LemmonJuice:
                return CheckCocktail(typesOfDrink, state, allCocktails["LemonJuice"], sprite, decorations);
            case CocktailNode.Type.Gazpacho:
                return CheckCocktail(typesOfDrink, state, allCocktails["Gazpacho"], sprite, decorations);       
            default:
                return "ERROR";
        }        
    }


    private string CheckCocktail(Dictionary<DrinkNode, int> typesOfDrink, CocktailNode.State state, CocktailNode cocktail, Sprite sprite, Dictionary<ItemNode, int> decorations)
    {
        //Check if same amount of ingredients
        if (typesOfDrink.Count != cocktail.ingredients.Count)
            return "BadIngredients";

        foreach (KeyValuePair<DrinkNode, int> ingredient in cocktail.ingredients)
        {
            //Check if same drinkTypes
            if (!typesOfDrink.ContainsKey(ingredient.Key))
                return "BadIngredients";

            //Check if same quantity
            if (typesOfDrink[ingredient.Key] < ingredient.Value * 10 * cocktail.errorMargin)
                return "BadIngredients";
        }

        //Check if same state
        if (state != cocktail.state)
        {
            return "BadState";
        }

        //Check if same sprite
        if (sprite != cocktail.sprite)
            return "BadGlass";

        if (decorations.Count < 1)
            return "NoIce";
        foreach (KeyValuePair<ItemNode, int> decoration in cocktail.decorations)
        {
            //Check if same quantity
            if (decoration.Value == 1 || decoration.Value == 2)
            {
                if (decorations[decoration.Key] < 1)
                    return "NoIce";
                if (decorations[decoration.Key] > 2)
                    return "MuchIce";
            }
            else
            {
                if (decorations[decoration.Key] < 3)
                    return "NoIce";
                if (decorations[decoration.Key] > 5)
                    return "MuchIce";
            }
        }
        Debug.Log(cocktail);
        return "Good";
    }

    public Dictionary<string, CocktailNode> GetAllCocktails()
    {
        return allCocktails;
    }
}
