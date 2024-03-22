using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPanel : MonoBehaviour
{
    private CocktailNode cocktail;

    private TextMeshPro textMesh;

    [Header("ClientManager")]
    [SerializeField] private ClientManager clientManager;

    private ClientNode client;
    private GameObject clientObject;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (clientObject == null)
        {
            textMesh.text = " ";
        }
        if (clientObject != null && clientObject.GetComponent<BoxCollider2D>().enabled)
        {
            SetInformation();
        }
        else
        {
            client = clientManager.GetClient();
            clientObject = clientManager.GetClientObject();
        }
    }

    private void SetInformation()
    {
        cocktail = ClientManager.instance.GetCurrentClientScript().GetOrder();
        if (cocktail != null && ClientManager.instance.GetClient().acceptsAll && !ClientManager.instance.GetClient().dontPay)
        {
            textMesh.text = "?";
        }
        else if (cocktail != null && !ClientManager.instance.GetClient().notNeedTakeDrink)
        {
            textMesh.text = cocktail.cocktailName.ToString();
        }
        else
        {
            textMesh.text = " ";
        }
    }
    public void SetDrink(CocktailNode _cocktail)
    {
        cocktail = _cocktail;
    }
}
