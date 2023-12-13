using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWindow : MonoBehaviour
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
