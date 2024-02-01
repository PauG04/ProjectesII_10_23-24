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
    private float _divideProgress;

    private bool _canShake;
    private bool _isDown;

    private Vector2 _initScale = Vector2.one;
    private float _increaseScale = 1.5f;

    public ShakerDraggingClose(ShakerStateMachine shakerStateMachine, float maxAngle, float progress, float maxProgress, float divideProgress) : base(ShakerStateMachine.ShakerState.DraggingClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _maxAngle = maxAngle;
        _maxProgress = maxProgress;
        _progress = progress;
        _divideProgress = divideProgress;
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
    public override void OnMouseUp()
    {
        _shakerStateMachine.transform.localEulerAngles = Vector3.zero;
        _state = ShakerStateMachine.ShakerState.IdleClosed;
    }
    public override void UpdateState()
    {       
        _targetJoint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _rb.SetRotation(Vector2.Dot(_rb.velocity.normalized, Vector2.up) * _rb.velocity.sqrMagnitude * _maxAngle);
        Shaking();
    }

    private void Shaking()
    {
        if(_shakerStateMachine.GetIsInWorkSpace())
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
    }
    private void IncreaseBar()
    {
        if (_isDown)
        {
            _progress += (_newPosition.y - _shakerStateMachine.transform.position.y) / _divideProgress;
        }
        else
        {
            _progress += (_shakerStateMachine.transform.position.y - _newPosition.y) / _divideProgress;
        }
        //cameraShake.ShakeCamera((transform.position.y - _newPosition.y) * intensityShaking);
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

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("WorkSpace") && !_shakerStateMachine.GetIsInWorkSpace())
        {

            _shakerStateMachine.SetGetInWorkSpace(true);
            _shakerStateMachine.transform.localScale *= _increaseScale;
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && _shakerStateMachine.GetIsInWorkSpace())
        {
            _shakerStateMachine.SetGetInWorkSpace(false);
            _shakerStateMachine.transform.localScale = _initScale;
        }
    }
    public float GetProgress() => _progress;
}