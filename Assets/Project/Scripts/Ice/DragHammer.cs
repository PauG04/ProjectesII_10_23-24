using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Windows;

public class DragHammer : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;
    private Vector2 position;
    private DragWindows window;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        window = gameObject.transform.parent.gameObject.transform.parent.GetChild(0).GetComponent<DragWindows>();
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

    public bool GetDragging()
    {
        return dragging;
    }
}


