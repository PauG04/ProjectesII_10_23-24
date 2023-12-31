using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBucketObject : MonoBehaviour
{
    private bool isDragging;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        isDragging = true;
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
        if(Input.GetMouseButtonUp(0))
        {
            rigidbody.gravityScale = 0.5f;
            rigidbody.mass = 0.5f;
            isDragging = false;
            rigidbody.isKinematic = false;
            
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        rigidbody.isKinematic = true;
        rigidbody.gravityScale = 0.5f;
        rigidbody.mass = 0.5f;
    }

}
