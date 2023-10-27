using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndReturn : MonoBehaviour
{
    private bool dragging = false;

    private Vector3 offset;

    [SerializeField]
    private Vector3 initialOffset;

    [SerializeField]
    private Transform currentWindowPosition;

    [SerializeField]
    private Rigidbody2D rb;

    private void Awake()
    {
        transform.position = currentWindowPosition.position + initialOffset;
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
        rb.MovePosition(currentWindowPosition.position + initialOffset);
        dragging = false;
    }
}
