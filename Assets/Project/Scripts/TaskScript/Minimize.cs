using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : Button
{
    [SerializeField] private bool isMinimize;
    [SerializeField] private OpenApp openApp;
    [SerializeField] private GameObject parentObject;

    private void Update()
    {
        if(isMinimize)
        {
            parentObject.SetActive(false);
        }
        else
        {
            parentObject.SetActive(true);
        }
    }
    
    private void OnMouseDown()
    {
         isMinimize = true;           
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
        isMinimize = value;
    }

    public bool IsMinimize()
    {
        return isMinimize;
    }


    
}

