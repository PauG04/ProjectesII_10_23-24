using UnityEngine.UI;
using UnityEngine;

public class ShakerDraggingClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private TargetJoint2D _targetJoint;
    private Rigidbody2D _rb;

    private Vector2 _newPosition;

    private float _maxAngle = 0.1f;
    private float _progress;
    private float _maxProgress;
    private float _divideProgress;

    private bool _canShake;
    private bool _isDown;

    private float _scaleMultiplier = 2f;
    private LiquidManager _liquidManager;

    //private ProgressSlider _slider;
    private Slider _progressSlider;
    private Image _color;
    private Image _background;
    private float velocityColor = 2;

    private Collider2D _workspace;

    public ShakerDraggingClose(
        ShakerStateMachine shakerStateMachine,
        float progress,
        float maxProgress,
        float divideProgress,
        LiquidManager liquidManager,
        Collider2D workspace,
        Slider progressSlider,
        Image color,
        Image background
    ) : base(ShakerStateMachine.ShakerState.DraggingClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _maxProgress = maxProgress;
        _progress = progress;
        _liquidManager = liquidManager;
        _divideProgress = divideProgress;
        _workspace = workspace;
        _progressSlider = progressSlider;
        _color = color;
        _background = background;
    }
    public override void EnterState()
    {
        _state = ShakerStateMachine.ShakerState.DraggingClosed;

        _targetJoint = _shakerStateMachine.GetComponent<TargetJoint2D>();
        _targetJoint.enabled = true;

        _rb = _shakerStateMachine.GetComponent<Rigidbody2D>();

        _newPosition = _shakerStateMachine.transform.position;

        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.bodyType = RigidbodyType2D.Dynamic;

        _newPosition = _shakerStateMachine.transform.position;
    }
    public override void ExitState()
    {

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _targetJoint.target = mousePosition;

        if (_workspace.OverlapPoint(mousePosition))
        {
            InsideWorkspace();
        }
        else
        {
            OutsideWorkspace();
            _shakerStateMachine.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }

        _rb.SetRotation(Vector2.Dot(_rb.velocity.normalized, Vector2.up) * _rb.velocity.sqrMagnitude * _maxAngle);
        Shaking();
        AlphaLerp();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }
    public override void OnTriggerExit2D(Collider2D collision)
    {

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
            _progress += (_newPosition.y - _shakerStateMachine.transform.position.y) / _divideProgress;
        }
        if(!_isDown)
        {
            _progress += (_shakerStateMachine.transform.position.y - _newPosition.y) / _divideProgress;
        }
        _progressSlider.value = _progress;
        _color.color = new Color (1,1 - (_progress / _maxProgress),0, AlphaLerp());

        //cameraShake.ShakeCamera((transform.position.y - _newPosition.y) * intensityShaking);

    }
    private float AlphaLerp()
    {
        Color newColor = _color.color;
        newColor.a = Mathf.Lerp(_color.color.a, 1, Time.deltaTime * velocityColor);

        _color.color = newColor;
        _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, newColor.a);

        return newColor.a;
    }
    private void DirectionShaker()
    {
        if(_isDown && _rb.velocity.y >= 0)
        {
            _isDown = false;
            _newPosition.y = _shakerStateMachine.transform.position.y;
        }
        if(!_isDown && _rb.velocity.y < 0)
        {
            _isDown = true;
            _newPosition.y = _shakerStateMachine.transform.position.y;
        }
    }
    private void StartShaking()
    {
        if ((_rb.velocity.y >= 0.5 || _rb.velocity.y <= -0.5))
        {
            _canShake = true;
        }
    }
    private void EndClicking()
    {
        if ((_rb.velocity.y <= 0.5 && _rb.velocity.y >= -0.5) || _progress >= _maxProgress)
        {
            _canShake = false;
        }
    }
    private void SetDrinkState()
    {
        if (_progress < _maxProgress * 0.05f)
        {
            _liquidManager.SetDrinkState(CocktailNode.State.Idle);
        }
        else if (_progress >= _maxProgress * 0.95f)
        {
            _liquidManager.SetDrinkState(CocktailNode.State.Mixed);
        }
        else
        {
            _liquidManager.SetDrinkState(CocktailNode.State.Shaked);
        }
    }
    private void InsideWorkspace()
    {
        _shakerStateMachine.SetGetInWorkSpace(true);

        InsideWorkspaceRenderersChilds(_shakerStateMachine.transform);

        _shakerStateMachine.transform.localScale = new Vector2(_scaleMultiplier, _scaleMultiplier);

        if(!_shakerStateMachine.GetWasInTable())
        {
            _shakerStateMachine.SetWasInTable(true);
        }
    }
    private void OutsideWorkspace()
    {
        _shakerStateMachine.SetGetInWorkSpace(false);

        OutsidewWorkspaceRenderersChilds(_shakerStateMachine.transform);

        if (!_shakerStateMachine.GetIsInTutorial())
        {
            _shakerStateMachine.transform.localScale = Vector3.one;
        }          
    }
    private void InsideWorkspaceRenderersChilds(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.sortingLayerName = "WorkSpace";
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
                renderer.sortingLayerName = "Default";
                renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }

            OutsidewWorkspaceRenderersChilds(child);
        }
    }
    public float GetProgress() => _progress;
    public void SetProgress(float progress) => _progress = progress;
    public void SetSlider(float progress) => _progressSlider.value = progress;
}