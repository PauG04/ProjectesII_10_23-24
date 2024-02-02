using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFridge : MonoBehaviour
{
    [Header("Velocities")]
    [SerializeField] private float velocityRotation;
    [SerializeField] private float velocityPosition;

    [Header("DragItems")]
    [SerializeField] private DragItem[] items;    

    private Quaternion initRotation;
    private Vector3 initPosition;

    private bool isOpen;

    private bool isRotationLerp;
    private bool isPositionLerp;

    private float width;

    private void Start()
    {
        isOpen = false;

        initRotation = transform.localRotation;
        initPosition = transform.localPosition;

        isRotationLerp = false;
        isPositionLerp = false;

        width = GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
    }

    private void Update()
    {
        if (isRotationLerp)
        {
           RotationLerp();
        }
        if(isPositionLerp) 
        {
            PositionLerp();
        }

        if(isOpen) 
        { 
            for(int i = 0; i<items.Length; i++)
            {
                items[i].SetCanBeCatch(false);
            }
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetCanBeCatch(true);
            }
        }

    }

    private void RotationLerp()
    {      
        if(isOpen)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, new Quaternion(0,1,0,0), Time.deltaTime * velocityRotation);
        }
        if(isOpen && transform.localRotation.y >= 0.999)
        {
            isRotationLerp = false;
        }

        if (!isOpen)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, initRotation, Time.deltaTime * velocityRotation);
        }
        if (!isOpen && transform.localRotation.y <= initRotation.y + 0.001f)
        {
            isRotationLerp = false;
        }

    }

    private void PositionLerp()
    {
        if (isOpen)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.x = Mathf.Lerp(transform.localPosition.x, initPosition.x + width, Time.deltaTime * velocityPosition);

            newPosition.z = -1;
            transform.localPosition = newPosition;
            
        }
        if (isOpen && transform.localPosition.x >= initPosition.x + width - 0.005)
        {
            isPositionLerp = false;
        }

        if (!isOpen)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.x = Mathf.Lerp(transform.localPosition.x, initPosition.x, Time.deltaTime * velocityPosition);

            newPosition.z = -1;
            transform.localPosition = newPosition;
        }
        if (!isOpen && transform.localPosition.x <= initPosition.x + 0.005)
        {
            isPositionLerp = false;
        }
    }

    private void OnMouseDown()
    {
        if (!isRotationLerp && !isPositionLerp)
        {
            isOpen = !isOpen;
            isRotationLerp = true;
            isPositionLerp = true;
        }  
    }

}
