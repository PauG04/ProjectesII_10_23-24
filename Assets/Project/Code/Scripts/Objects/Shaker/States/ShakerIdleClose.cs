using UnityEngine;
using UnityEngine.UI;

public class ShakerIdleClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;
    private LiquidManager _liquidManager;
    private Rigidbody2D _rb;

    private float lerpSpeed = 1.0f;

    private bool firstLerp;
    private bool secondLerp;

    private float velocityX = 6.0f;
    private float velocityY = 10.0f;

    private Vector3 _initPosition;

    private Image _color;
    private Image _background;
    private float velocityColor = 5;
    private float velocityColorPositive = 25;

    private Collider2D _workSpace;

    public ShakerIdleClose(
        ShakerStateMachine shakerStateMachine, 
        SetTopShaker shakerClosed, 
        Vector3 initPosition, 
        Collider2D workSpace,
        Image color,
        Image background,
        LiquidManager liquidManager
    ) : base(ShakerStateMachine.ShakerState.IdleClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
        _initPosition = initPosition;
        _workSpace = workSpace;
        _color = color;
        _background = background;
        _liquidManager = liquidManager;
    }
    public override void EnterState()
    {
        _rb = _shakerStateMachine.GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;

        _shakerClosed.SetStayClosed(false);

        _shakerStateMachine.GetComponent<TargetJoint2D>().enabled = false;

        _state = ShakerStateMachine.ShakerState.IdleClosed;
        if(!_shakerStateMachine.GetIsInWorkSpace()) 
        {
            firstLerp = true;
            secondLerp = false;
        }

        _rb.bodyType = RigidbodyType2D.Dynamic;
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
        firstLerp = false;
        secondLerp = false;
    }
    public override void OnMouseUp()
    {
        
    }
    public override void UpdateState()
    {

        if (!_workSpace.OverlapPoint(_shakerStateMachine.transform.position))
        {
            _rb.bodyType = RigidbodyType2D.Static;
            OutsideWorkspace();
        }
        else
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }

        MoveObjectToParent();

        if (!_shakerClosed.GetIsShakerClosed())
        {
            _state = ShakerStateMachine.ShakerState.IdleOpen;
        }

        if (_shakerStateMachine.transform.rotation != Quaternion.identity)
        {
            ResetObjectPosition();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            LayerMask layerMask = 1 << LayerMask.NameToLayer("ShakerLayer");

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                Rigidbody2D rigidbody2D = hit.collider.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    _shakerClosed.SetStayClosed(true);
                    _state = ShakerStateMachine.ShakerState.DraggingClosed;
                }
            }
        }

        if ((_liquidManager.GetCurrentLiquid() == 0 && _shakerStateMachine.GetProgress() > 0) || _shakerStateMachine.GetReset())
        {
            _shakerStateMachine.ResetShaker(_shakerStateMachine.GetProgress() - 0.2f);
            AlphaLerpPositive();
        }
        else
        {
            AlphaLerp();
        }
    }

    private void AlphaLerp()
    {
        Color newColor = _color.color;
        newColor.a = Mathf.Lerp(_color.color.a, 0, Time.deltaTime * velocityColor);
        _color.color = newColor;
        _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, newColor.a);
    }
    private float AlphaLerpPositive()
    {
        Color newColor = _color.color;
        newColor.a = Mathf.Lerp(_color.color.a, 1, Time.deltaTime * velocityColorPositive);

        _color.color = newColor;
        _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, newColor.a);

        return newColor.a;
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {

    }
    private void MoveObjectToParent()
    {
        if (!_shakerStateMachine.GetIsInWorkSpace())
        {
            if (firstLerp)
            {
                Vector3 newPosition = _shakerStateMachine.transform.localPosition;
                newPosition.x = Mathf.Lerp(_shakerStateMachine.transform.localPosition.x, _initPosition.x, Time.deltaTime * velocityX);

                _shakerStateMachine.transform.localPosition = newPosition;
            }
            if (_shakerStateMachine.transform.localPosition.x > _initPosition.x - 0.002 && _shakerStateMachine.transform.localPosition.x < _initPosition.x + 0.002)
            {
                firstLerp = false;
                secondLerp = true;
            }

            if (secondLerp)
            {
                Vector3 newPosition = _shakerStateMachine.transform.localPosition;
                newPosition.y = Mathf.Lerp(_shakerStateMachine.transform.localPosition.y, _initPosition.y, Time.deltaTime * velocityY);

                _shakerStateMachine.transform.localPosition = newPosition;
            }
            if (_shakerStateMachine.transform.localPosition.y > _initPosition.y - 0.002 && _shakerStateMachine.transform.localPosition.y < _initPosition.y + 0.002)
            {
                secondLerp = false;
                _state = ShakerStateMachine.ShakerState.IdleOpen;
            }
        }
    }
    private void ResetObjectPosition()
    {
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, lerpSpeed * Time.deltaTime);
    }
    private void OutsideWorkspace()
    {
        _shakerStateMachine.SetGetInWorkSpace(false);

        OutsidewWorkspaceRenderersChilds(_shakerStateMachine.transform);

        _shakerStateMachine.transform.localScale = Vector3.one;
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
}
