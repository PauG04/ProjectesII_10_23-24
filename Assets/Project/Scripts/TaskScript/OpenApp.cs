using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Windows;

public class OpenApp : MonoBehaviour
{
    [Header("App values ")]
    [SerializeField] private GameObject appIcon;
    [SerializeField] private SetTaskBarPosition setTaskBarPosition;
    [SerializeField] private float timeToAppear;

    private OrderTaskBar orderTaskBar;
    private GameObject app;
    private Vector3 finalSize = new Vector3(1, 1, 1);
    private bool isOpen;
    private float elapsedTime;

    #region Windows Creation
    [Header("Windows values")]
    [SerializeField] private WindowNode node;
    [SerializeField] private GameObject windowPrefab;

    private GameObject windowGroup;
    private bool isCreated = false;
    #endregion

    private void Awake()
    {
        orderTaskBar = GetComponent<OrderTaskBar>();
        orderTaskBar.enabled = false;

        // Encontrar otra manera de hacer esto
        windowGroup = GameObject.Find("WindowGroup");
    }

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            isOpen = true;

            if (!isCreated)
            {
                CreateWindows();
            }

            orderTaskBar.SetIcon();
            elapsedTime = 0;
        }
    }
    private void Update()
    {
        if (isCreated) 
        { 
            MaxMinLerp();
        }
    }
    private void MaxMinLerp()
    {
        if (app != null)
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
    }
    private void CreateWindows()
    {
        windowPrefab.GetComponent<WindowCreation>().node = node;

        app = Instantiate(windowPrefab);
        app.transform.parent = windowGroup.transform;

        app.GetComponent<WindowCreation>().UpdateWindow();
        app.transform.transform.localScale = Vector3.zero;

        CreateMiniIcon(app.GetComponent<WindowCreation>());

        orderTaskBar.enabled = true;
        isCreated = true;
    }

    private void CreateMiniIcon(WindowCreation window)
    {
        GameObject obj = new GameObject();

        obj.name = gameObject.name + "_icon";

        BoxCollider2D boxCollider2DObj = obj.AddComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderObj = obj.AddComponent<SpriteRenderer>();
        OpenMinimize openMinimizeObj = obj.AddComponent<OpenMinimize>();

        boxCollider2DObj.isTrigger = true;
        boxCollider2DObj.size = new Vector2(0.3f, 0.3f);

        spriteRenderObj.sprite = GetComponent<SpriteRenderer>().sprite;
        spriteRenderObj.sortingOrder = 2;

        openMinimizeObj.minimizeBigIcon = app;
        openMinimizeObj.minimizeWindow = window.GetMinimize();

        obj.transform.parent = transform;
        obj.transform.localScale /= 1.5f;

        window.GetMinimize().icon = obj;
        window.GetMinimize().openApp = this;

        orderTaskBar.icon = obj;
    }
    public void DesactiveApp()
    {
        //elapsedTime = 0; 
        //orderTaskBar.SetCloseIcon();
        //isOpen = false;
    }
    public bool GetIsOpen()
    {
        return isOpen;
    }
    public Vector3 GetFinalSize()
    {
        return finalSize;
    }
}

