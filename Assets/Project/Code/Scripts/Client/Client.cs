using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Cocktail.Type order;
    private float payout;

    private void Awake()
    {
        ChooseDrink();
    }

    private void ChooseDrink()
    {
        order = (Cocktail.Type)Random.Range(0, (int)Cocktail.Type.Total - 1);
    }

    public bool CompareCocktails(Cocktail.Type cocktail)
    {
        if (cocktail == order)
            return true;
        return false;
    }

    public float ReceiveOrder(Cocktail.Type cocktail)
    {
        if(CompareCocktails(cocktail))
            return payout;
        return 0;
    }
}
