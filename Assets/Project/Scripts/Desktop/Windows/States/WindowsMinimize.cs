using UnityEngine;

public class WindowsMinimize : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
	private WindowsStateMachine _windowsStateMachine;
	
	private GameObject _minimizeGameObject;
	private DesktopApp _app;
	
	public WindowsMinimize(WindowsStateMachine windowsStateMachine, DesktopApp app,GameObject minimizeGameObject) : base(WindowsStateMachine.WindowState.Minimize)
	{
		_windowsStateMachine = windowsStateMachine;
		_app = app;
		_minimizeGameObject = minimizeGameObject;
	}
	
	public override void EnterState()
	{
		_state = WindowsStateMachine.WindowState.Minimize;
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
		_app.Minimize();
		_state = WindowsStateMachine.WindowState.Idle;
	}
}
