using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInBucket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("IceBucket"))
        {
            Destroy(gameObject);
            Debug.Log("ha entrado");
        }
        if(collision.CompareTag("TaskBar"))
        {
            Destroy(gameObject);
            Debug.Log("Se rompe");
        }
    }
}
