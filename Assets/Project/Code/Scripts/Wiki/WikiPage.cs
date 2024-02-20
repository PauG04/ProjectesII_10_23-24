using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WikiPage : MonoBehaviour
{
    [SerializeField] private bool isLeft;

    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro subtitleText;
    [SerializeField] private TextMeshPro descriptionText;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void UpdatePage(CocktailNode cocktail)
    {
        nameText.text = cocktail.cocktailName;
        subtitleText.text = cocktail.subtitle;
        descriptionText.text = cocktail.description;
        spriteRenderer.sprite = cocktail.sprite;
    }

    public void ClearPage()
    {
        nameText.text = "";
        subtitleText.text = "";
        descriptionText.text = "";
        spriteRenderer.sprite = null;
    }

    private void OnMouseDown()
    {
        if (isLeft) 
        { 
            WikiManager.instance.PrevPage();
        }
        else
            WikiManager.instance.NextPage();

    }
}
