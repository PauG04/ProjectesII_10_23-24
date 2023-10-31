using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Minimize : MonoBehaviour
{
    [SerializeField] private float timeToAppear;
    [SerializeField] private GameObject parentObject;
    public OpenApp openApp;
    public GameObject icon;

    private bool isMinimize;
    private float times = 0;
    private Vector3 initialPosition;
    private bool isMoving;

    private void Update()
    {
        openApp.isOpen = !isMinimize;
        MoveLerp();
        MaxMinLerp();
    }
    private void OnMouseDown()
    {
        isMoving = true;
        isMinimize = true;
        times = 0;
    }
    /*
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            openApp.DesactiveApp();
        }          
    }
    */
    private void MaxMinLerp()
    {
        if (!isMinimize && parentObject.transform.localScale.x < openApp.GetFinalSize().x)
        {
            times += Time.deltaTime;
            float time = times / timeToAppear;
            parentObject.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), openApp.GetFinalSize(), time);
        }
        if (isMinimize && parentObject.transform.localScale.x > 0)
        {
            times += Time.deltaTime;
            float time = times / timeToAppear;
            parentObject.transform.localScale = Vector3.Lerp(parentObject.transform.localScale, new Vector3(0, 0, 0), time);
        }
    }

    private void MoveLerp()
    { 
        if (isMoving)
        {
            isMoving = false;

            if (!isMinimize && parentObject.transform.position != initialPosition)
            {
                times += Time.deltaTime;
                float time = times / timeToAppear;
                parentObject.transform.position = Vector3.Lerp(parentObject.transform.position, initialPosition, time);
            }

            if (isMinimize && parentObject.transform.localScale != icon.transform.position)
            {
                times += Time.deltaTime;
                float time = times / timeToAppear;
                parentObject.transform.position = Vector3.Lerp(initialPosition, icon.transform.position, time);
            }
        }
        else
        {
            initialPosition = parentObject.transform.position;
        }
    }
    public void SetIsMinimize()
    {
        isMoving = true;
        isMinimize = false;
        times = 0;
    }

    public bool IsMinimize()
    {
        return isMinimize;
    }
    
}

