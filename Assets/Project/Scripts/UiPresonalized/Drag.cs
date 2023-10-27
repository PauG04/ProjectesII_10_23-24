using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    private bool dragging = false;
    
    private Vector3 offset;
    
    [SerializeField]
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (dragging)
        {
            CalculatePosition();
        }
    }
    
    private void CalculatePosition()
    {
        rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset);
    }
    
    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}
