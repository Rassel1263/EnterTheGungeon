using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : IState
{
    Enemy enemy;

    public EnemyMove(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.enemy = (Enemy)unit;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.state = Enemy.EnemyState.Move;
        enemy.ani.SetInteger("state", (int)enemy.state);
    }

 
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(!enemy.Move())
        {
            stateMachine.SetState(enemy.idleState);
            return;
        }
    }

    public override void Update()
    {
        base.Update();

        if(enemy.hit)
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


