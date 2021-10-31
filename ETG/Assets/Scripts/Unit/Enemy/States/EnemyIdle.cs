using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : IState
{
    Enemy enemy;
    public EnemyIdle(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.enemy = (Enemy)unit;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.state = Enemy.EnemyState.Idle;
        enemy.ani.SetInteger("state", (int)enemy.state);
        enemy.rigid.velocity = Vector2.zero;
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (enemy.Move())
        {
            stateMachine.SetState(enemy.moveState);
            return;
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.hit)
        {
            stateMachine.SetState(enemy.hitState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
