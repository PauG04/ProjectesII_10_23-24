using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class Click : MonoBehaviour
{

    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;
    private DragWindows window;
    private Vector2 position;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        window = gameObject.transform.parent.gameObject.transform.parent.transform.GetChild(0).GetComponent<DragWindows>();
        position = transform.position;
    }

    private void Update()
    {
        CalculatePosition();
        if (window.GetIsDragging())
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void CalculatePosition()
    {
        if (dragging)
            targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else
            targetJoint.target = window.GetPosition() + position;

    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }
}
