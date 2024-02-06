using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Cocktail.Type order;
    private float payment;

    private void Awake()
    {
        InitClient();
    }

    #region INIT
    private void InitOrder()
    {
        int randomOrder = Random.Range(0, WikiManager.instance.GetAvailableCocktails().Count);
        order = WikiManager.instance.GetAvailableCocktails()[randomOrder].type;
    }

    private void InitPayment()
    {
        payment = (float)Random.Range(0, 100);
    }

    private void InitClient()
    {
        InitOrder();
        InitPayment();
    }
    #endregion

    private bool CompareCocktails(Cocktail.Type cocktail)
    {
        if (cocktail == order)
            return true;
        return false;
    }

    //Llamar a esta funcion cuando dejes una bebida encima del cliente
    public float ReceiveOrder(Cocktail.Type cocktail)
    {
        if(CompareCocktails(cocktail))
            return payment;
        return 0;
    }
}
