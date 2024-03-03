using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class CocktailIngredientDictionary
{
    [SerializeField] NewDrinkTypeIntItem[] items;

    public Dictionary<DrinkNode, int> ToDictionary()
    {
        Dictionary<DrinkNode, int> newDict = new Dictionary<DrinkNode, int>();

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
    [SerializeField] public DrinkNode type;
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

[Serializable]
public class CocktailDecorationsDictionary
{
    [SerializeField] NewItemNodeIntItem[] items;

    public Dictionary<ItemNode, int> ToDictionary()
    {
        Dictionary<ItemNode, int> newDict = new Dictionary<ItemNode, int>();

        foreach (NewItemNodeIntItem item in items)
        {
            newDict.Add(item.itemNode, item.quantity);
        }

        return newDict;
    }
}

[Serializable]
public class NewItemNodeIntItem
{
    [SerializeField] public ItemNode itemNode;
    [SerializeField] public int quantity;
}
