﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsTranistioningState = false;

    void Start()
    {
        CurrentState.EnterState();
    }
    void Update()
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
    void OnMouseDown()
    {
        CurrentState.OnMouseDown();
    }
    private void OnMouseDrag()
    {
        CurrentState.OnMouseDrag();
    }
    void OnMouseUp()
    {
        CurrentState.OnMouseUp();
    }
   
}
