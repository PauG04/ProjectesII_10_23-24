using System.Collections;
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
    private LiquidManager _liquidManager;
    private float _spawnerPositionX = 0.19f;

    private float _minRotationToPourLiquid = 10f;
    private float _maxRotationToPourLiquid = 140f;

    private float _minRotationToMoveSpawner = 90f;
    private float _maxRotationToMoveSpawner = 180f;

    private float _timeSinceLastPour = 0f;
    #endregion

    private float _increaseScale;
    private bool _isInWorkSpace;
    private Vector2 _initScale;

    public ShakerDraggingOpen(ShakerStateMachine shakerStateMachine, float rotationSpeed, GameObject liquidPrefab, Transform spawnPoint, LiquidManager liquidManager, float increaseScale, bool isInWorkSpace, Vector2 initScale) : base(ShakerStateMachine.ShakerState.DraggingOpen)
    {
        _shakerStateMachine = shakerStateMachine;
        _rotationSpeed = rotationSpeed;
        _liquidPrefab = liquidPrefab;
        _spawnPoint = spawnPoint;
        _liquidManager = liquidManager;
        _increaseScale = increaseScale;
        _isInWorkSpace = isInWorkSpace;
        _initScale = initScale;
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

            _timeSinceLastPour += Time.deltaTime;

            if (_currentRotation <= -_minRotationToPourLiquid)
            {
                CalculateSpawnerPosition();
                PourLiquid();
            }
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
    private void CalculateSpawnerPosition()
    {
        if (_currentRotation < -90f)
        {
            float spawnerMovement = _spawnerPositionX + (_currentRotation + _minRotationToMoveSpawner) * (0 - _spawnerPositionX / (-_maxRotationToMoveSpawner + _minRotationToMoveSpawner));
            _spawnPoint.localPosition = new Vector2(spawnerMovement, _spawnPoint.localPosition.y);
        }
        else
        {
            _spawnPoint.localPosition = new Vector2(_spawnerPositionX, _spawnPoint.localPosition.y);
        }
    }

    private void PourLiquid()
    {
        float currentLiquid = (_liquidManager.GetCurrentLiquid() * 100) / _liquidManager.GetMaxLiquid();
        float currentRotation = 100 - ((-_currentRotation * 100) / _maxRotationToPourLiquid);

        //Debug.Log("Current Liquid: " + currentLiquid + "%");
        //Debug.Log("Current Rot: " + currentRotation + "%");

        float difference = Mathf.Abs(currentLiquid - currentRotation);
        float spawnSpeed = 0.5f;

        if (currentRotation <= currentLiquid)
        {
            if (difference > 0)
            {
                spawnSpeed = 0.5f / difference;
            }

            float pouringInterval = Mathf.Lerp(0f, 1f, spawnSpeed);

            if (_timeSinceLastPour >= pouringInterval)
            {
                GameObject liquid = GameObject.Instantiate(_liquidPrefab, _spawnPoint.position, Quaternion.identity);

                _liquidManager.DeacreaseCurrentLiquid();

                _timeSinceLastPour = 0;
            }
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("WorkSpace") && !_isInWorkSpace)
        {
            _isInWorkSpace = true;
            _shakerStateMachine.transform.localScale *= _increaseScale;
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WorkSpace") && _isInWorkSpace)
        {
            _isInWorkSpace = false;
            _shakerStateMachine.transform.localScale = _initScale;
        }
    }
}