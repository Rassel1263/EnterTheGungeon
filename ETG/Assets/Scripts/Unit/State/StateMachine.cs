using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState { get; private set; }
    
    public void Init(IState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void SetState(IState nextState)
    {
        currentState.Exit();
        currentState = nextState;

        nextState.Enter();

        return;
    }
}
