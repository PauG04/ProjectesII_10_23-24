using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DestroyPaint : MonoBehaviour
{
    private int hits = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            hits--;
            if (hits == 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
