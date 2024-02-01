using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    private GameObject _parent;

    public ShakerIdleOpen(ShakerStateMachine shakerStateMachine, SetTopShaker shakerClosed, GameObject parent) : base(ShakerStateMachine.ShakerState.IdleOpen)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
        _parent = parent;
    }

    public override void EnterState()
    {
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

    private void MoveObjectToParent()
    {
        if (!_shakerStateMachine.GetIsInWorkSpace())
        {
            if (firstLerp)
            {
                Vector3 newPosition = _shakerStateMachine.transform.localPosition;
                newPosition.x = Mathf.Lerp(_shakerStateMachine.transform.position.x, _parent.transform.position.x, Time.deltaTime * velocityX);

                _shakerStateMachine.transform.position = newPosition;
            }
            if (_shakerStateMachine.transform.position.x > _parent.transform.position.x - 0.02 && _shakerStateMachine.transform.position.x < _parent.transform.position.x + 0.02)
            {
                firstLerp = false;
                secondLerp = true;
            }

            if (secondLerp)
            {
                Vector3 newPosition = _shakerStateMachine.transform.localPosition;
                newPosition.y = Mathf.Lerp(_shakerStateMachine.transform.position.y, _parent.transform.position.y, Time.deltaTime * velocityY);

                _shakerStateMachine.transform.position = newPosition;
            }
            if (_shakerStateMachine.transform.position.y > _parent.transform.position.y - 0.02 && _shakerStateMachine.transform.position.y < _parent.transform.position.y + 0.02)
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

    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }
    public override void OnTriggerExit2D(Collider2D collision)
    {

    }
}
