using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMinimize : Button
{
    [SerializeField]
    private GameObject minimize;

    private void Update()
    {
        if (SimulateButton(gameObject.name))
        {
            minimize.gameObject.SetActive(true);
        }
    }
}
