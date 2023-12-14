using System.Collections;
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

    [Header("Animation Windows Speed")]
    [SerializeField] private float scaleSpeed = 3f;

    [Header("Taskbar Setup")]
    [SerializeField] private GameObject miniIconsPanel;

    private GameObject app;
    private ListOfWindows listOfWindows;

    private bool isOpen = false;
    private bool isCreated = false;
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
        if (!isOpen)
        {
            isOpen = true;

            if (!isCreated)
            {
                CreateWindows();
                CreateMiniIcons();
            }
        } 
        else
        {
            PutWindowInFront();
        }
    }
    private void CreateWindows()
    {
        WindowsStateMachine windows = windowsPrefab.GetComponent<WindowsStateMachine>();
        listOfWindows = windowsGroupManager.GetComponent<ListOfWindows>();

        windows.SetNode(node);
        windows.SetListOfWindows(listOfWindows);

        app = Instantiate(windowsPrefab, windowsGroupManager.transform);
        app.transform.localScale = Vector3.zero;

        listOfWindows.AddWindowInList(app);

        app.GetComponent<WindowsStateMachine>().ChangeState(WindowsStateMachine.WindowState.Creating);

        StartCoroutine(ScaleWindows());

        isCreated = true;
    }
    private void CreateMiniIcons()
    {
        GameObject newMiniIcon = new GameObject();

        newMiniIcon.name = gameObject.name + "_miniIcon";

        Image imageMiniIcon = newMiniIcon.AddComponent<Image>();
        RectTransform rectTransformMiniIcon = newMiniIcon.GetComponent<RectTransform>();

        imageMiniIcon.sprite = GetComponent<Image>().sprite;

        rectTransformMiniIcon.sizeDelta = new Vector2(50, 50);

        CreateButtonMiniIcon(newMiniIcon);

        newMiniIcon.transform.parent = miniIconsPanel.transform;
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

        button.onClick.AddListener(PutWindowInFront);
    }
    private void PutWindowInFront()
    {
        listOfWindows.MoveObjectInFront(app);
    }
    private IEnumerator ScaleWindows()
    {
        Vector3 initialScale = app.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            app.transform.localScale = Vector3.Lerp(initialScale, Vector3.one, elapsedTime);
            elapsedTime += Time.deltaTime * scaleSpeed;
            yield return null;
        }

        app.transform.localScale = Vector3.one;
    }
}
