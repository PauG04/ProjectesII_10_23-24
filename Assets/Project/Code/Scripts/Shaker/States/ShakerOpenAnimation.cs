public class ShakerOpenAnimation : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    public ShakerOpenAnimation() : base(ShakerStateMachine.ShakerState.OpenShaker)
    {

    }
    public override void EnterState()
    {
        
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