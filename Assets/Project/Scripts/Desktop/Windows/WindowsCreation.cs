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

	private float _scaleSpeed = 3f;
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
	private Vector2 _closeAdjustement = new Vector2(0.0225f, 0.02f);
	private Vector2 _minimizeAdjustement = new Vector2(0.0625f, 0.02f);
	private Vector2 _newClosePosition;
	private Vector2 _newMinimizePosition;
    #endregion

    private SpriteRenderer _spriteRenderer;
	private PolygonCollider2D _collider;
	private Image _imageRenderer;

	public WindowsCreation(
		WindowsStateMachine windowsStateMachine, 
		ListOfWindows listOfWindows, 
		WindowNode node, 
		bool isCanvas, 
		GameObject close, 
		GameObject minimize
	) : base(WindowsStateMachine.WindowState.Creating)
    {
	    _windowsStateMachine = windowsStateMachine;
	    _listOfWindows = listOfWindows;
	    _node = node;
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
		    _collider = _windowsStateMachine.gameObject.GetComponent<PolygonCollider2D>();
	    }
	    _isCreated = false;
    }

    public override void ExitState()
	{
		Debug.Log("Exit Creating State");
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
		
		_spriteRenderer.size = newWindowSize;
    }
	private void AdjustChildPositions()
	{
		_newClosePosition = new Vector2(
			_spriteRenderer.bounds.max.x - _closeAdjustement.x, 
			_spriteRenderer.bounds.max.y - _closeAdjustement.y
		);
		
		_newMinimizePosition = new Vector2(
			_spriteRenderer.bounds.max.x - _minimizeAdjustement.x, 
			_spriteRenderer.bounds.max.y - _minimizeAdjustement.y
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
		
		RectTransform parentRectTransform = _windowsStateMachine.GetComponent<RectTransform>();
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
	private IEnumerator ScaleWindows()
	{
		Vector3 initialScale = _windowsStateMachine.transform.localScale;
		float elapsedTime = 0f;

		while (elapsedTime < 1f)
		{
			_windowsStateMachine.transform.localScale = Vector3.Lerp(initialScale, Vector3.one, elapsedTime);
			elapsedTime += Time.deltaTime * _scaleSpeed;
			yield return null;
		}

		_windowsStateMachine.transform.localScale = Vector3.one;
	}
}
