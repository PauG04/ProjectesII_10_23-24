using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShaker : MonoBehaviour
{
    private bool dragging = false;

    //[SerializeField] private SpriteRenderer sprite;
    //[SerializeField] private Transform parent;

    [SerializeField] private TargetJoint2D targetJoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxAngle = 0.0f;
    [SerializeField] private bool hasToRotate;

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
