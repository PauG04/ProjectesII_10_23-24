using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Client : MonoBehaviour
{
    private CocktailNode.Type order;
    private float payment;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitClient();
    }

    #region INIT
    private void InitOrder()
    {
        //int randomOrder = Random.Range(0, WikiManager.instance.GetAvailableCocktails().Count);
        //order = WikiManager.instance.GetAvailableCocktails()[randomOrder].type;
        order = CocktailNode.Type.DiscoN;
    }

    private void InitPayment()
    {
        payment = Random.Range(10.0f, 100.0f);
    }

    private void InitClient()
    {
        InitOrder();
        InitPayment();
    }
    #endregion

    private bool CompareCocktails(CocktailNode.Type cocktail)
    {
        if (cocktail == order)
            return true;
        return false;
    }
     
    public void ReceiveCoctel(CocktailNode.Type cocktail)
    {
        if (CompareCocktails(cocktail))
        {
            ReactWell();
            return;
        }

        ReactBad();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cocktail") && CursorManager.instance.IsMouseUp())
        {
             ReceiveCoctel(collision.gameObject.GetComponent<Cocktail>().GetCocktail().type);
        }
    }

    private void ReactWell()
    {
        //Change Animation
        Pay();
        //Wait X Seconds
        ClientManager.instance.CreateNewClient();
        GameObject.Destroy(gameObject);
    }

    private void ReactBad()
    {
        //Change Animation
        //Wait X Seconds
        ClientManager.instance.CreateNewClient();
        GameObject.Destroy(gameObject);
    }

    private void Pay()
    {
        EconomyManager.instance.AddMoney(payment);
    }
}
