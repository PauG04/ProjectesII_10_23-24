using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDecoration : MonoBehaviour
{
    private SpriteRenderer spriteRendererObejct;
    private GameObject currentObject;
    private bool isInTheCollision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Decoration"))
        {
            currentObject = collision.gameObject;
            isInTheCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Decoration"))
        {
            currentObject = null;
            isInTheCollision = false;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && isInTheCollision)
        {
            Rigidbody2D rigidbody = currentObject.GetComponent<Rigidbody2D>();
            SpriteRenderer spriteRenderer = currentObject.GetComponent<SpriteRenderer>();
            currentObject.transform.SetParent(transform);
            spriteRenderer.sortingOrder = 6;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.7f);
            currentObject.GetComponent<DragBucketObject>().enabled = false;
            rigidbody.isKinematic = true;
        }
    }
}
