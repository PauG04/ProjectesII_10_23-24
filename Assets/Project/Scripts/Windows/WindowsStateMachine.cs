using System;
using UnityEngine;
using Windows;

public class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    [Header("Windows Creation Variables")]
    [SerializeField] private WindowNode node;

    [Header("Windows Ordering Variables")]
    [SerializeField] private ListOfWindows listOfWindows;


    public enum WindowState
    {
        Idle,
        Creating,
        Order,
        Dragging,
        Minimized,
        Closing
    }

    private void Awake()
    {
        States.Add(WindowState.Idle, new WindowsIdle());
        States.Add(WindowState.Creating, new WindowsCreation(this, node));
        States.Add(WindowState.Order, new WindowsOrder(this, listOfWindows));
        States.Add(WindowState.Dragging, new WindowsDragging(this, listOfWindows));

        CurrentState = States[WindowState.Creating];
    }
}