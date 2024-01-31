using UnityEngine.UIElements;
using UnityEngine;

public class ShakerDraggingClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private TargetJoint2D _targetJoint;
    private Rigidbody2D _rb;

    private Vector2 _newPosition;

    private float _maxAngle;
    private float _progress;
    private float _maxProgress;

    private bool _canShake;
    private bool _isDown;
    public ShakerDraggingClose(ShakerStateMachine shakerStateMachine, float maxAngle, float progress, float maxProgress) : base(ShakerStateMachine.ShakerState.DraggingClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _maxAngle = maxAngle;
        _maxProgress = maxProgress;
        _progress = progress;
    }
    public override void EnterState()
    {
        _state = ShakerStateMachine.ShakerState.DraggingClosed;

        _targetJoint = _shakerStateMachine.GetComponent<TargetJoint2D>();
        _rb = _shakerStateMachine.GetComponent<Rigidbody2D>();

        _newPosition = _shakerStateMachine.transform.position;

        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public override void ExitState()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public override ShakerStateMachine.ShakerState GetNextState()
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
        _shakerStateMachine.transform.localEulerAngles = Vector3.zero;
        _state = ShakerStateMachine.ShakerState.IdleClosed;
    }
    public override void UpdateState()
    {
        Shaking();
        _targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _shakerStateMachine.transform.SetParent(null);

        _rb.SetRotation(Vector2.Dot(_rb.velocity.normalized, Vector2.up) * _rb.velocity.sqrMagnitude * _maxAngle);
    }

    private void Shaking()
    {
        StartShaking();
        EndClicking();
        if (_canShake && _progress <= _maxProgress)
        {
            DirectionShaker();
            IncreaseBar();
        }
        else
        {
            //cameraShake.SetTransforPosition();
        }
        SetDrinkState();
    }
    private void IncreaseBar()
    {
        if (_isDown)
        {
            _progress += (_newPosition.y - _shakerStateMachine.transform.position.y) / 5;
        }
        else
        {
            _progress += (_shakerStateMachine.transform.position.y - _newPosition.y) / 5;
        }
        //cameraShake.ShakeCamera((transform.position.y - _newPosition.y) * intensityShaking);
        Debug.Log(_progress);
        _newPosition = _shakerStateMachine.transform.position;
    }
    private void DirectionShaker()
    {
        _isDown = !(_isDown && _rb.velocity.y >= 0 && _canShake);
    }
    private void StartShaking()
    {
        if ((_rb.velocity.y >= 0.00001f || _rb.velocity.y <= -0.00001f))
        {
            _canShake = true;
        }
    }
    private void EndClicking()
    {
        if ((_rb.velocity.y <= 0.00001f && _rb.velocity.y >= -0.00001f) || _progress >= _maxProgress)
        {
            _canShake = false;
        }
    }
    private void SetDrinkState()
    {
        /*
        if (_progress < _maxProgress * 0.33f)
        {
            liquidManager.SetDrinkState(TypeOfCocktails.StateOfCocktail.Idle);
        }
        else if (_progress >= _maxProgress * 0.66f)
        {
            liquidManager.SetDrinkState(TypeOfCocktails.StateOfCocktail.Mixed);
        }
        else
        {
            liquidManager.SetDrinkState(TypeOfCocktails.StateOfCocktail.Shaked);
        }
        */
    }

    public float GetProgress() => _progress;
}