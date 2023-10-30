using System.Collections;
using System.Collections.Generic;
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
    private bool isOpen = false;
    private float elapsedTime;

    #region Windows Creation
    [Header("Windows values")]
    [SerializeField] private WindowNode node;
    [SerializeField] private GameObject windowPrefab;

    private GameObject windowGroup;
    public bool isCreated { private get; set; }
    #endregion

    private void Awake()
    {
        orderTaskBar = GetComponent<OrderTaskBar>();
        orderTaskBar.enabled = false;
        windowGroup = GameObject.Find("WindowGroup");
    }

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            isOpen = true;
            appIcon.SetActive(true);

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
    private void CreateWindows()
    {
        windowPrefab.GetComponent<WindowCreation>().node = node;

        app = Instantiate(windowPrefab);
        app.transform.parent = windowGroup.transform;

        app.GetComponent<WindowCreation>().UpdateWindow();
        app.transform.transform.localScale = Vector3.zero;

        CreateMiniIcon();

        orderTaskBar.enabled = true;
        isCreated = true;
    }
    private void CreateMiniIcon()
    {
        GameObject obj = new GameObject();

        obj.name = gameObject.name + "_MiniIcon";

        BoxCollider2D boxCollider2DObj = obj.AddComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderObj = obj.AddComponent<SpriteRenderer>();
        OpenMinimize openMinimizeObj = obj.AddComponent<OpenMinimize>();

        boxCollider2DObj.isTrigger = true;
        openMinimizeObj.minimize = app;

        spriteRenderObj.sprite = GetComponent<SpriteRenderer>().sprite;
        spriteRenderObj.sortingOrder = 1;

        GameObject miniApp = Instantiate(obj);

        orderTaskBar.icon = miniApp;

        miniApp.transform.parent = transform;
        miniApp.transform.localScale /= 1.5f;
    }
}

