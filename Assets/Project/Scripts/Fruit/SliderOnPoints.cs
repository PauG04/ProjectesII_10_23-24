using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderOnPoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JuicerPoints"))
            Debug.Log("si");
    }
}
