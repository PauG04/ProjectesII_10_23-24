using UnityEngine;

public class ShakerIdleClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;

    private float lerpSpeed = 1.0f;
    public ShakerIdleClose(ShakerStateMachine shakerStateMachine, SetTopShaker shakerClosed) : base(ShakerStateMachine.ShakerState.IdleClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
    }

    public override void EnterState()
    {
        _state = ShakerStateMachine.ShakerState.IdleClosed;
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
        _state = ShakerStateMachine.ShakerState.DraggingClosed;
    }

    public override void OnMouseDrag()
    {
        
    }

    public override void OnMouseUp()
    {
        
    }

    public override void UpdateState()
    {
        if (!_shakerClosed.GetIsShakerClosed())
        {
            _state = ShakerStateMachine.ShakerState.IdleOpen;
        }

        if (_shakerStateMachine.transform.rotation != Quaternion.identity)
        {
            ResetObjectPosition();
        }
    }


    private void ResetObjectPosition()
    {
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, lerpSpeed * Time.deltaTime);
    }
}
