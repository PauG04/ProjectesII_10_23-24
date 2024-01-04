using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    private TextMeshProUGUI textToShow;

    private void Awake()
    {
        textToShow = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeName(TypeOfCocktails cocktail)
    {
        textToShow.text = cocktail.cocktailName + " - " + cocktail.price + "$";
    }
    public void ChangeSubtitle(TypeOfCocktails cocktail)
    {
        textToShow.text = cocktail.subtitle;
    }
    public void ChangeDescription(TypeOfCocktails cocktail)
    {
        textToShow.text = cocktail.description;
    }
}
