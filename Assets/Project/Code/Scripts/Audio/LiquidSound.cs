using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSound : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Liquid"))
        {
            AudioManager.instance.PlaySFX("LiquidCollision");
        }
    }
    
}
