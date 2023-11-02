using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Windows;

public class Minimize : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Close close;
    [SerializeField] private ReOrderWindows reOrderWindows;

    [SerializeField] private float timeToAppear;

    public OpenApp openApp;
    public GameObject icon;

    private bool isMinimize;
    private float times = 0;
    private Vector3 initialPosition;
    private bool isMoving;

    private void FixedUpdate()
    {
        if (openApp != null)
        {
            openApp.isOpen = !isMinimize;
            MoveLerp();
            MaxMinLerp();
        }
    }
    private void OnMouseDown()
    {
        isMoving = true;
        isMinimize = true;
        times = 0;
    }
    private void MaxMinLerp()
    {
        if(!close.GetClose())
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
                parentObject.transform.localScale = Vector3.Lerp(parentObject.transform.localScale, Vector3.zero, time);
            }
        }
    }
    private void MoveLerp()
    { 
        if (isMoving)
        {

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
        reOrderWindows.OrderGroupLayer(parentObject);
        isMoving = true;
        isMinimize = false;
        times = 0;
    }
    public bool IsMinimize()
    {
        return isMinimize;
    }
}