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

    private void Start()
    {
        targetJoint= GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        //if (transform.parent != null)
        //{
        //    parent = transform.parent;
        //}
        //if (GetComponent<SpriteRenderer>() != null)
        //{
        //    sprite = GetComponent<SpriteRenderer>();
        //}
    }

    private void Update()
    {
        CalculatePosition();
        rb.SetRotation(Vector2.Dot(rb.velocity.normalized, Vector2.up) * rb.velocity.sqrMagnitude * maxAngle);
    }

    private void CalculatePosition()
    {
        // targetJoint.target = dragging ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : Vector2.zero;
        if(dragging)
            targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (sprite != null)
        //{
        //    sprite.sortingOrder = 10;
        //}
    }

    private void OnMouseDown()
    {
        //transform.SetParent(null);
        dragging = true;
    }

    private void OnMouseUp()
    {
        //if (parent != null)
        //{
        //    transform.SetParent(parent);
        //}
        //if (sprite != null)
        //{
        //    sprite.sortingOrder = 1;
        //}

        dragging = false;

    }

    public bool GetDragging()
    {
        return dragging;
    }
}
