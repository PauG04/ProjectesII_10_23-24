using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CocktailIngredientDictionary
{
    [SerializeField] NewDrinkTypeIntItem[] items;

    public Dictionary<DrinkNode.Type, int> ToDictionary()
    {
        Dictionary<DrinkNode.Type, int> newDict = new Dictionary<DrinkNode.Type, int>();

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
    [SerializeField] public DrinkNode.Type type;
    [SerializeField] public int quantity;
}

[Serializable]
public class TypeOfCocktailsDictionary
{
    [SerializeField] NewStringCocktailItem[] items;

    public Dictionary<string, CocktailNode> ToDictionary()
    {
        Dictionary<string, CocktailNode> newDict = new Dictionary<string, CocktailNode>();

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
    [SerializeField] public CocktailNode cocktail;
}

