using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Vector2 mousePos;

    public bool SimulateButton(string objectName)
    {
        if (Input.GetMouseButtonDown(0)) { 
            Vector3 mousePosToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePosToWorldPoint.x, mousePosToWorldPoint.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == objectName)
                { 
                    return true; 
                }
            }
        }
        return false;
    }
}
