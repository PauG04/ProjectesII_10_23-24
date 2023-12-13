using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Liquid"))
        {
            Destroy(collision.gameObject);
            Debug.Log("AVEMARIA PURISIMA");
        }
    }
}