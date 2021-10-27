using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public StateMachine stateMachine;
    public Rigidbody2D rigid { get; private set; }
    public Animator ani { get; private set; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        stateMachine = new StateMachine();
    }

    // Update is called once per frame
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
}
