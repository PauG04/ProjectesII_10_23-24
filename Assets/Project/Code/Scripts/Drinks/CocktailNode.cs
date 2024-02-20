using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cocktail", menuName = "Cocktail", order = 3)]
public class CocktailNode : ScriptableObject
{
    public enum State
    {
        Idle,
        Shaked,
        Mixed
    }

    public enum Type
    {
        Roncola,
        Mojito,
        PomadaMenorquina,
        Gintonic,
        AguaDePalencia,
        WhiskeySour,
        Sangria,
        Kalimotxo,
        Cocktail09,
        Cocktail10,
        Cocktail11,

        Total,
        Error,
        Empty
    }

    public State state;
    public Type type;
    public float price;
    public string cocktailName;
    public string subtitle;
    public string description;
    public Sprite sprite;

    public CocktailIngredientDictionary SerializableIngredients;
    public Dictionary<DrinkNode.Type, int> ingredients;

    private void OnEnable()
    {
        ingredients = SerializableIngredients.ToDictionary();
        InitDescription();
    }

    private void InitDescription()
    {
        description = "";
        foreach (KeyValuePair<DrinkNode.Type, int> ingridient in ingredients)
        {
            description += ingridient.Value + " " + ingridient.Key.ToString();

            if (ingridient.Value > 1)
                description += " Onzas" + "\n";
            else
                description += " Onza" + "\n";
        }
        description += "State: " + state.ToString();
    }
}