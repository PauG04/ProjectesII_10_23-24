using System;
using UnityEngine;
using Windows;

public class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    [Header("Windows Creation Variables")]
    public WindowNode node;

    public enum WindowState
    {
        Idle,
        Creating,
        MoveToFront,
        Dragging,
        Minimized,
        Closing
    }

    private void Awake()
    {
        States.Add(WindowState.Idle, new WindowsIdle());
        States.Add(WindowState.Creating, new WindowCreation(this, node));
        States.Add(WindowState.Dragging, new WindowsDragging(this));

        CurrentState = States[WindowState.Creating];
    }
}