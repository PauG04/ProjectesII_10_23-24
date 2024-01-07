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
    private bool isOut;
    private bool pressing;
    private Transform parent;

    [SerializeField] private WindowsSetup window;
    [SerializeField] private Vector2 initPosition;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        anim = GetComponent<Animator>();
        transform.localPosition = initPosition;
        isOut = false;
        pressing = false;
        parent = transform.parent;
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

        if(Input.GetMouseButtonDown(1))
        {
            pressing = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            pressing = false;
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
           targetJoint.target = (Vector2)window.GetWindows().transform.localPosition + initPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceWindows") && !pressing)
        {
            Debug.Log("salimos");
            isOut = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IceWindows") && !pressing)
        {
            Debug.Log("entramos");
            isOut = false;
        }
    }


    private void OnMouseDown()
    {
        dragging = true;
        transform.SetParent(null);
    }

    private void OnMouseUp()
    {
        dragging = false;
        transform.SetParent(parent);
    }

    public bool GetDragging()
    {
        return dragging;
    }

    public void Animation(bool state)
    {
        anim.SetBool("isPressing", state);
    }

    public bool GetIsOut()
    {
        return isOut;
    }


}


