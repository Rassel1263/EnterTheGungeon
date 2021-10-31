using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    Player player;
    public MoveState(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.player = (Player)unit;
    }

    public override void Enter()
    {
        base.Enter();

        player.state = Player.PlayerState.Move;
        player.ani.SetInteger("state", (int)player.state);
    }
    public override void Update()
    {
        base.Update();

        player.LookAtPointer();

        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.SetState(player.rollState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!player.Move())
        {
            stateMachine.SetState(player.idleState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
