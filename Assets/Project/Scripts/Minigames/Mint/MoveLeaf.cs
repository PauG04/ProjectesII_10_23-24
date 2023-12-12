using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class MoveLeaf : MonoBehaviour
{
    [SerializeField] private float maxVelocity;
    [SerializeField] private float force;
    [SerializeField] private GameObject brokenLeaf;

    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rb;
    private Vector2 position;
    private WindowsStateMachine window;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        window = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.GetComponent<WindowsStateMachine>();
        position = transform.position;
    }

    private void Update()
    {
        CalculatePosition();
        DestroyLeaf();
        if (window.GetCurrentState() == WindowsStateMachine.WindowState.Dragging)
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
    }

    private void DestroyLeaf()
    {
        if(rb.velocity.magnitude >= maxVelocity && dragging)
        {
            Slice(transform.position, transform.position);
        }

    }

    private void OnMouseDown()
    {
        dragging = true;
        targetJoint.enabled = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        targetJoint.enabled = false;
    }

    void Slice(Vector3 direction, Vector3 pos)
    {
        GameObject newLeaf = Instantiate(brokenLeaf, transform);

        foreach (Transform slice in newLeaf.transform)
        {
            Rigidbody2D rbLeaf = slice.GetComponent<Rigidbody2D>();
            Vector3 dir = slice.transform.position - pos;
            rbLeaf.AddForceAtPosition(dir.normalized * Random.Range(-force, force), pos, ForceMode2D.Impulse);
        }
        newLeaf.transform.parent = transform.parent;
        newLeaf.transform.localScale = gameObject.transform.localScale;

        Destroy(gameObject);
    }

    public bool GetDragging()
    {
        return dragging;
    }
}
