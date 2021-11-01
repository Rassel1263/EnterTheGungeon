using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : IState
{
    Enemy enemy;
    Vector2 force;

    public EnemyHit(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.enemy = (Enemy)unit;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.state = Enemy.EnemyState.Hit;
        enemy.ani.SetInteger("state", (int)enemy.state);

        enemy.rigid.velocity = Vector2.zero;
        force = enemy.hitVec.normalized * 50;
        enemy.rigid.AddForce(force, ForceMode2D.Impulse);
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //if (Mathf.Abs(enemy.rigid.velocity.x) <= 10 && Mathf.Abs(enemy.rigid.velocity.y) <= 10)
        //    enemy.rigid.velocity = Vector2.zero;
        //else
        //    enemy.rigid.velocity -= force * Time.deltaTime * 2;

        enemy.rigid.velocity = Vector2.Lerp(enemy.rigid.velocity, new Vector2(0, 0), Time.deltaTime * 2);

        //Debug.Log(new Vector2(Mathf.Abs(enemy.rigid.velocity.x), Mathf.Abs(enemy.rigid.velocity.y)));
    }

    public override void Update()
    {
        base.Update();

        if ((Mathf.Abs(enemy.rigid.velocity.x) <= 10 && Mathf.Abs(enemy.rigid.velocity.y) <= 10))
        {
            stateMachine.SetState(enemy.idleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.hit = false;
    }
}
