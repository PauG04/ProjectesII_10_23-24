using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private bool isInWorkSpace;

    private Vector3 initScale;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    [Header("Parent Position")]
    [SerializeField] private GameObject parent;
    [SerializeField] private bool hasToBeDestroy;

    [Header("Lerp Variables")]
    [SerializeField] private float velocity;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        isInWorkSpace = false;
        initScale = transform.localScale;
    }

    private void Update()
    {
        CalculatePosition();
        MoveObjectToParent();
    }

    private void CalculatePosition()
    {
        if (dragging)
        {
           targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void MoveObjectToParent()
    {
        if (!dragging && !isInWorkSpace)
        {
            transform.position = Vector2.Lerp(transform.position, parent.transform.position, Time.deltaTime * velocity);
        }
        if(transform.position.x > parent.transform.position.x - 0.1 && transform.position.x < parent.transform.position.x + 0.1 && hasToBeDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && dragging)
        {
            isInWorkSpace = false;
            transform.localScale = initScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && dragging)
        {
            isInWorkSpace = true;
            transform.localScale *= increaseScale;
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
}


