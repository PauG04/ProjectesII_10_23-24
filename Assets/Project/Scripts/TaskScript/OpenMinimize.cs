using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMinimize : MonoBehaviour
{
    [SerializeField]
    private GameObject minimize;
    [SerializeField]
    private Minimize minimize_;

    //private void Update()
    //{
    //    if (SimulateButton(gameObject.name))
    //    {
    //        minimize.gameObject.SetActive(true);
    //    }
    //}

    private void OnMouseDown()
    {
        if (minimize_.IsMinimize())
        {
            minimize.SetActive(true);
            minimize_.SetIsMinimize(false);
        }
            
    }
}
