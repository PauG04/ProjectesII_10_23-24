using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Windows;

public class Close : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private float speed = 10.0f;
    public OpenApp openApp;
    public GameObject icon;

    private ReOrderWindows orderWindows;

    private bool closeWindow;

    private void Awake()
    {
        orderWindows = mainWindow.GetComponent<ReOrderWindows>();
    }

    private void Update()
    {
        if (closeWindow)
        {
            mainWindow.transform.localScale = Vector3.Lerp(mainWindow.transform.localScale, Vector3.zero, Time.deltaTime * speed);

            if (icon != null)
            {
                openApp.DesactiveApp();
                Destroy(icon);
            }

            if (mainWindow.transform.localScale == Vector3.zero)
            {
                Destroy(mainWindow.gameObject);
                orderWindows.RemoveObjectFromList(mainWindow);
                openApp.SetClose();
            }
        }
    }
    private void OnMouseDown()
    {
        closeWindow = true;
    }
    public bool GetClose()
    {
        return closeWindow;
    }
}
