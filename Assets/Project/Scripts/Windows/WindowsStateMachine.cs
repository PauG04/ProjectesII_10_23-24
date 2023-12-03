using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    public enum WindowState
    {
        None,
        Dragging,
        Minimized,
        Closing
    }

    private void Awake()
    {
        CurrentState = States[WindowState.None];
    }
}