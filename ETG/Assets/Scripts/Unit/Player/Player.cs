using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update

    public Vector2 MoveDir;

    public IdleState idleState;
    public MoveState moveState;
    public RollState rollState;

    public enum PlayerState
    {
        Idle,
        Move,
        Roll,
    } 
    
    public PlayerState state {get; set;}

    public override void Start()
    {
        base.Start();

        idleState = new IdleState(this, stateMachine);
        moveState = new MoveState(this, stateMachine);
        rollState = new RollState(this, stateMachine);

        stateMachine.Init(idleState);
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {
        base.FixedUpdate();
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

    public void LookAtPointer()
    {
        Vector2 lookDir = GameManager.Instance.mouse.transform.position - transform.position;
        lookDir.Normalize();

        SetAniDir(lookDir);
    }

    public void SetAniDir(Vector2 direction)
    {
        ani.SetFloat("dirX", direction.x);
        ani.SetFloat("dirY", direction.y);
    }
}
