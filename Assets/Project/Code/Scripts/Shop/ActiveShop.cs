using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shop"))
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.parent.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shop"))
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.parent.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
