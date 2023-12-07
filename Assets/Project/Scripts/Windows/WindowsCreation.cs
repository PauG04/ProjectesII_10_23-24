using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
    public WindowsCreation(WindowsStateMachine windowsStateMachine, WindowNode node) : base(WindowsStateMachine.WindowState.Creating)
    {
        _windowsStateMachine = windowsStateMachine;
        _node = node;
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Creating;
        _spriteRenderer = _windowsStateMachine.gameObject.GetComponent<SpriteRenderer>();
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
        ResizeWindowToAdjust();
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
        prefabInstance.transform.parent = _windowsStateMachine.transform;

        MoveWindowToAdjust(prefabInstance);
    }
    private void MoveWindowToAdjust(GameObject prefab)
    {
        prefab.transform.position = new Vector2(
             _windowsStateMachine.transform.position.x,
             _windowsStateMachine.transform.position.y + _moveHeight
        );
        // Reset collision autotiling to autosize with the object.
        /*
        PolygonCollider2D polygonCollider = _windowsStateMachine.GetComponent<PolygonCollider2D>();

        polygonCollider.autoTiling = false;
        */
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
