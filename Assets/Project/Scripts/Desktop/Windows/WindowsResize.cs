using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Windows;

public class WindowsResize : BaseState<WindowsStateMachine.WindowState>
{
    
    private WindowsStateMachine.WindowState _state;
    /*
    private WindowsStateMachine _windowsStateMachine;
    private DesktopApp _app;
    private bool _isCanvas;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    #region Adjustements Window
    private float _offsetWidth = 0.02f;
    private float _offsetHeight = 0.05f;
    private float _moveHeight = -0.015f;

    private float _offsetWidthCanvas = 11.75f;
    private float _offsetHeightCanvas = 29f;
    private float _moveHeightCanvas = -9f;
    #endregion
    */
    public WindowsResize(
        WindowsStateMachine windowsStateMachine,
        DesktopApp app,
        bool isCanvas
    ) : base(WindowsStateMachine.WindowState.Resize)
    {
        /*
        _windowsStateMachine = windowsStateMachine;
        _isCanvas = isCanvas;
        _app = app;
        _isCanvas = isCanvas;
        */
    }

    public override void EnterState()
    {
        _state = WindowsStateMachine.WindowState.Resize;
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

    }
    private void ResizeWindowToAdjust()
    {
        // Get childapp and resize the windows
        /*
        Vector2 newWindowSize = new Vector2(
            0, 
            0
        );

        _spriteRenderer.size = newWindowSize;
        */
    }
}
