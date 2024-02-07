using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool dragging = false;
    private TargetJoint2D targetJoint;
    private bool isInWorkSpace = false;

    private Vector3 initScale;

    private bool horizontalLerp = false;
    private bool verticalLerp = false;

    private bool isRotating;
    private bool firstRotateLerp = false;
    private bool secondRotateLerp = false;
    private Quaternion initRotation;

    private bool detectCollision;

    private Vector3 initPosition;

    private bool isLerping = false;
    private bool canBeCatch;

    [Header("Tranform Vairables")]
    [SerializeField] private float increaseScale;

    [Header("Parent Position")] 
    [SerializeField] private bool hasToBeDestroy;

    [Header("Lerp Variables")]
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private float rotationVelocityZ;

    [Header("2nd Sprite")]
    [SerializeField] private Sprite workSpaceSprite;
    private Sprite normalSprite;

    private void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        
        initScale = transform.localScale;
        initPosition = transform.localPosition;

        detectCollision = true;

        canBeCatch = true;

        initRotation = transform.localRotation;

        if(GetComponent<SpriteRenderer>() != null )
            normalSprite = GetComponent<SpriteRenderer>().sprite;

        if(transform.localRotation.z != 0)
        {
            isRotating = true;
        }
    }

    private void Update()
    {
        CalculatePosition();
        MoveObjectToParent();
        if(isRotating)
        {
            RotateObject();
        }    
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            horizontalLerp = true;
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
            isLerping = true;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, new Quaternion(0, 0, 0, 1), Time.deltaTime * rotationVelocityZ);
        }
        if(transform.localRotation.z < 0.0001 && !secondRotateLerp)
        {
            isLerping = false;
            firstRotateLerp = false;
        }
    }

    private void MoveObjectToParent()
    {
        if (!dragging && !isInWorkSpace)
        {
            if(isRotating)
            {
                if (secondRotateLerp)
                {
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, initRotation, Time.deltaTime * rotationVelocityZ);
                }
                if (transform.localRotation.z >= initRotation.z - 0.01 && secondRotateLerp)
                {
                    transform.localRotation = initRotation;
                    secondRotateLerp = false;
                    horizontalLerp = true;
                }
            }
            if (horizontalLerp)
            {

                Vector3 newPosition = transform.localPosition;
                newPosition.x = Mathf.Lerp(transform.localPosition.x, initPosition.x, Time.deltaTime * velocityX);

                transform.localPosition = newPosition;
            }
            if (transform.localPosition.x > initPosition.x - 0.002 && transform.localPosition.x < initPosition.x + 0.002)
            {
                horizontalLerp = false;
                verticalLerp = true;
            }

            if(verticalLerp)
            {
                Vector3 newPosition = transform.localPosition;
                newPosition.y = Mathf.Lerp(transform.localPosition.y, initPosition.y, Time.deltaTime * velocityY);

                transform.localPosition = newPosition;
            }
            if (transform.localPosition.y > initPosition.y - 0.002 && transform.localPosition.y < initPosition.y + 0.002)
            {
                verticalLerp = false;
                isLerping = false;
                if (hasToBeDestroy)
                {
                    Destroy(gameObject);
                }
            }
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && isInWorkSpace && detectCollision && !isLerping)
        {
            if (workSpaceSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = normalSprite;
            }
            isInWorkSpace = false;
            //transform.localScale = initScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && !isInWorkSpace && detectCollision && !isLerping)
        {
            if(workSpaceSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = workSpaceSprite;
            }
            isInWorkSpace = true;
            //transform.localScale *= increaseScale;
        }
    }

    private void OnMouseDown()
    {
        if(canBeCatch)
        {
            dragging = true;

            horizontalLerp = false;
            verticalLerp = false;
            secondRotateLerp = false;
            firstRotateLerp = true;

            isLerping = false;
        }
        
    }

    private void OnMouseUp()
    {
        dragging = false;      
        if (isRotating)
        {
            secondRotateLerp = true;
        }
        else
        {
            horizontalLerp = true;
        } 
        
        if(!isInWorkSpace)
        {
            isLerping = true;
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

    public void SetCanBeCatch(bool state)
    {
       canBeCatch = state;
    }
}


