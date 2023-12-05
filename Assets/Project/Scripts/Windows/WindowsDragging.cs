﻿using UnityEngine;

public class WindowsDragging : BaseState<WindowsStateMachine.WindowState>
{
    private WindowsStateMachine.WindowState _state;
    private WindowsStateMachine _windowsStateMachine;

    private Vector3 _offset;
    private Vector3 _mousePos;

    public WindowsDragging(WindowsStateMachine windowsStateMachine) : base(WindowsStateMachine.WindowState.Dragging)
    {
        _windowsStateMachine = windowsStateMachine;
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Dragging;

        _offset = new Vector3(
            _windowsStateMachine.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            _windowsStateMachine.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y
        );

        Debug.Log("Enter Window Dragging State");
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Window Dragging State");
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
        Vector3 mousePosToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _mousePos = new Vector3(
            mousePosToWorldPoint.x + _offset.x,
            mousePosToWorldPoint.y + _offset.y,
            _windowsStateMachine.transform.position.z
            );

        _windowsStateMachine.transform.position = _mousePos;
    }
}
