using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPanel : MonoBehaviour
{
    private CocktailNode cocktail;

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        //cocktail = ClientManager.instance.GetCurrentClientScript().GetOrder();
        //if(cocktail != null) 
        //{
        //    textMesh.text = cocktail.cocktailName.ToString();
        //}      
        textMesh.text = " ";
    }
    public void SetDrink(CocktailNode _cocktail)
    {
        cocktail = _cocktail;
    }
}
