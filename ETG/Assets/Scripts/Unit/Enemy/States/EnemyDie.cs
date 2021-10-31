using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : IState
{
    Enemy enemy;
    public EnemyDie(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.enemy = (Enemy)unit;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.state = Enemy.EnemyState.Die;
        enemy.ani.SetInteger("state", (int)enemy.state);
        enemy.rigid.velocity = Vector2.zero;

        enemy.DieEnter();
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        enemy.DieLogic();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
