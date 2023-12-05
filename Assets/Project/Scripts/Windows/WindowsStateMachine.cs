using System;
using UnityEngine;
using Windows;

public class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    [Header("Windows Creation Variables")]
    public WindowNode node;

    [SerializeField] private float offsetWidth = 0.02f;
    [SerializeField] private float offsetHeight = 0.08f;

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
        States.Add(WindowState.Creating, new WindowCreation(this, node, offsetWidth, offsetHeight));
        States.Add(WindowState.Dragging, new WindowsDragging(this));

        CurrentState = States[WindowState.Creating];
    }
}