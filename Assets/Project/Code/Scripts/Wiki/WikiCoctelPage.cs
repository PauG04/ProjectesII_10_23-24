using TMPro;
using UnityEngine;

public class WikiCoctelPage : MonoBehaviour
{
    [SerializeField] private bool isLeft;

    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro subtitleText;
    [SerializeField] private TextMeshPro descriptionText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private RectTransform turnPage;

    public void UpdatePage(CocktailNode cocktail)
    {
        nameText.text = cocktail.cocktailName;
        int integerPart = (int)cocktail.price;
        subtitleText.text = integerPart.ToString() + "'99 €";
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
