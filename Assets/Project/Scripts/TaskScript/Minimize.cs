using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Minimize : Button
{
    [SerializeField] private bool isMinimize;
    private bool pressButton;
    [SerializeField] private OpenApp openApp;
    [SerializeField] private GameObject parentObject;
    private float times = 0;
    [SerializeField]
    private float timeToAppear;
    [SerializeField]
    private GameObject icon;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = parentObject.transform.position;
    }

    private void Update()
    {
        MaxMinLerp();
        MoveLerp();
        SetPressButton();
    }

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

    private void SetPressButton()
    {
        if (!openApp.GetIsOpen())
        {
            pressButton = false;
        }
    }

    private void OnMouseDown()
    {
        isMinimize = true;
        pressButton = false;
        times = 0;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            openApp.DesactiveApp();
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

