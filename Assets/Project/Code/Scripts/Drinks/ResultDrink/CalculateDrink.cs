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

    public CocktailNode.Type CalculateResultDrink(Dictionary<DrinkNode, int> typesOfDrink, CocktailNode.State state, Sprite sprite, Dictionary<ItemNode, int> decorations)
    {
        if (CheckCocktail(typesOfDrink, state, allCocktails["Roncola"], sprite, decorations))
            return CocktailNode.Type.Roncola;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Mojito"], sprite, decorations))
            return CocktailNode.Type.Mojito;
        
        if (CheckCocktail(typesOfDrink, state, allCocktails["PomadaMenorquina"], sprite, decorations))
            return CocktailNode.Type.Ginlemmon;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Gintonic"], sprite, decorations))
            return CocktailNode.Type.Gintonic;

        if (CheckCocktail(typesOfDrink, state, allCocktails["AguaDePalencia"], sprite, decorations))
            return CocktailNode.Type.AguaDePalencia;

        if (CheckCocktail(typesOfDrink, state, allCocktails["WhiskeySour"], sprite, decorations))
            return CocktailNode.Type.WhiskeySour;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Sangria"], sprite, decorations))
            return CocktailNode.Type.Sangria;

        if (CheckCocktail(typesOfDrink, state, allCocktails["Kalimotxo"], sprite, decorations))
            return CocktailNode.Type.Kalimotxo;
        return CocktailNode.Type.Error;
    }

    private bool CheckCocktail(Dictionary<DrinkNode, int> typesOfDrink, CocktailNode.State state, CocktailNode cocktail, Sprite sprite, Dictionary<ItemNode, int> decorations)
    {
        //Check if same sprite
        if (sprite != cocktail.sprite)
            return false;

        foreach (KeyValuePair<ItemNode, int> decoration in cocktail.decorations)
        {
            //Check if same decorations
            if (!decorations.ContainsKey(decoration.Key))
                return false;

            //Check if same quantity
            if (decorations[decoration.Key] != decoration.Value)
                return false;
        }

        //Check if same amount of ingredients
        if (typesOfDrink.Count != cocktail.ingredients.Count)
            return false;

        //Check if same state
        if (state != cocktail.state)
            return false;

        foreach (KeyValuePair<DrinkNode, int> ingredient in cocktail.ingredients)
        {
            //Check if same drinkTypes
            if(!typesOfDrink.ContainsKey(ingredient.Key))
                return false;

            //Check if same quantity
            if (typesOfDrink[ingredient.Key] < ingredient.Value * 10 * (errorMargin / 100))
                return false;
        }
        Debug.Log(cocktail);
        return true;
    }

    public Dictionary<string, CocktailNode> GetAllCocktails()
    {
        return allCocktails;
    }
}
