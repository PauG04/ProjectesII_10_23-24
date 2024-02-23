using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class ChooseDrink : MonoBehaviour
{
    public Dictionary<CocktailNode.Type, Dialogue.Dialogue> type;

    public void Drink(bool shouldChoose)
    {
        foreach (CocktailNode drink in WikiManager.instance.GetAvailableCocktails())
        {

        }
    }
}
