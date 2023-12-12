using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideLeaf : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Leaf"))
        {
            Debug.Log("Leaf Collided With Inventory");
            Destroy(collision.gameObject);
        }
    }
}
