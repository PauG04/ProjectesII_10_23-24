using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WikiPage : MonoBehaviour
{
    [SerializeField] private bool isLeft;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI subtitleText;
    private TextMeshProUGUI descriptionText;
    private Image image;

    private void Awake()
    {
        if (isLeft)
        {
            nameText = GameObject.Find("CocktailName1").GetComponent<TextMeshProUGUI>();
            subtitleText = GameObject.Find("CocktailSubtitle1").GetComponent<TextMeshProUGUI>();
            descriptionText = GameObject.Find("CocktailDescription1").GetComponent<TextMeshProUGUI>();
            image = GameObject.Find("CocktailImage1").GetComponent<Image>();
        }
        else
        {
            nameText = GameObject.Find("CocktailName2").GetComponent<TextMeshProUGUI>();
            subtitleText = GameObject.Find("CocktailSubtitle2").GetComponent<TextMeshProUGUI>();
            descriptionText = GameObject.Find("CocktailDescription2").GetComponent<TextMeshProUGUI>();
            image = GameObject.Find("CocktailImage2").GetComponent<Image>();
        }
        
    }

    public void UpdatePage(Cocktail cocktail)
    {
        nameText.text = cocktail.cocktailName;
        subtitleText.text = cocktail.subtitle;
        descriptionText.text = cocktail.description;
        image.sprite = cocktail.sprite;
    }

    public void ClearPage()
    {
        nameText.text = "";
        subtitleText.text = "";
        descriptionText.text = "";
        image.sprite = null;
    }
}
