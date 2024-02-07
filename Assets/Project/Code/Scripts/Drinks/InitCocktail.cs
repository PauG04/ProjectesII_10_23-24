using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCocktail : MonoBehaviour
{
    [SerializeField] private Cocktail cocktail;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cocktail.sprite;
    }

    public Cocktail GetCocktail()
    {
        return cocktail;
    }
}
