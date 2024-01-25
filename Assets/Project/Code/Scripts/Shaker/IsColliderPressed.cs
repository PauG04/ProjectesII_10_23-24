using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsColliderPressed : MonoBehaviour
{
    private bool isPressed;

    private void OnMouseDown()
    {
        isPressed = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }
}

