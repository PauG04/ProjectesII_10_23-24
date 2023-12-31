﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Windows;

public class DesktopApp : MonoBehaviour
{
    [Header("Windows Setup")]
    [SerializeField] private GameObject windowsGroupManager;
	[SerializeField] private GameObject windowsPrefab;
	[SerializeField] private WindowNode node;
    
	[Header("Windows Setup Canvas")]
	[SerializeField] private GameObject windowsPrefabCanvas;
	[SerializeField] private bool isCanvas;

    [Header("Animation Windows Speed")]
    [SerializeField] private float scaleSpeed = 3f;

    [Header("Taskbar Setup")]
    [SerializeField] private GameObject miniIconsPanel;

    private GameObject app;
    private ListOfWindows listOfWindows;

    private bool isOpen = false;
	private bool isCreated = false;
    
	private GameObject newMiniIcon;
    
	private Vector3 correctSize;
    
    private void Awake()
    {
	    windowsGroupManager = GameObject.Find("WindowsGroup");
	    miniIconsPanel = GameObject.Find("MiniIconsPanel");
    }
    private void OnMouseDown()
    {
        OpenApp();
    }
    public void OpenApp()
	{
		if (!isCreated)
		{
			isOpen = true;

			CreateMiniIcons();
			CreateWindows();
		}
		AppControl();
	}
    
    private void CreateWindows()
	{
		WindowsStateMachine windows;
		
		if (isCanvas)
		{
			windows = windowsPrefabCanvas.GetComponent<WindowsStateMachine>();
		}
		else 
		{
			windows = windowsPrefab.GetComponent<WindowsStateMachine>();
		}
        listOfWindows = windowsGroupManager.GetComponent<ListOfWindows>();

		windows.SetIsCanvas(isCanvas);
        windows.SetNode(node);
        windows.SetListOfWindows(listOfWindows);
	    windows.SetMiniIcon(newMiniIcon);
	    windows.SetApp(this);

		if (isCanvas)
		{
			app = Instantiate(windowsPrefabCanvas, windowsGroupManager.transform);
			correctSize = windowsPrefabCanvas.GetComponent<RectTransform>().localScale;
			app.GetComponent<RectTransform>().localScale = Vector3.zero;
		}
		else 
		{
			app = Instantiate(windowsPrefab, windowsGroupManager.transform);
			correctSize = app.transform.localScale;
			app.transform.localScale = Vector3.zero;
		}

        listOfWindows.AddWindowInList(app);

        app.GetComponent<WindowsStateMachine>().ChangeState(WindowsStateMachine.WindowState.Creating);

		StartCoroutine(ScaleWindows(correctSize));
	
        isCreated = true;
    }
    private void CreateMiniIcons()
    {
        newMiniIcon = new GameObject();

        newMiniIcon.name = gameObject.name + "_miniIcon";

        Image imageMiniIcon = newMiniIcon.AddComponent<Image>();
        RectTransform rectTransformMiniIcon = newMiniIcon.GetComponent<RectTransform>();

        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            imageMiniIcon.sprite = GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            imageMiniIcon.sprite = GetComponent<Image>().sprite;
        }

        rectTransformMiniIcon.sizeDelta = new Vector2(50, 50);
        

        CreateButtonMiniIcon(newMiniIcon);

	    newMiniIcon.transform.SetParent(miniIconsPanel.transform);
        newMiniIcon.transform.localScale = Vector3.one;
    }

    private void CreateButtonMiniIcon(GameObject parent)
    {
        GameObject buttonMiniIcon = new GameObject();
        buttonMiniIcon.name = "Button";
        buttonMiniIcon.transform.parent = parent.transform;

        RectTransform rectTransformMiniIcon = buttonMiniIcon.AddComponent<RectTransform>();
        rectTransformMiniIcon.sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;

        buttonMiniIcon.AddComponent<Image>();

        Button button = buttonMiniIcon.AddComponent<Button>();

        ColorBlock buttonColors = button.colors;

        Color noAlpha = new Color(0f, 0f, 0f, 1f);
        Color halfAlpha = new Color(0f, 0f, 0f, 0.5f);

        buttonColors.normalColor -= noAlpha;
        buttonColors.highlightedColor -= halfAlpha;
        buttonColors.pressedColor -= halfAlpha;
        buttonColors.selectedColor -= noAlpha;
        buttonColors.disabledColor -= halfAlpha;

        button.colors = buttonColors;

        button.onClick.AddListener(AppControl);
    }
	public void AppControl()
	{
		if (!isOpen)
		{
			Minimize();
		} 
		else
		{
			PutWindowInFront();
		}
	}
    private void PutWindowInFront()
    {
        listOfWindows.MoveObjectInFront(app);
    }
	public void Minimize()
	{
		if(isOpen)
		{
			StartCoroutine(ScaleWindows(Vector3.zero));
		}
		else
		{
			PutWindowInFront();
			StartCoroutine(ScaleWindows(correctSize));
		}
				
		isOpen = !isOpen;
	}
	private IEnumerator ScaleWindows(Vector3 objectiveScale)
    {
        Vector3 initialScale = app.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            app.transform.localScale = Vector3.Lerp(initialScale, objectiveScale, elapsedTime);
            elapsedTime += Time.deltaTime * scaleSpeed;
	        yield return null;
        }

        app.transform.localScale = objectiveScale;
    }
	public void ResetApp()
	{
		app = null;
		listOfWindows = null;
		newMiniIcon = null;
		
		isCreated = false;
		isOpen = false;
	}
	public void SetIsCreated(bool isCreated)
	{
		this.isCreated = isCreated;
	}
	public void SetIsOpen(bool isOpen)
	{
		this.isOpen = isOpen;
	}

	public bool GetIsCreated()
	{
		return isCreated;
	}
}
