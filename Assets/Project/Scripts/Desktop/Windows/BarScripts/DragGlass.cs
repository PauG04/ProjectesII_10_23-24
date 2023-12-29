using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragGlass : MonoBehaviour
{
    private CreateGlass createGlass;

    private Rigidbody2D rigidbody;
    private bool isDragging;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        createGlass = gameObject.GetComponentInParent<CreateGlass>();
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        createGlass.CreateNewGlass();
        transform.SetParent(createGlass.transform.parent);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rigidbody.isKinematic = false;
    }
}
