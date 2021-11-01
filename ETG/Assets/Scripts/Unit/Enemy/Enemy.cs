using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public EnemyIdle idleState;
    public EnemyMove moveState;
    public EnemyHit hitState;
    public EnemyDie dieState;

    public Collider2D range;

    public GameObject dieParticle;
    public GameObject scoreParticle;
    public GameObject bigScoreParticle;

    public Vector2 dir;

    public Vector2 hitVec;

    //bool detect;
    
    protected float detectRange;

    public enum EnemyState
    {
        Idle,
        Move,
        Hit,
        Die
    }
    public EnemyState state { get; set; }

    public override void Start()
    {
        base.Start();

        idleState = new EnemyIdle(this, stateMachine);
        moveState = new EnemyMove(this, stateMachine);
        hitState = new EnemyHit(this, stateMachine);
        dieState = new EnemyDie(this, stateMachine);

        stateMachine.Init(idleState);
    }
 
    public override void FixedUpdate()
    {
        SetDir();
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (ability.hp <= 0)
        {
            stateMachine.SetState(dieState);
            return;
        }
    }

    public override bool Move()
    {
        //if (detect)
        //   return false;

        Transform target = GameObject.Find("Player").transform;
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectRange)
            return false;

        rigid.velocity = dir * ability.speed;

        return true;
    }

   
    public virtual void DieEnter() { }
    public virtual void DieLogic() { }
    public virtual void DieExit()  
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Player")
        //    detect = true;


        if(collision.tag == "PlayerBullet")
        {
            ability.hp -= collision.gameObject.GetComponentInParent<Bullet>().damage;

            hit = true;
        }

        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.tag == "Player")
        //    detect = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            ability.hp -= 1;

            if (!hit)
            {
                hit = true;
                hitVec = collision.transform.position - transform.position;
            }
        }
    }

    void SetDir()
    {
        Transform target = GameObject.Find("Player").transform;

        dir = target.position - transform.position;
        dir.Normalize();
    }

}
