using System;
using UnityEngine;
using Windows;
using UnityEngine.UI;

public class WindowsStateMachine : StateMachineManager<WindowsStateMachine.WindowState>
{
    [Header("Windows Creation Variables")]
    [SerializeField] private WindowNode _node;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PolygonCollider2D _collider;
	
	[Header("Windows Canvas Variables")]
	[SerializeField] private bool _isCanvas;

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
	    States.Add(WindowState.Idle, new WindowsIdle(_close, _minimize, _isCanvas));
	    States.Add(WindowState.Creating, new WindowsCreation(this, _listOfWindows, _node, _isCanvas, _close, _minimize));
        States.Add(WindowState.Order, new WindowsOrder(this, _listOfWindows));
	    States.Add(WindowState.Dragging, new WindowsDragging(this, _listOfWindows, _isCanvas));
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
    
	public void SetStateClosed()
	{
		CurrentState = States[WindowState.Closing];
	}
	public void SetStateMinimize()
	{
		CurrentState = States[WindowState.Minimize];
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
	public void SetIsCanvas(bool isCanvas)
	{
		_isCanvas = isCanvas;
	}
}