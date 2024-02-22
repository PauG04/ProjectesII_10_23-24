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
    [SerializeField] private RectTransform turnPage;

    private void Start()
    {
        if (isLeft)
        {
            turnPage.anchorMin = new Vector2(0, 0.5f);
            turnPage.anchorMax = new Vector2(0, 0.5f);
            turnPage.pivot = new Vector2(0, 0.5f);
            turnPage.anchoredPosition = new Vector2(-0.075f, turnPage.transform.localPosition.y);
        }
    }
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

    public bool GetIsLeft()
    {
        return isLeft;
    }
}
