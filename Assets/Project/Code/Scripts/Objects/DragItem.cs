using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private bool isInWorkSpace;

    private Vector3 initScale;

    private bool firstLerp;
    private bool secondLerp;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    [Header("Parent Position")]
    [SerializeField] private GameObject parent;
    [SerializeField] private bool hasToBeDestroy;

    [Header("Lerp Variables")]
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        isInWorkSpace = false;
        initScale = transform.localScale;
        firstLerp = false;
        secondLerp = false;
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
            if(firstLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.x = Mathf.Lerp(transform.position.x, parent.transform.position.x, Time.deltaTime * velocityX);

                transform.position = newPosition;
            }
            if (transform.position.x > parent.transform.position.x - 0.02 && transform.position.x < parent.transform.position.x + 0.02)
            {
                firstLerp = false;
                secondLerp = true;
            }
            if(secondLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.y = Mathf.Lerp(transform.position.y, parent.transform.position.y, Time.deltaTime * velocityY);

                transform.position = newPosition;
            }
            if (transform.position.y > parent.transform.position.y - 0.02 && transform.position.y < parent.transform.position.y + 0.02)
            {
                secondLerp = false;
                if(hasToBeDestroy)
                {
                    Destroy(gameObject);
                }
            }
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
        firstLerp = false;
        secondLerp = false;
    }

    private void OnMouseUp()
    {
        dragging = false;
        firstLerp = true;
    }
}


