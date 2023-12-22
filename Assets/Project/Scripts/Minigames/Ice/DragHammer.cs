using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class DragHammer : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;
    private Vector2 position;
    private Animator anim;

    [SerializeField] private WindowsSetup window;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        anim = GetComponent<Animator>();
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
        if (dragging)
        {
           targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
           targetJoint.target = (Vector2)window.GetWindows().transform.localPosition + position;
        }
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

    public void Animation(bool state)
    {
        anim.SetBool("isPressing", state);
    }
}


