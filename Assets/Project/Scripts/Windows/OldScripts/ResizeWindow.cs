using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeWindow : MonoBehaviour
{
    [SerializeField] private Transform windowControl;

    private GameObject parent;
    private GameObject prefab;

    [Header("Offset")]
    [SerializeField] private float offsetWidth = 0.02f;
    [SerializeField] private float offsetHeight = 0.08f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parent = transform.parent.gameObject;
        prefab = parent.GetComponent<CreatePrefabInsideWindow>().GetPrefab();

        spriteRenderer.drawMode = SpriteDrawMode.Tiled;

        ResizeWindowToPrefab();
        SetWindowControlPosition();
    }

    void ResizeWindowToPrefab()
    {
        prefab.transform.position = new Vector3(prefab.transform.position.x, prefab.transform.position.y, parent.transform.position.z);

        parent.transform.position = prefab.transform.position;

        Vector2 newWindowSize = new Vector2(
            prefab.transform.localScale.x + offsetWidth, 
            prefab.transform.localScale.y + offsetHeight
        );

        spriteRenderer.size = newWindowSize;
    }

    void SetWindowControlPosition()
    {
        float correctXPos = 0.01f;
        float correctYPos = 0.03f;

        Vector2 newWindowControlPos = new Vector2(
            (spriteRenderer.size.x / 2) - correctXPos, 
            (spriteRenderer.size.y / 2) + correctYPos
        );

        windowControl.position = newWindowControlPos;
    }
}
