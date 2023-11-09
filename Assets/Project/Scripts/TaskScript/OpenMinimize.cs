using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows;

public class OpenMinimize : MonoBehaviour
{
    public GameObject minimizeBigIcon;
    public Minimize minimizeWindow;

    private void OnMouseDown()
    {
        if (minimizeWindow.IsMinimize())
        {
            minimizeBigIcon.SetActive(true);
            minimizeWindow.SetIsMinimize();
        }
    }
}
