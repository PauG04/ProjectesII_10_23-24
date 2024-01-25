using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ShakerDraggingOpen : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;
    private ShakerStateMachine _shakerStateMachine;

    private Vector3 _offset;

    #region Rotation Variables
    private float _rotationSpeed = 10f;
    private float maxRotation = 180f;

    private bool _isRotating = false;
    private float _targetRotation = 0f;
    private float _currentRotation = 0f;
    #endregion

    public ShakerDraggingOpen(ShakerStateMachine shakerStateMachine, float rotationSpeed) : base(ShakerStateMachine.ShakerState.DraggingOpen)
    {
        _shakerStateMachine = shakerStateMachine;
        _rotationSpeed = rotationSpeed;
    }

    public override void EnterState()
    {
        _state = ShakerStateMachine.ShakerState.DraggingOpen;

        _offset = _shakerStateMachine.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public override void ExitState()
    {
        _targetRotation = 0f;
        _currentRotation = 0f;
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
        _state = ShakerStateMachine.ShakerState.IdleOpen;
    }

    public override void UpdateState()
    {
        CalculatePosition();

        if (Input.GetMouseButtonDown(1))
        {
            _isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _isRotating = false;
        }

        if (_isRotating)
        {
            RotateObject();
        } 
        else
        {
            if (_shakerStateMachine.transform.rotation != Quaternion.identity)
            {
                ResetObjectPosition();
            }
        }
    }

    private void CalculatePosition()
    {
        _shakerStateMachine.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x + _offset.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + _offset.y,
            0
        );
    }

    private void RotateObject()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        
        _targetRotation += mouseY * _rotationSpeed;
        _targetRotation = Mathf.Clamp(_targetRotation, 0, maxRotation);

        _currentRotation = Mathf.Lerp(_currentRotation, -_targetRotation, Time.deltaTime * _rotationSpeed);
        _shakerStateMachine.transform.rotation = Quaternion.Euler(Vector3.forward * _currentRotation);
    }
    private void ResetObjectPosition()
    {
        _targetRotation = 0f;
        _currentRotation = 0f;
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);
    }
}
