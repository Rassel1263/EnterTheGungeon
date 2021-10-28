using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    Player player;
    public IdleState(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.player = (Player)unit;
    }

    public override void Enter()
    {
        base.Enter();

        player.state = Player.PlayerState.Idle;
        player.ani.SetInteger("state", (int)player.state);

        Debug.Log(player.state.ToString());
    }
    public override void Update()
    {
        base.Update();

        player.LookAtPointer();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.Move())
        {
            stateMachine.SetState(player.moveState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
