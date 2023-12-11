using System;
using UnityEngine;
using Windows;

public class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    [Header("Windows Creation Variables")]
    [SerializeField] private WindowNode _node;

    [Header("Windows Ordering Variables")]
    [SerializeField] private ListOfWindows _listOfWindows;

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
        States.Add(WindowState.Creating, new WindowsCreation(this, _node));
        States.Add(WindowState.Order, new WindowsOrder(this, _listOfWindows));
        States.Add(WindowState.Dragging, new WindowsDragging(this, _listOfWindows));

        //CurrentState = States[WindowState.Creating];
    }
    public void ChangeState(WindowState state)
    {
        CurrentState = States[state];
    }
    public void SetNode(WindowNode node)
    {
        _node = node; 
    }
    public void SetListOfWindows(ListOfWindows listOfWindows)
    {
        _listOfWindows = listOfWindows;
    }
}