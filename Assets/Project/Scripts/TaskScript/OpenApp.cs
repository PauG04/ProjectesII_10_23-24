using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
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
    [SerializeField]
    private Vector3 finalSize;
    private OrderTaskBar orderTaskBar;
    private float elapsedTime;
    [SerializeField]
    private float timeToAppear;
    [SerializeField]
    private Minimize minimize;

    private void Awake()
    {
        orderTaskBar = GetComponent<OrderTaskBar>();
    }

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            isOpen = true;
            appIcon.SetActive(true);
            app.SetActive(true);
            orderTaskBar.SetIcon();
            elapsedTime = 0;
        }

    }
    private void Update()
    {
        MaxMinLerp();
    }

    private void MaxMinLerp()
    {
        if (isOpen && app.transform.localScale.x < finalSize.x)
        {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime / timeToAppear;
            app.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), finalSize, time);
        }
        if (!isOpen && app.transform.localScale.x > 0)
        {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime / timeToAppear;
            app.transform.localScale = Vector3.Lerp(app.transform.localScale, new Vector3(0, 0, 0), time);
        }
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

    public void DesactiveApp()
    {
        elapsedTime = 0; 
        appIcon.SetActive(false);
        orderTaskBar.SetCloseIcon();
        isOpen= false;
    }

    public Vector3 GetFinalSize()
    {
        return finalSize;
    }


}

