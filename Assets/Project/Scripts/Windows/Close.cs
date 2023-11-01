using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private float speed = 10.0f;
    public OpenApp openApp;
    public GameObject icon;

    private bool closeWindow;

    private void Update()
    {
        if (closeWindow)
        {
            mainWindow.transform.localScale = Vector3.Lerp(mainWindow.transform.localScale, Vector3.zero, Time.deltaTime * speed);
            openApp.DesactiveApp();

            if (mainWindow.transform.localScale.x == mainWindow.transform.localScale.x / 2)
            {
                Destroy(mainWindow.gameObject);
                Destroy(icon);
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
