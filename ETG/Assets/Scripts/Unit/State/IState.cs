using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState
{
    protected StateMachine stateMachine;
    protected Unit unit;

    public IState(Unit unit, StateMachine stateMachine)
    {
        this.unit = unit;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
