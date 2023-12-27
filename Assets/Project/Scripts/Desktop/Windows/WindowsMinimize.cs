using UnityEngine;

public class WindowsMinimize : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
	private WindowsStateMachine _windowsStateMachine;
	
	private GameObject _minimizeGameObject;
	
	public WindowsMinimize(WindowsStateMachine windowsStateMachine, GameObject gameObject) : base(WindowsStateMachine.WindowState.Minimize)
	{
		_windowsStateMachine = windowsStateMachine;
		_minimizeGameObject = gameObject;
	}
	
	public override void EnterState()
	{
		Debug.Log("Minimize Windows " + _windowsStateMachine.gameObject.name);
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
		
	}
}
