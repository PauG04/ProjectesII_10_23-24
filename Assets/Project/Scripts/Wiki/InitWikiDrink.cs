using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitWikiDrink : MonoBehaviour
{
    [SerializeField] TypeOfCocktails cocktail;
    private TextMeshProUGUI textToShow;
    private Image image;

    private void Awake()
    {
        textToShow = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();

        InitDrink();
    }

    private void InitDrink()
    {
        textToShow.text = cocktail.cocktailName + " - " + cocktail.price + "$\n" + cocktail.subtitle;
        image.sprite = cocktail.sprite;
    }
}
