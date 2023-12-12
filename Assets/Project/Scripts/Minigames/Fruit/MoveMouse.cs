using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class MoveMouse : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DetectMouse();
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void DetectMouse()
    {
        if(Input.GetMouseButton(0)) 
        {
            dragging = true;
        }
        else
        {
            dragging = false;
        }
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }

    public bool GetDragging()
    {
        return dragging;
    }
}
