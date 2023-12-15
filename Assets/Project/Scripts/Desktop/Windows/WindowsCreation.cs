using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Windows;

public class WindowsCreation : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine _windowsStateMachine;
    private WindowsStateMachine.WindowState _state;
    private WindowNode _node;

    #region Adjustements Window
    private float _offsetWidth = 0.02f;
    private float _offsetHeight = 0.05f;
    private float _moveHeight = -0.015f;
    #endregion

    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _collider;

    public WindowsCreation(WindowsStateMachine windowsStateMachine, WindowNode node) : base(WindowsStateMachine.WindowState.Creating)
    {
        _windowsStateMachine = windowsStateMachine;
        
        _node = node;
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Creating;
        _spriteRenderer = _windowsStateMachine.gameObject.GetComponent<SpriteRenderer>();
        _collider = _windowsStateMachine.gameObject.GetComponent<PolygonCollider2D>();
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
        RenameObject();
        CreatePrefabInsideWindow();
        _state = WindowsStateMachine.WindowState.Order;
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

        prefabInstance.transform.parent = _windowsStateMachine.transform;

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
}
