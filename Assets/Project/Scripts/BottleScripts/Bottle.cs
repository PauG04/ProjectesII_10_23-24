using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Drink
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private int currentSprite;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        sprites = new Sprite[10];
        currentSprite = 0;
        spriteRenderer.sprite = sprites[0];

        for (int i = 0; i < maxOunces; i++)
        {
            ouncesInDrink.Add(typeOfDrink);
        }
        currentOunces = maxOunces;
        isFull = true;
    }

    public void RefillOunces()
    {
        currentOunces = maxOunces;
    }

    public void SubstractOneOunce()
    {
        currentOunces--;
        currentSprite++;
        if(currentSprite > 9)
            spriteRenderer.sprite = null;
        else
            spriteRenderer.sprite = sprites[currentSprite];
    }
}
