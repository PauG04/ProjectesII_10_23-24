
using System.Collections.Generic;
using UnityEngine;

public class WindowsClose : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
	private WindowsStateMachine _windowsStateMachine;
	private ListOfWindows _listOfWindows;
	private List<DesktopApp> _childApps;

    private GameObject _miniIcon;
	private DesktopApp _app;
	
	public WindowsClose(WindowsStateMachine windowsStateMachine, ListOfWindows listOfWindows, GameObject miniIcon, DesktopApp app) : base(WindowsStateMachine.WindowState.Closing)
	{
		_windowsStateMachine = windowsStateMachine;
		_listOfWindows = listOfWindows;

		_miniIcon = miniIcon;
		_app = app;
	}
	public override void EnterState()
	{
		_state = WindowsStateMachine.WindowState.Closing;

		_childApps = _windowsStateMachine.GetChildApps();

    }
	public override void ExitState()
	{

	}
	public override WindowsStateMachine.WindowState GetNextState()
	{
		return _state;
	}
	public override void OnMouseDown()
	{
        
	}
	public override void OnMouseDrag()
	{
        
	}
	public override void OnMouseUp()
	{
		
	}
	public override void UpdateState()
    {
        foreach (DesktopApp childApp in _childApps)
        {
			if (childApp.GetApp() != null)
			{
                _listOfWindows.RemoveWindowInList(childApp.GetApp());
                GameObject.Destroy(childApp.GetApp());
                GameObject.Destroy(childApp.GetMiniIcon());
                childApp.ResetApp();
            }
        }

        _listOfWindows.RemoveWindowInList(_windowsStateMachine.gameObject);
        _app.ResetApp();
        GameObject.Destroy(_windowsStateMachine.gameObject);
        GameObject.Destroy(_miniIcon);    
	}
}
