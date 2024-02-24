using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance { get; private set; }

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private bool isMouseUp;
    private Vector2 cursorPosition;

    [SerializeField] private TextMeshPro itemName;
    [SerializeField] private GameObject box;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    private void Update()
    {
        cursorPosition = GetMouseWorldPosition();

        box.transform.position = cursorPosition;
        box.transform.position = new Vector2(cursorPosition.x + 0.5f, cursorPosition.y + 0.1f);

        if (Input.GetMouseButtonDown(0))
            isMouseUp = false;
        else if(Input.GetMouseButtonUp(0))
            isMouseUp = true;
    }

    public bool IsMouseUp()
    {
        return isMouseUp;
    }
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public Vector3 GetCursorPosition()
    {
        return cursorPosition;
    }

    public GameObject GetBox()
    {
        return box;
    }

    public TextMeshPro GetItemName()
    {
        return itemName;
    }
}
