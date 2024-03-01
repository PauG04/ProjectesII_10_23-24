using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientInformation : MonoBehaviour
{
    private CocktailNode drink;

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        if(drink != null) 
        {
            textMesh.text = drink.name.ToString();
        }      
    }
    public void SetDrink(CocktailNode _drink)
    {
        drink = _drink;
    }
}
