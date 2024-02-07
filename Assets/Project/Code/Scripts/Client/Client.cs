using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Cocktail.Type order;
    private float payment;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        InitClient();
    }

    #region INIT
    private void InitOrder()
    {
        //int randomOrder = Random.Range(0, WikiManager.instance.GetAvailableCocktails().Count);
        //order = WikiManager.instance.GetAvailableCocktails()[randomOrder].type;
        order = Cocktail.Type.DiscoN;

    }

    private void InitPayment()
    {
        payment = (float)Random.Range(10, 100);
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
     
    public float ReceiveCoctel(Cocktail.Type cocktail)
    {
        if (CompareCocktails(cocktail))
            return payment;
        return 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cocktail") && CursorManager.instance.IsMouseUp())
        {
             EconomyManager.instance.AddMoney(ReceiveCoctel(collision.gameObject.GetComponent<InitCocktail>().GetCocktail().type));
        }
    }
}
