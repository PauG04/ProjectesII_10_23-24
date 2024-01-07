using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideLeaf : MonoBehaviour
{
    [SerializeField] private Item mint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Leaf"))
        {
            InventoryManager.instance.AddItem(mint);
            Destroy(collision.gameObject);
        }
    }
}
