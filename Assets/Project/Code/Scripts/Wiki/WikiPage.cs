using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WikiPage : MonoBehaviour
{
    [SerializeField] private bool isLeft;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image image;

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
