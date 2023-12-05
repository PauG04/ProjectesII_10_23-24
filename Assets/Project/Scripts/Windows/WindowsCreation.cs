using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WindowCreation : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine _windowsStateMachine;
    private WindowsStateMachine.WindowState _state;

    private WindowNode _node;

    private float _offsetWidth = 0.02f;
    private float _offsetHeight = 0.08f;

    private SpriteRenderer _spriteRenderer;
    public WindowCreation(WindowsStateMachine windowsStateMachine, WindowNode node, float offsetWidth, float offsetHeight) : base(WindowsStateMachine.WindowState.Creating)
    {
        _windowsStateMachine = windowsStateMachine;
        _node = node;

        _offsetWidth = offsetWidth;
        _offsetHeight = offsetHeight;
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Creating;

        _spriteRenderer = _windowsStateMachine.gameObject.GetComponent<SpriteRenderer>();

        Debug.Log("Enter Window Creation State");
    }

    public override void ExitState()
    {
        Debug.Log("Exit Window Creation State");
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
        ResizeWindowToPrefab();
        _state = WindowsStateMachine.WindowState.Idle;
    }

    private void RenameObject()
    {
        _windowsStateMachine.gameObject.name = _node.GetWindowName();

    }
    private void CreatePrefabInsideWindow()
    {
        GameObject prefabInstance = GameObject.Instantiate(_node.GetPrefabChild());
        prefabInstance.transform.parent = _windowsStateMachine.transform;
    }
    private void ResizeWindowToPrefab()
    {
        _node.GetPrefabChild().transform.position = new Vector3(
            _node.GetPrefabChild().transform.position.x,
            _node.GetPrefabChild().transform.position.y,
            _windowsStateMachine.transform.position.z
        );

        _windowsStateMachine.transform.position = _node.GetPrefabChild().transform.position;

        Vector2 newWindowSize = new Vector2(
            _node.GetPrefabChild().transform.localScale.x + _offsetWidth,
            _node.GetPrefabChild().transform.localScale.y + _offsetHeight
        );

        _spriteRenderer.size = newWindowSize;
    }
}
