using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMinimize : MonoBehaviour
{
    [HideInInspector] public GameObject minimize;
    [HideInInspector] public Minimize minimize_;

    private void OnMouseDown()
    {
        if (minimize_.IsMinimize())
        {
            minimize.SetActive(true);
            minimize_.SetIsMinimize(false);
        }
    }
}
