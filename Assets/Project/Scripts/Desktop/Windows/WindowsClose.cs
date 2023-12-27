using UnityEngine;

public class WindowsClose : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
	private WindowsStateMachine _windowsStateMachine;
	
	private GameObject _closeGameObject;
	
	public WindowsClose(WindowsStateMachine windowsStateMachine, GameObject gameObject) : base(WindowsStateMachine.WindowState.Closing)
	{
		_windowsStateMachine = windowsStateMachine;
		_closeGameObject = gameObject;
	}
	
	public override void EnterState()
	{
		Debug.Log("Closing Windows " + _windowsStateMachine.gameObject.name);
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
