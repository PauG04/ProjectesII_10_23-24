using Unity.VisualScripting;
using UnityEngine;

public class ShakerIdleOpen : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;

    private float _lerpSpeed = 10f;

    private bool firstLerp;
    private bool secondLerp;

    private float velocityX = 6.0f;
    private float velocityY = 10.0f;

    private Vector3 _initPosition;

    private Collider2D _workSpace;

    public ShakerIdleOpen(
        ShakerStateMachine shakerStateMachine, 
        SetTopShaker shakerClosed,
        Vector3 initPosition,
        Collider2D workSpace
    ) : base(ShakerStateMachine.ShakerState.IdleOpen)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
        _initPosition = initPosition;
        _workSpace = workSpace;
    }
    public override void EnterState()
    {
        _shakerStateMachine.GetComponent<TargetJoint2D>().enabled = false;
        _state = ShakerStateMachine.ShakerState.IdleOpen;

        if (!_shakerStateMachine.GetIsInWorkSpace())
        {
            firstLerp = true;
            secondLerp = false;
        }
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
        _state = ShakerStateMachine.ShakerState.DraggingOpen;
    }
    public override void OnMouseUp()
    {
        
    }
    public override void UpdateState()
    {
        if (!_workSpace.OverlapPoint(_shakerStateMachine.transform.position))
        {
            _shakerStateMachine.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            OutsideWorkspace();
        }
        else
        {
            _shakerStateMachine.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        MoveObjectToParent();
        if (_shakerClosed.GetIsShakerClosed())
        {
            _state = ShakerStateMachine.ShakerState.IdleClosed;
        }

        if (_shakerStateMachine.transform.rotation != Quaternion.identity)
        {
            ResetObjectPosition();
        }
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
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, _lerpSpeed * Time.deltaTime);
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
                renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }

            OutsidewWorkspaceRenderersChilds(child);
        }
    }
}
