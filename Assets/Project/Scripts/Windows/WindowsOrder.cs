using UnityEngine;

public class WindowsOrder : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine _windowsStateMachine;
    private WindowsStateMachine.WindowState _state;

    private ListOfWindows _listOfWindows;


    public WindowsOrder(WindowsStateMachine windowsStateMachine, ListOfWindows listOfWindows) : base(WindowsStateMachine.WindowState.Order)
    {
        _windowsStateMachine = windowsStateMachine;
        _listOfWindows = listOfWindows;
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Order;
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
        _state = WindowsStateMachine.WindowState.Idle;
    }
}