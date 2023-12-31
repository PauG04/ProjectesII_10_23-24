using System;
using UnityEngine;
using Windows;

public class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    [Header("Windows Creation Variables")]
    [SerializeField] private WindowNode _node;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PolygonCollider2D _collider;

    [Header("Windows Ordering Variables")]
    [SerializeField] private ListOfWindows _listOfWindows;

	[Header("Windows Control Variables")]
	[SerializeField] private GameObject _close;
	[SerializeField] private GameObject _minimize;
	
	[Header("UI Objects")]
	[SerializeField] private GameObject _miniIcon;
	[SerializeField] private DesktopApp _app;
    
    [Header("Testing Variables")]
    [SerializeField] private bool isTesting = false;
    

    public enum WindowState
    {
        Idle,
        Creating,
        Order,
        Dragging,
	    Minimize,
        Closing
    }

    private void Awake()
    {
	    States.Add(WindowState.Idle, new WindowsIdle(_close, _minimize));
	    States.Add(WindowState.Creating, new WindowsCreation(this, _listOfWindows, _node, _close, _minimize));
        States.Add(WindowState.Order, new WindowsOrder(this, _listOfWindows));
        States.Add(WindowState.Dragging, new WindowsDragging(this, _listOfWindows));
	    States.Add(WindowState.Closing, new WindowsClose(this, _listOfWindows, _close, _miniIcon, _app));
	    States.Add(WindowState.Minimize, new WindowsMinimize(this, _app, _minimize));
	    
        if (isTesting)
        {
            CurrentState = States[WindowState.Creating];
        }
    }
    public void ChangeState(WindowState state)
    {
        CurrentState = States[state];
    }
    public WindowState GetCurrentState()
    {
        return CurrentState.StateKey;
    }
    public void SetNode(WindowNode node)
    {
        _node = node; 
    }
    public void SetListOfWindows(ListOfWindows listOfWindows)
    {
        _listOfWindows = listOfWindows;
    }
	public void SetMiniIcon(GameObject miniIcon)
	{
		_miniIcon = miniIcon;
	}
	public void SetApp(DesktopApp app)
	{
		_app = app;
	}
}