using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShaker : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;

    [SerializeField] private float maxAngle;
    [SerializeField] private bool hasToRotate;
    [SerializeField] private CloseShaker close;

    private void Start()
    {
        targetJoint= GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CalculatePosition();
        if(hasToRotate) 
            rb.SetRotation(Vector2.Dot(rb.velocity.normalized, Vector2.up) * rb.velocity.sqrMagnitude * maxAngle);
    }

    private void CalculatePosition()
    {
        if(dragging)
            targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else
            targetJoint.target = new Vector2(0,0);
        
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
