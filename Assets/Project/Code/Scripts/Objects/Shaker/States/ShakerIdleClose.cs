using UnityEngine;

public class ShakerIdleClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;
    private LayerMask _layerMask;

    private float lerpSpeed = 1.0f;
    public ShakerIdleClose(ShakerStateMachine shakerStateMachine, SetTopShaker shakerClosed, LayerMask layerMask) : base(ShakerStateMachine.ShakerState.IdleClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
        _layerMask = layerMask;
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
                    _state = ShakerStateMachine.ShakerState.DraggingClosed;
                }
            }
        }
    }


    private void ResetObjectPosition()
    {
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, lerpSpeed * Time.deltaTime);
    }
}
