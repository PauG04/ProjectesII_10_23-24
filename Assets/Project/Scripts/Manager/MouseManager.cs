using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            AudioManager.instance.Play("click");
    }
}
