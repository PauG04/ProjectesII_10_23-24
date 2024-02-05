using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemsNew : MonoBehaviour
{
    [SerializeField] private GameObject bigObject;
    [SerializeField] private Collider2D bigTableCollider;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Vector3 normalScale = Vector3.one;

    private SpriteRenderer bigObjectRenderer;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (bigTableCollider.OverlapPoint(mousePosition))
        {
            bigObject.SetActive(true);
        }
        else
        {
            bigObject.SetActive(false);
            bigObjectRenderer.sprite = normalSprite;
            bigObject.transform.localScale = normalScale;
        }
    }
}
