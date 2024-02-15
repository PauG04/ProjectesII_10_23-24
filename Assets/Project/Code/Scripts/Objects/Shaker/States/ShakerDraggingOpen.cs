using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShakerDraggingOpen : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;
    private ShakerStateMachine _shakerStateMachine;

    private Vector3 _offset;

    #region Rotation Variables
    private float _rotationSpeed = 20f;
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

    private float _minRotationToPourLiquid = 70f;
    private float _maxRotationToPourLiquid = 140f;

    private float _minRotationToMoveSpawner = 90f;
    private float _maxRotationToMoveSpawner = 180f;

    private float _timeSinceLastPour = 0f;
    #endregion

    private float _scaleMultiplier = 1.5f;

    private Collider2D _workspace;

    private Image _color;
    private Image _background;
    private float velocityColor = 5;

    public ShakerDraggingOpen(
        ShakerStateMachine shakerStateMachine, 
        GameObject liquidPrefab, 
        Transform spawnPoint, 
        LiquidManager liquidManager,
        Collider2D workspace,
        Image color,
        Image background

    ) : base(ShakerStateMachine.ShakerState.DraggingOpen)
    {
        _shakerStateMachine = shakerStateMachine;
        _liquidPrefab = liquidPrefab;
        _spawnPoint = spawnPoint;
        _liquidManager = liquidManager;
        _workspace = workspace;
        _color = color;
        _background = background;
    }

    public override void EnterState()
    {
        _shakerStateMachine.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        _isRotating = true;
        _state = ShakerStateMachine.ShakerState.DraggingOpen;
        _offset = _shakerStateMachine.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public override void ExitState()
    {
        _shakerStateMachine.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        _isRotating = false;

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
        AlphaLerp();

        //if (Input.GetMouseButtonDown(1))
        //{
        //    _isRotating = true;
        //}

        //if (Input.GetMouseButtonUp(1))
        //{
        //    _isRotating = false;
        //}

        if (_isRotating)
        {
            RotateObject();

            _timeSinceLastPour += Time.deltaTime;
            CalculateSpawnerPosition();

            if (_currentRotation <= -_minRotationToPourLiquid || _currentRotation >= _minRotationToPourLiquid)
            {
                
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
    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }
    public override void OnTriggerExit2D(Collider2D collision)
    {

    }
    private void CalculatePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _shakerStateMachine.transform.position = new Vector3(
            mousePosition.x + _offset.x,
            mousePosition.y + _offset.y,
            0
        );

        if (_workspace.OverlapPoint(mousePosition))
        {
            InsideWorkspace();
        }
        else
        {
            OutsideWorkspace();
            _shakerStateMachine.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }

    }
    private void RotateObject()
    {
        float mouseY = Input.mouseScrollDelta.y;
        
        _targetRotation += mouseY * _rotationSpeed;
        _targetRotation = Mathf.Clamp(_targetRotation, -_maxRotation, _maxRotation);

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
        float spawnerMovement;

        if (_currentRotation < -90f)
        {
            spawnerMovement = _spawnerPositionX + (_currentRotation + _minRotationToMoveSpawner) * (-_spawnerPositionX / (-_maxRotationToMoveSpawner + _minRotationToMoveSpawner));
            SetSpawnPointPosition(spawnerMovement);
        }
        else if(_currentRotation > 90f)
        {
            spawnerMovement = _spawnerPositionX + (_currentRotation - _minRotationToMoveSpawner) * (_spawnerPositionX / (-_maxRotationToMoveSpawner + _minRotationToMoveSpawner));
            SetSpawnPointPosition(-spawnerMovement);
        }
        else if (_currentRotation < 0f) 
        {
            SetSpawnPointPosition(_spawnerPositionX);
        }
        else if (_currentRotation > 0f)
        {
            SetSpawnPointPosition(-_spawnerPositionX);
        }
    }
    private void PourLiquid()
    {
        float currentLiquid = (_liquidManager.GetCurrentLiquid() * 100) / _liquidManager.GetMaxLiquid();
        float currentRotation;
        
        if (_currentRotation <= -_minRotationToPourLiquid)
        {
            currentRotation = 100 - ((-_currentRotation * 100) / _maxRotationToPourLiquid);
        }
        else if (_currentRotation >= _minRotationToPourLiquid)
        {
            currentRotation = 100 - ((_currentRotation * 100) / _maxRotationToPourLiquid);
        }
        else
        {
            currentRotation = 0;
        }
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

            if (_timeSinceLastPour >= pouringInterval && _liquidManager.GetCurrentLiquid() > 0)
            {
                GameObject liquid = GameObject.Instantiate(_liquidPrefab, _spawnPoint.position, Quaternion.identity);

                _liquidManager.DeacreaseCurrentLiquid();

                _timeSinceLastPour = 0;
            }
        }
    }
    private void AlphaLerp()
    {
        Color newColor = _color.color;
        newColor.a = Mathf.Lerp(_color.color.a, 0, Time.deltaTime * velocityColor);
        _color.color = newColor;
        _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, newColor.a);
    }
    private void InsideWorkspace()
    {
        _shakerStateMachine.SetGetInWorkSpace(true);
        InsideWorkspaceRenderersChilds(_shakerStateMachine.transform);

        _shakerStateMachine.transform.localScale = new Vector2(_scaleMultiplier, _scaleMultiplier);
    }
    private void OutsideWorkspace()
    {
        _shakerStateMachine.SetGetInWorkSpace(false);

        OutsidewWorkspaceRenderersChilds(_shakerStateMachine.transform);

        _shakerStateMachine.transform.localScale = Vector3.one;
    }

    private void InsideWorkspaceRenderersChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }

            InsideWorkspaceRenderersChilds(child);
        }
    }
    private void OutsidewWorkspaceRenderersChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }

            OutsidewWorkspaceRenderersChilds(child);
        }
    }
    private void SetSpawnPointPosition(float x)
    {
        _spawnPoint.localPosition = new Vector2(x, _spawnPoint.localPosition.y);
    }
}
