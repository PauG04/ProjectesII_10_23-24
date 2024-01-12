using UnityEngine;
using UnityEngine.UIElements;

public class WindowsIdle : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
    
	private GameObject _close;
	private GameObject _minimize;

	private bool _isCanvas;

	private BoxCollider2D _backgroundCollider;

	private IsColliderPressed _closeIsPressed;
	private IsColliderPressed _minimizeIsPressed;
    private IsColliderPressed _backgroundIsPressed;


    public WindowsIdle(GameObject close, GameObject minimize, BoxCollider2D backgroundCollider, bool isCanvas) : base(WindowsStateMachine.WindowState.Idle)
    {
	    _close = close;
	    _minimize = minimize;
		_backgroundCollider = backgroundCollider;

	    _isCanvas = isCanvas;
    }
    public override void EnterState()
	{
   
	    _state = WindowsStateMachine.WindowState.Idle;

        _closeIsPressed = _close.GetComponent<IsColliderPressed>();
        _minimizeIsPressed = _minimize.GetComponent<IsColliderPressed>();
        _backgroundIsPressed = _backgroundCollider.GetComponent<IsColliderPressed>();
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
		_state = WindowsStateMachine.WindowState.Dragging;
    }
    public override void OnMouseDrag()
    {

    }
    public override void OnMouseUp()
    {

    }
    public override void UpdateState()
	{
        if (Input.GetMouseButtonDown(0))
        {
            if (_backgroundIsPressed.GetIsPressed())
            {
                _state = WindowsStateMachine.WindowState.Order;
            }
            if (!_isCanvas)
            {
                if (_closeIsPressed.GetIsPressed())
                {
                    _state = WindowsStateMachine.WindowState.Closing;
                }
                else if (_minimizeIsPressed.GetIsPressed())
                {
                    _state = WindowsStateMachine.WindowState.Minimize;
                }
            }
        }
    }
}
