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

    private bool isRotate;
    private bool firstRotateLerp;
    private bool secondRotateLerp;
    private Quaternion initRotation;

    private bool detectCollision;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    [Header("Parent Position")]
    [SerializeField] private GameObject parent;
    [SerializeField] private bool hasToBeDestroy;

    [Header("Lerp Variables")]
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private float velocityZ;

    [Header("2nd Sprite")]
    [SerializeField] private Sprite workSpaceSprite;
    private Sprite normalSprite;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        
        initScale = transform.localScale;

        isInWorkSpace = false;
        firstLerp = false;
        secondLerp = false;
        secondRotateLerp = false;
        firstRotateLerp = false;

        detectCollision = true;

        initRotation = transform.localRotation;

        normalSprite = GetComponent<SpriteRenderer>().sprite;

        if(transform.rotation.z != 0)
        {
            isRotate = true;
        }
    }

    private void Update()
    {
        CalculatePosition();
        MoveObjectToParent();
        if(isRotate)
        {
            RotateObject();
        }    
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            firstLerp = true;
        }
    }

    private void CalculatePosition()
    {
        if (dragging)
        {
           targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void RotateObject()
    {
        if (firstRotateLerp && !secondRotateLerp)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 0, 1), Time.deltaTime * velocityZ);
        }
        if(transform.rotation.z < 0.0001 && !secondRotateLerp)
        {
            firstRotateLerp= false;
        }
    }

    private void MoveObjectToParent()
    {
        if (!dragging && !isInWorkSpace)
        {
            if(isRotate)
            {
                if (secondRotateLerp)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, initRotation, Time.deltaTime * velocityZ);
                }
                if (transform.rotation.z == initRotation.z && secondRotateLerp)
                {
                    secondRotateLerp = false;
                    firstLerp = true;
                }
            }
            if (firstLerp)
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
        if (collision.CompareTag("WorkSpace") && isInWorkSpace && detectCollision)
        {
            if (workSpaceSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = normalSprite;
            }
            isInWorkSpace = false;
            transform.localScale = initScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && !isInWorkSpace && detectCollision)
        {
            if(workSpaceSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = workSpaceSprite;
            }
            isInWorkSpace = true;
            transform.localScale *= increaseScale;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
        firstLerp = false;
        secondLerp = false;
        secondRotateLerp = false;
        firstRotateLerp = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        if(isRotate)
        {
            secondRotateLerp = true;
        }
        else
        {
            firstLerp = true;
        }     
    }

    public void SetIsDragging(bool state)
    {
        dragging = state;
    }

    public bool GetIsDraggin()
    {
        return dragging;
    }

    public bool GetIsInWorkSpace()
    {
        return isInWorkSpace;
    }

    public void SetDetectCollision(bool state)
    {
        detectCollision = state;
    }
}


