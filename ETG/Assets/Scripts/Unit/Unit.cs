using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public StateMachine stateMachine;
    public Rigidbody2D rigid { get; private set; }
    public Animator ani { get; private set; }

    public bool hit;

    protected float hitTime;
    protected float hitTimer;

    protected struct Abilty
    {
        public int hp, maxHp;
        public float speed;

        public void SetAbility(int hp, float speed)
        {
            this.hp = hp;
            maxHp = hp;
            this.speed = speed;
        }
    };

    protected Abilty ability;

    public virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        stateMachine = new StateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Update();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public virtual bool Move()
    {
        return false;
    }

    public void HItManagement()
    {
        if(hit)
        {
            hitTimer += Time.deltaTime;

            if(hitTimer >= hitTime)
            {
                hitTimer = 0.0f;
                hit = false;
            }
        }
    }
}
