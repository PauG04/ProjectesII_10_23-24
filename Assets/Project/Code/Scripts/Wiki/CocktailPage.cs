using TMPro;
using UnityEngine;

public class CocktailPage : MonoBehaviour
{
    [SerializeField] private CocktailNode cocktail;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro subtitleText;
    [SerializeField] private TextMeshPro descriptionText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private RectTransform turnPage;

    public void InitPage()
    {
        nameText.text = cocktail.cocktailName;
        int integerPart = (int)cocktail.price;
        subtitleText.text = integerPart.ToString() + "'99 €";
        descriptionText.text = cocktail.description;
        spriteRenderer.sprite = Resources.Load<Sprite>(cocktail.sprite.name + "Wiki");
        
    }
}
