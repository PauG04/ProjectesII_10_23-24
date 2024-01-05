using UnityEngine;

public class WindowsIdle : BaseState<WindowsStateMachine.WindowState>
{
	private WindowsStateMachine.WindowState _state;
    
	private GameObject _close;
	private GameObject _minimize;

	private bool _isCanvas;

	private BoxCollider2D _closeCollider;
	private BoxCollider2D _minimizeCollider;

	public WindowsIdle(GameObject close, GameObject minimize, bool isCanvas) : base(WindowsStateMachine.WindowState.Idle)
    {
	    _close = close;
	    _minimize = minimize;
	    _isCanvas = isCanvas;
    }
    public override void EnterState()
	{
   
	    _state = WindowsStateMachine.WindowState.Idle;
        
	    _closeCollider = _close.GetComponent<BoxCollider2D>();
    	_minimizeCollider = _minimize.GetComponent<BoxCollider2D>();
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
		if (!_isCanvas)
		{
			_state = WindowsStateMachine.WindowState.Dragging;
		}
    }
    public override void OnMouseDrag()
    {

    }
    public override void OnMouseUp()
    {

    }
    public override void UpdateState()
	{
		
		if (Input.GetMouseButtonDown(0) && !_isCanvas)   
	    {
	    	Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    	
	    	if (_closeCollider.OverlapPoint(position))
	    	{
	    		_state = WindowsStateMachine.WindowState.Closing;
	    	}
	    	if (_minimizeCollider.OverlapPoint(position))
	    	{
	    		_state = WindowsStateMachine.WindowState.Minimize;
	    	}
	    }
    }
}
