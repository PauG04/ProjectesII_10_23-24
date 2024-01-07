using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField] private GameObject desktop;
    [SerializeField] private Item ice;
    [SerializeField] private Item lemon;

    private bool isCreated = false;

    private void Update()
    {
       if(desktop.transform.localScale.x > 0.98 && !isCreated)
       {
            Rigidbody2D gameObjectsRigidBody = gameObject.AddComponent<Rigidbody2D>();
            gameObjectsRigidBody.isKinematic = true;
            isCreated = true;
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Decoration")) 
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("IceBroken"))
        {
            InventoryManager.instance.AddItem(ice);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("LemonSlide"))
        {
            InventoryManager.instance.AddItem(lemon);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Mint"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Leaf"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Liquid"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("IceLiquid"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("BottleBroken"))
        {
            Destroy(collision.gameObject);
        }
        
    }
}
