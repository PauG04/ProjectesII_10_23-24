using UnityEngine;

public class ShakerIdleClose : BaseState<ShakerStateMachine.ShakerState>
{
    private ShakerStateMachine.ShakerState _state;

    private ShakerStateMachine _shakerStateMachine;
    private SetTopShaker _shakerClosed;
    private LayerMask _layerMask;

    private float lerpSpeed = 1.0f;

    private bool firstLerp;
    private bool secondLerp;

    private float velocityX = 6.0f;
    private float velocityY = 10.0f;

    private Vector3 _initPosition;
    private LerpTopShaker _lerp;

    private ProgressSlider _slider;
    public ShakerIdleClose(ShakerStateMachine shakerStateMachine, SetTopShaker shakerClosed, LayerMask layerMask, LerpTopShaker lerp, Vector3 initPosition, ProgressSlider slider) : base(ShakerStateMachine.ShakerState.IdleClosed)
    {
        _shakerStateMachine = shakerStateMachine;
        _shakerClosed = shakerClosed;
        _layerMask = layerMask;
        _initPosition = initPosition;
        _lerp = lerp;
        _slider = slider;
    }

    public override void EnterState()
    {
        _state = ShakerStateMachine.ShakerState.IdleClosed;
        if(!_shakerStateMachine.GetIsInWorkSpace()) 
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
        firstLerp = false;
        secondLerp = false;
    }

    public override void OnMouseUp()
    {
        
    }

    public override void UpdateState()
    {
        MoveObjectToParent();
        _slider.SetIsLerp(false);

        if (!_shakerStateMachine.GetIsInWorkSpace())
        {
            _lerp.startLerp(true);
        }         

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

    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }
    public override void OnTriggerExit2D(Collider2D collision)
    {

    }
    private void ResetObjectPosition()
    {
        _shakerStateMachine.transform.rotation = Quaternion.Lerp(_shakerStateMachine.transform.rotation, Quaternion.identity, lerpSpeed * Time.deltaTime);
    }
}
