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
    private bool pressButton;
    private Vector3 initialPosition;

    private void Update()
    {
        MaxMinLerp();
        MoveLerp();
        SetPressButton();
    }

    private void SetPressButton()
    {
        if (!openApp.GetIsOpen())
        {
            pressButton = false;
        }
    }
    private void OnMouseDown()
    {
        initialPosition = parentObject.transform.position;
        isMinimize = true;
        pressButton = false;
        times = 0;
        //openApp.DesactiveApp();
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
        if (!isMinimize && parentObject.transform.localScale.x < openApp.GetFinalSize().x && pressButton)
        {
            times += Time.deltaTime;
            float time = times / timeToAppear;
            parentObject.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), openApp.GetFinalSize(), time);
        }
        if (isMinimize && parentObject.transform.localScale.x > 0 && !pressButton)
        {
            times += Time.deltaTime;
            float time = times / timeToAppear;
            parentObject.transform.localScale = Vector3.Lerp(parentObject.transform.localScale, new Vector3(0, 0, 0), time);
        }
    }

    private void MoveLerp()
    {
        if (!isMinimize && parentObject.transform.position != initialPosition && pressButton)
        {
            times += Time.deltaTime;
            float time = times / timeToAppear;
            parentObject.transform.position = Vector3.Lerp(parentObject.transform.position, initialPosition, time);
        }

        if (isMinimize && parentObject.transform.localScale != icon.transform.position && !pressButton)
        {
            times += Time.deltaTime;
            float time = times / timeToAppear;
            parentObject.transform.position = Vector3.Lerp(initialPosition, icon.transform.position, time);
        }
    }
    public void SetIsMinimize(bool value)
    {
        pressButton = true;
        isMinimize = value;
        times = 0;
    }

    public bool IsMinimize()
    {
        return isMinimize;
    }
    
}

