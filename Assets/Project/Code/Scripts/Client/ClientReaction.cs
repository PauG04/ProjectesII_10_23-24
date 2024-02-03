using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientReaction : MonoBehaviour
{
    private Cocktail.Type clientOrder;

    private void Awake()
    {
        clientOrder = ChooseDrink();
    }

    private Cocktail.Type ChooseDrink()
    {
        return Cocktail.Type.Empty;
    }

    public bool CompareCocktails(Cocktail.Type cocktail)
    {
        if (cocktail == clientOrder)
            return true;
        return false;
    }
}
