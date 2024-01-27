using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CocktailIngredientDictionary
{
    [SerializeField] NewDrinkTypeIntItem[] items;

    public Dictionary<Drink.Type, int> ToDictionary()
    {
        Dictionary<Drink.Type, int> newDict = new Dictionary<Drink.Type, int>();

        foreach (NewDrinkTypeIntItem item in items)
        {
            newDict.Add(item.type, item.quantity);
        }

        return newDict;
    }
}

[Serializable]
public class NewDrinkTypeIntItem
{
    [SerializeField] public Drink.Type type;
    [SerializeField] public int quantity;
}

[Serializable]
public class TypeOfCocktailsDictionary
{
    [SerializeField] NewStringCocktailItem[] items;

    public Dictionary<string, Cocktail> ToDictionary()
    {
        Dictionary<string, Cocktail> newDict = new Dictionary<string, Cocktail>();

        foreach (NewStringCocktailItem item in items)
        {
            newDict.Add(item.cocktailName, item.cocktail);
        }

        return newDict;
    }
}

[Serializable]
public class NewStringCocktailItem
{
    [SerializeField] public string cocktailName;
    [SerializeField] public Cocktail cocktail;
}

