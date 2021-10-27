using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update

    Vector2 MoveDir;

    public IdleState idleState;
    public MoveState moveState;

    public enum PlayerState
    {
        Idle,
        Move,
    } 
    
    public PlayerState state {get; set;}

    public override void Start()
    {
        base.Start();

        idleState = new IdleState(this, stateMachine);
        moveState = new MoveState(this, stateMachine);

        stateMachine.Init(idleState);
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        LookAtPointer();
    }
    public override void Update()
    {
        base.Update();
    }

    public override bool Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveDir = new Vector2(h, v);

        if (h == 0 && v == 0)
            return false;

        transform.Translate(new Vector2(h, v) * Time.deltaTime * 100);

        return true;
    }

    void LookAtPointer()
    {
        Vector2 lookDir = GameManager.Instance.mouse.transform.position - transform.position;
        lookDir.Normalize();

        SetAni(lookDir);
    }

    void SetAni(Vector2 direction)
    {
        ani.SetFloat("dirX", direction.x);
        ani.SetFloat("dirY", direction.y);
    }
}
