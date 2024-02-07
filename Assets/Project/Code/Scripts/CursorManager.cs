using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance { get; private set; }

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private bool isMouseUp;

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
        if (Input.GetMouseButtonDown(0))
            isMouseUp = false;
        else if(Input.GetMouseButtonUp(0))
            isMouseUp = true;
    }

    public bool IsMouseUp()
    {
        return isMouseUp;
    }
}
