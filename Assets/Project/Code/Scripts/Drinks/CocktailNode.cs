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
        Ginlemmon,
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

    public CocktailIngredientDictionary serializableIngredients;
    public Dictionary<DrinkNode, int> ingredients;

    public CocktailDecorationsDictionary serializableDecorations;
    public Dictionary<ItemNode, int> decorations;

    private void OnEnable()
    {
        ingredients = serializableIngredients.ToDictionary();
        decorations = serializableDecorations.ToDictionary();
        InitDescription();
    }

    private void InitDescription()
    {
        description = "";
        //Ingridients
        foreach (KeyValuePair<DrinkNode, int> ingridient in ingredients)
        {
            description += ingridient.Value;

            if (ingridient.Value > 1)
                description += " Onzas de";
            else
                description += " Onza de";

            description += " " + ingridient.Key.spanishName + "\n";
        }
        //Shaker State
        description += "Serivir ";
        if (state == State.Idle)
            description += "directamente en vaso";
        else if (state == State.Shaked)
            description += "agitado";
        else
            description += "mezclado perfectamente";
        //Decorations
        //Needs implementation
    }
}