using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : IState
{
    Player player;

    Vector2 force;
    public RollState(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.player = (Player)unit;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(player.state.ToString());

        player.state = Player.PlayerState.Roll;
        player.ani.SetInteger("state", (int)player.state);
        player.SetAniDir(player.moveDir);
        player.weapon.SetActive(false);

        player.rigid.velocity = Vector2.zero;
        force = player.moveDir.normalized * 250;
        player.rigid.AddForce(force, ForceMode2D.Impulse);
    }
    public override void Update()
    {
        base.Update();

        if (player.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            stateMachine.SetState(player.idleState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if ((Mathf.Abs(player.rigid.velocity.x) <= 0 && Mathf.Abs(player.rigid.velocity.y) <= 0) || player.checkWall)
            player.rigid.velocity = Vector2.zero;
        else
            player.rigid.velocity -= force * (Time.deltaTime * 1.46f);
    }

    public override void Exit()
    {
        base.Exit();

        player.weapon.SetActive(true);
    }
}
