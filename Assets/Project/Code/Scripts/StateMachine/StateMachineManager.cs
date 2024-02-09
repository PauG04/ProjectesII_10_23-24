using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    public BaseState<EState> CurrentState { get; protected set; }

    protected bool IsTranistioningState = false;

    private void Start()
    {
        CurrentState.EnterState();
    }
    private void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTranistioningState)
        {
            if (nextStateKey.Equals(CurrentState.StateKey))
            {
                CurrentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }
    }
    public void TransitionToState(EState stateKey)
    {
        IsTranistioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTranistioningState = false;
    }
    private void OnMouseDown()
    {
        CurrentState.OnMouseDown();
    }

    private void OnMouseUp()
    {
        CurrentState.OnMouseUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentState.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrentState.OnTriggerExit2D(collision);
    }
}
