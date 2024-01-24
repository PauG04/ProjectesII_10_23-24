public class ShakerIdleClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    public ShakerIdleClose() : base(ShakerStateMachine.ShakerState.IdleClosed)
    {
        
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
        
    }
}
