using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBucketObject : MonoBehaviour
{
    private bool isDragging;
    private Rigidbody2D rb;
    private void Start()
    {
        isDragging = true;
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
        if(Input.GetMouseButtonUp(0))
        {
            rb.gravityScale = 0.5f;
            rb.mass = 0.5f;
            isDragging = false;
            rb.isKinematic = false;
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            transform.Rotate(0.0f, 0.0f, 90.0f);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        rb.isKinematic = true;
        rb.gravityScale = 0.5f;
        rb.mass = 0.5f;
    }

}
