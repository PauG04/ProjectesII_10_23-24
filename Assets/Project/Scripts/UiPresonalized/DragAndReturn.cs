using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndReturn : MonoBehaviour
{
    private bool dragging = false;

    private Vector3 offset;

    [SerializeField] private Vector3 currentWindowPosition;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform parent;

    private void Start()
    {
        if (transform.parent.gameObject != null)
        {
            parent = transform.parent;
        }
        if (GetComponent<SpriteRenderer>() != null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        currentWindowPosition = transform.localPosition;
    }

    private void Update()
    {
        if (dragging)
        {
            CalculatePosition();
        }
    }

    private void CalculatePosition()
    {
        transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offset.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offset.y,
            -20
            );
        if (sprite != null)
        {
            sprite.sortingOrder = 10;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.SetParent(null);
        dragging = true;
    }

    private void OnMouseUp()
    {
        if (parent != null)
        {
            transform.SetParent(parent);
        }
        if (sprite != null)
        {
            sprite.sortingOrder = 1;
        }
        transform.localPosition = currentWindowPosition;

        dragging = false;

    }

    public bool GetDragging()
    {
        return dragging;
    }
}

