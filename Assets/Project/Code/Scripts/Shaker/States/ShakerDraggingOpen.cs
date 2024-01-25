using UnityEngine;

public class ShakerDraggingOpen : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;
    private ShakerStateMachine _shakerStateMachine;

    private Vector3 _offset;

    #region Rotation Variables
    private float _rotationSpeed = 10f;
    private float _maxRotation = 180f;

    private bool _isRotating = false;
    private float _targetRotation = 0f;
    private float _currentRotation = 0f;
    #endregion

    #region Liquid Variables
    private GameObject _liquidPrefab;
    private Transform _spawnPoint;

    private float _spawnRate = 5.0f;
    private float _spawnWidth = 1.0f;
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
        _targetRotation = Mathf.Clamp(_targetRotation, 0, _maxRotation);

        _currentRotation = Mathf.Lerp(_currentRotation, -_targetRotation, Time.deltaTime * _rotationSpeed);
        _shakerStateMachine.transform.rotation = Quaternion.Euler(Vector3.forward * _currentRotation);
    }
    private void ResetObjectPosition()
    {
        _targetRotation = 0f;
        _currentRotation = 0f;
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);
    }

    private void DropLiquid(float currentFillLevel, float inclination)
    {
        /// TODO
        /// ----------
        /// Drop liquid depending on the current liquid inside the shaker, if it is almost empty, higher inclination is needed.
        /// The liquid spawns depending on the inclination, if it is aiming to the right, the liquid spawns from the right spot of the shaker.
        /// If the current liquid is high, and the inclination is too much, the liquid spawner is wider and faster.

        Vector3 spawnPosition = _spawnPoint.position + Quaternion.Euler(0f, 0f, inclination) * Vector3.right * _spawnWidth;
        GameObject liquid = GameObject.Instantiate(_liquidPrefab, spawnPosition, Quaternion.identity);


        GameObject.Destroy(liquid, 2.0f);
    }
}
