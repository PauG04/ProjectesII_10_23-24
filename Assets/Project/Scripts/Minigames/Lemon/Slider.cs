using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] private float magnitude;

    private Vector3 direction;
    BoxCollider2D collider;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        direction = newPosition - transform.position;
        if (direction.magnitude > magnitude)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }

        transform.position = newPosition;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public BoxCollider2D GetCollider()
    {
        return collider;
    }
}
