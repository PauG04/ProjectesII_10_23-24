using UnityEngine;

public class WindowsIdle : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine.WindowState _state;

    public WindowsIdle() : base(WindowsStateMachine.WindowState.Idle)
    {
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Idle;
        Debug.Log("Enter Window Idle State");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Window Idle State");
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

    }
}
