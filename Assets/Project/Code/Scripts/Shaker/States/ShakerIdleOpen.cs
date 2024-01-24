
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ShakerIdleOpen : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;

    private float lerpSpeed = 1.0f;

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

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Rigidbody2D rigidbody2D = hit.collider.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    _state = ShakerStateMachine.ShakerState.DraggingOpen;
                }
            }
        }
    }

    private void ResetObjectPosition()
    {
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, lerpSpeed * Time.deltaTime);
    }
}
