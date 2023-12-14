using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class DragShaker : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;   
    private Vector2 position;

    [SerializeField] private GetWindow window;
    [SerializeField] private float maxAngle;
    [SerializeField] private bool hasToRotate;
    [SerializeField] private CloseShaker close; 

    private void Start()
    {
        targetJoint= GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
    }

    private void Update()
    {
        CalculatePosition();
        if(hasToRotate) 
            rb.SetRotation(Vector2.Dot(rb.velocity.normalized, Vector2.up) * rb.velocity.sqrMagnitude * maxAngle);
        if(window.GetWindows().GetCurrentState() == WindowsStateMachine.WindowState.Dragging)
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
        if(dragging)
            targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else
            targetJoint.target = (Vector2)window.GetWindows().transform.localPosition + position;

    }

    private void OnMouseDown()
    {
        if(close.GetClose())
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
