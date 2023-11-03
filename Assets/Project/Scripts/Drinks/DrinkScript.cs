using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkScript : MonoBehaviour
{
    [SerializeField] private bool isMouseNotPressed = true;
    private void OnMouseUp()
    {
        isMouseNotPressed = true;
    }

    private void OnMouseDown()
    {
        isMouseNotPressed = false;
    }

    public bool GetIsMouseNotPressed()
    {
        return isMouseNotPressed;
    }
}
