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
    public Dictionary<DrinkNode, int> ingredients;

    private void OnEnable()
    {
        ingredients = SerializableIngredients.ToDictionary();
        InitDescription();
    }

    private void InitDescription()
    {
        description = "";
        foreach (KeyValuePair<DrinkNode, int> ingridient in ingredients)
        {
            description += ingridient.Value;

            if (ingridient.Value > 1)
                description += " Onzas";
            else
                description += " Onza";

            description += " " + ingridient.Key.spanishName + "\n";
        }
        description += "State: " + state.ToString();
    }
}