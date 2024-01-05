using UnityEngine;
using UnityEngine.EventSystems;

public class WindowsDragging : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine.WindowState _state;
    private WindowsStateMachine _windowsStateMachine;

	private bool _isCanvas;
	
	private Canvas _canvas;
	private RectTransform _rectTransform;
	
    private ListOfWindows _listOfWindows;

    private Vector3 _offset;
    private Vector3 _mousePos;

	public WindowsDragging(WindowsStateMachine windowsStateMachine, ListOfWindows listOfWindows, bool isCanvas) : base(WindowsStateMachine.WindowState.Dragging)
    {
	    _windowsStateMachine = windowsStateMachine;
	    _isCanvas = isCanvas;
        _listOfWindows = listOfWindows;
    }

    public override void EnterState()
	{
		Debug.Log("Enter Drag State");
		
		if (_isCanvas)
		{
			_canvas = _windowsStateMachine.GetComponent<Canvas>();
			_rectTransform = _windowsStateMachine.GetComponent<RectTransform>();
		}
		
        _state = WindowsStateMachine.WindowState.Dragging;

        _offset = new Vector3(
            _windowsStateMachine.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            _windowsStateMachine.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y
        );
    }

    public override void ExitState()
    {
	    Debug.Log("Exit Drag State");
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
        _state = WindowsStateMachine.WindowState.Idle;
    }
    public override void UpdateState()
    {
	    _listOfWindows.MoveObjectInFront(_windowsStateMachine.gameObject);
        
	    if(!_isCanvas)
	    {
		    DragWindows();
	    }
    }
    
	private void DragWindows()
	{
		Vector3 mousePosToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		_mousePos = new Vector3(
			mousePosToWorldPoint.x + _offset.x,
			mousePosToWorldPoint.y + _offset.y,
			_windowsStateMachine.transform.position.z
		);

		_windowsStateMachine.transform.position = _mousePos;
	}

}
