
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ShakerIdleOpen : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;

    private float _lerpSpeed = 10f;

    public ShakerIdleOpen(ShakerStateMachine shakerStateMachine, SetTopShaker shakerClosed) : base(ShakerStateMachine.ShakerState.IdleOpen)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
    }

    public override void EnterState()
    {
        _state = ShakerStateMachine.ShakerState.IdleOpen;
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

    public override void OnMouseDrag()
    {
        
    }

    public override void OnMouseUp()
    {
        
    }

    public override void UpdateState()
    {
        if (_shakerClosed.GetIsShakerClosed())
        {
            _state = ShakerStateMachine.ShakerState.IdleClosed;
        }

        if (_shakerStateMachine.transform.rotation != Quaternion.identity)
        {
            ResetObjectPosition();
        }
    }

    private void ResetObjectPosition()
    {
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, _lerpSpeed * Time.deltaTime);
    }
}
