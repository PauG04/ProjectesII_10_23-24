using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class Click : MonoBehaviour
{
    [SerializeField] private MoveMouse moveMouse;
    [SerializeField] private GetWindow window;

    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;
    
    private Vector2 position;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
    }

    private void Update()
    {
        CalculatePosition();
        if (window.GetWindows().GetCurrentState() == WindowsStateMachine.WindowState.Dragging)
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
        if (moveMouse.GetDragging())
            targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else
            targetJoint.target = (Vector2)window.GetWindows().transform.localPosition + position;

    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }
}
