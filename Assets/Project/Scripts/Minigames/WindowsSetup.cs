using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsSetup : MonoBehaviour
{
    private WindowsStateMachine window;

    private void Start()
    {
       window = gameObject.transform.parent.GetComponent<WindowsStateMachine>();
    }

    public WindowsStateMachine GetWindows()
    {
        return window;
    }
}
