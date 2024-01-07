
using UnityEngine;

public class WindowsClose : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
	private WindowsStateMachine _windowsStateMachine;
	private ListOfWindows _listOfWindows;

	private GameObject _miniIcon;
	private DesktopApp _app;
	
	public WindowsClose(WindowsStateMachine windowsStateMachine, ListOfWindows listOfWindows, GameObject gameObject, GameObject miniIcon, DesktopApp app) : base(WindowsStateMachine.WindowState.Closing)
	{
		_windowsStateMachine = windowsStateMachine;
		_listOfWindows = listOfWindows;

		_miniIcon = miniIcon;
		_app = app;
	}
	public override void EnterState()
	{
		_state = WindowsStateMachine.WindowState.Closing;
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
		_listOfWindows.RemoveWindowInList(_windowsStateMachine.gameObject);
		_app.ResetApp();
		GameObject.Destroy(_windowsStateMachine.gameObject);
		GameObject.Destroy(_miniIcon);
	}
}
