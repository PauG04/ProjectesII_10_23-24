using UnityEngine;
using Windows;
using System.Collections;
using UnityEngine.UI;

public class WindowsCreation : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine _windowsStateMachine;
    private WindowsStateMachine.WindowState _state;
    private WindowNode _node;
	private ListOfWindows _listOfWindows;
	
	private GameObject _close;
	private GameObject _minimize;
    private BoxCollider2D _backgroundCollider;

	private bool _isCreated;

	private bool _isCanvas;

    #region Adjustements Window
    private float _offsetWidth = 0.02f;
    private float _offsetHeight = 0.05f;
	private float _moveHeight = -0.015f;
	
	private float _offsetWidthCanvas = 11.75f;
	private float _offsetHeightCanvas = 29f;
	private float _moveHeightCanvas = -9f;
    #endregion
    
    #region WindowsControl Adjustements
	private Vector3 _closeAdjustement = new Vector3(0.0225f, 0.02f, -0.01f);
	private Vector3 _minimizeAdjustement = new Vector3(0.0625f, 0.02f, -0.01f);
	private Vector3 _newClosePosition;
	private Vector3 _newMinimizePosition;
    #endregion

    private SpriteRenderer _spriteRenderer;
	private BoxCollider2D _collider;

	public WindowsCreation(
		WindowsStateMachine windowsStateMachine,
		ListOfWindows listOfWindows,
		WindowNode node,
		BoxCollider2D backgroundCollider,
        bool isCanvas, 
		GameObject close, 
		GameObject minimize
	) : base(WindowsStateMachine.WindowState.Creating)
    {
	    _windowsStateMachine = windowsStateMachine;
	    _listOfWindows = listOfWindows;
	    _node = node;
		_backgroundCollider = backgroundCollider;
        _isCanvas = isCanvas;
	    _close = close;
	    _minimize = minimize;
    }

    public override void EnterState()
    {
	    _state = WindowsStateMachine.WindowState.Creating;
	    
	    if (!_isCanvas)
	    {
		    _spriteRenderer = _windowsStateMachine.gameObject.GetComponent<SpriteRenderer>();
	    }
	    
	    _collider = _windowsStateMachine.gameObject.GetComponent<BoxCollider2D>();
	    _isCreated = false;
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
		_listOfWindows.MoveObjectInFront(_windowsStateMachine.gameObject);

		if (!_isCreated)
		{
			RenameObject();
			if (_isCanvas)
			{
				CreateCanvas();
			}
			else 
			{
				CreatePrefabInsideWindow();
			}
			_isCreated = true;
		}
		else 
		{
			if (!_isCanvas)
			{
				AdjustChildPositions();	
			}
		}

		if (!_isCanvas && _windowsStateMachine.transform.localScale == Vector3.one)
		{
			_state = WindowsStateMachine.WindowState.Idle;
		}
		if (_isCanvas)
		{
			_state = WindowsStateMachine.WindowState.Idle;
		}
    }
    private void RenameObject()
    {
        _windowsStateMachine.gameObject.name = _node.GetWindowName();
    }
    private void CreatePrefabInsideWindow()
    {
        ResizeWindowToAdjust();
	    
        GameObject prefabInstance = GameObject.Instantiate(_node.GetPrefabChild());
        Vector3 previousScale = prefabInstance.transform.localScale;
	    prefabInstance.transform.SetParent(_windowsStateMachine.transform);
        prefabInstance.transform.localScale = previousScale;

	    MoveWindowToAdjust(prefabInstance);
    }
    private void MoveWindowToAdjust(GameObject prefab)
    {
        Vector3 newPosition = new Vector3(
             _windowsStateMachine.transform.position.x,
             _windowsStateMachine.transform.position.y + _moveHeight,
             _windowsStateMachine.transform.position.z
        );

	    prefab.transform.localPosition = newPosition;
    }
    private void ResizeWindowToAdjust()
	{
		Vector2 newWindowSize = new Vector2(
			_node.GetPrefabChild().transform.localScale.x + _offsetWidth,
			_node.GetPrefabChild().transform.localScale.y + _offsetHeight
		);

		_backgroundCollider.size = _node.GetPrefabChild().transform.localScale;

        _spriteRenderer.size = newWindowSize;
    }
	private void AdjustChildPositions()
	{
		_newClosePosition = new Vector3(
			_spriteRenderer.bounds.max.x - _closeAdjustement.x, 
			_spriteRenderer.bounds.max.y - _closeAdjustement.y,
			_closeAdjustement.z
		);
		
		_newMinimizePosition = new Vector3(
			_spriteRenderer.bounds.max.x - _minimizeAdjustement.x, 
			_spriteRenderer.bounds.max.y - _minimizeAdjustement.y,
			_minimizeAdjustement.z
		);

		_close.transform.position = _newClosePosition;
		_minimize.transform.position = _newMinimizePosition;
	}
	private void CreateCanvas()
	{
		Canvas canvas = _windowsStateMachine.GetComponent<Canvas>();
		canvas.worldCamera = Camera.main;
		canvas.vertexColorAlwaysGammaSpace = true;
		CreatePrefabInsideCanvas();
	}
	private void CreatePrefabInsideCanvas()
	{
		GameObject prefabInstance = GameObject.Instantiate(_node.GetPrefabChild());
		RectTransform prefabRectTransform = prefabInstance.GetComponent<RectTransform>();

		ResizeCanvasToAdjust(prefabRectTransform);

		prefabRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
		prefabRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
		
		Vector2 previousScale = prefabRectTransform.localScale;

		prefabInstance.transform.SetParent(_windowsStateMachine.transform, false);

		prefabRectTransform.anchoredPosition = Vector2.zero;
		prefabRectTransform.localScale = previousScale;
		
		MoveWindowToAdjust(prefabRectTransform);
	}
	private void ResizeCanvasToAdjust(RectTransform rectTransform)
	{
		Vector2 newWindowsSizeCanvas = new Vector2(
			rectTransform.sizeDelta.x + _offsetWidthCanvas,
			rectTransform.sizeDelta.y + _offsetHeightCanvas
		);

		_collider.offset = new Vector2(0, (newWindowsSizeCanvas.y / 2) - 12.25f);
		_collider.size = new Vector2(newWindowsSizeCanvas.x, _collider.size.y);

        _backgroundCollider.size = rectTransform.sizeDelta;

        _windowsStateMachine.GetComponent<RectTransform>().sizeDelta = newWindowsSizeCanvas;
	}
	
	private void MoveWindowToAdjust(RectTransform rectTransform)
	{
		Vector3 newPosition = new Vector3(
			rectTransform.localPosition.x,
			rectTransform.localPosition.y + _moveHeightCanvas,
			rectTransform.localPosition.z
		);

		rectTransform.localPosition = newPosition;
	}
}
