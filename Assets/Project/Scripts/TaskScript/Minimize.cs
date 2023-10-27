using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : Button
{
    //[SerializeField] private bool isMinimize;
    [SerializeField] private OpenApp openApp;
    [SerializeField] private GameObject parentObject;

    private void Update()
    {
        if (SimulateButton(gameObject.name))
        {
            Debug.Log("Minimized");
            //isMinimize = false;
            parentObject.SetActive(false);
            /*
            if (isMinimize)
            {

            }
            else if (!isMinimize)
            {

            }
            */
        }
    }
    /*
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
            isMinimize = true;           
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            openApp.DesactiveApp();
    }
    
    public void SetIsMinimize(bool value)
    {
        isMinimize = value;
    }
    */
}

