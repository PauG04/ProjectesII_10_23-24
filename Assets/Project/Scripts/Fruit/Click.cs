using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliock : MonoBehaviour
{

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        transform.position = newPosition;
    }
}
