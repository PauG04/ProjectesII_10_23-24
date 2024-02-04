using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFridge : MonoBehaviour
{
    [Header("Velocities")]
    [SerializeField] private float velocityPosition;

    [Header("DragItems")]
    [SerializeField] private DragItem[] items;

    [Header("Widht Different")]
    [SerializeField] private float widthDifferent;

    private Vector3 initPosition;

    private bool isOpen;

    private bool isPositionLerp;

    private float width;

    private void Start()
    {
        isOpen = false;

        initPosition = transform.localPosition;

        isPositionLerp = false;

        width = GetComponent<SpriteRenderer>().bounds.size.x - widthDifferent;
    }

    private void Update()
    {
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
        if ( !isPositionLerp)
        {
            isOpen = !isOpen;
            isPositionLerp = true;
        }  
    }

}
