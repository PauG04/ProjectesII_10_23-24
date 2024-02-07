using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cocktail", menuName = "Cocktail", order = 3)]
public class Cocktail : ScriptableObject
{
    public enum State
    {
        Idle,
        Shaked,
        Mixed
    }

    public enum Type
    {
        DiscoN,
        Invade,
        LobsterCrami,
        Morgana,
        MoszkowskiFlip,
        PinkLeibel,
        PipiStrate,
        Razz,
        Sekiro,
        Thresh,
        Tiefti,

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
    public Dictionary<Drink.Type, int> ingredients;

    private void Awake()
    {
        ingredients = SerializableIngredients.ToDictionary();

    }
}