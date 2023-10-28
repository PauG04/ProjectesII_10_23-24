using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OpenApp : MonoBehaviour
{
    [SerializeField]
    private GameObject app;
    [SerializeField]
    private GameObject appIcon;
    private bool isOpen = false;
    [SerializeField]
    private SetTaskBarPosition setTaskBarPosition;
    private OrderTaskBar orderTaskBar;

    private void Awake()
    {
        orderTaskBar= GetComponent<OrderTaskBar>();
    }

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            appIcon.SetActive(true);
            app.SetActive(true);
            orderTaskBar.SetIcon();
            isOpen = true;
        }
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

    public void DesactiveApp()
    {
        appIcon.SetActive(false);
        app.SetActive(false);
        orderTaskBar.SetCloseIcon();
        isOpen= false;
    }


}

