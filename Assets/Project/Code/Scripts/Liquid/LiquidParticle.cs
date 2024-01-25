using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    public Color color;
	public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.color = color;
    }
}
