using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocktail : MonoBehaviour
{
    [SerializeField] private CocktailNode cocktail;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cocktail.sprite;
    }

    public CocktailNode GetCocktail()
    {
        return cocktail;
    }
}
