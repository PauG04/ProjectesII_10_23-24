using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTaskBar : MonoBehaviour
{
    private OpenApp openApp;
    private bool isOpen = false;
    private void Awake()
    {
        openApp = GetComponent<OpenApp>();
    }
    private void OnMouseDown()
    {
        if (openApp.GetIsOpen() && !isOpen)
        {
            openMinimizeWindow();
        }      
    }

    private void Update()
    {                 
        if (!openApp.GetIsOpen() && isOpen)
        {
            closeMinimizeWindos();
        }
    }

    private void openMinimizeWindow()
    {
        isOpen = true;
    }

    private void closeMinimizeWindos()
    {
        isOpen = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

}
