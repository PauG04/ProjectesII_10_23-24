using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ShakerStateMachine shakerState;
    public float progress;

    private void Update()
    {
        progress = shakerState.GetProgress();
    }
}
