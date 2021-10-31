using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update

    public Vector2 moveDir;
    public Vector2 lookDir;

    public bool checkWall;

    public IdleState idleState;
    public MoveState moveState;
    public RollState rollState;

    public GameObject weapon;

    [SerializeField]
    PlayerUI playerUI;

    public enum PlayerState
    {
        Idle,
        Move,
        Roll,
    }

    public PlayerState state { get; set; }

    public override void Start()
    {
        base.Start();

        idleState = new IdleState(this, stateMachine);
        moveState = new MoveState(this, stateMachine);
        rollState = new RollState(this, stateMachine);

        ability.SetAbility(3, 100);

        stateMachine.Init(idleState);

        playerUI.DrawHp(ability.hp, ability.maxHp);

        hitTime = 1.0f;
    }

    // Update is called once per frame

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Update()
    {
        base.Update();

        HItManagement();
    }

    public override bool Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(h, v);
        rigid.velocity = new Vector2(h, v) * ability.speed;

        if ((h == 0 && v == 0) || checkWall)
            return false;


        return true;
    }

    public void LookAtPointer()
    {
        lookDir = GameManager.Instance.mouse.transform.position - (transform.position + new Vector3(0, 15, 0));
        lookDir.Normalize();

        SetAniDir(lookDir);
    }

    public void SetAniDir(Vector2 direction)
    {
        ani.SetFloat("dirX", direction.x);
        ani.SetFloat("dirY", direction.y);
    }
    public void Hit(int damage)
    {
        if (hit) return;
        hit = true;

        ability.hp -= damage;

        playerUI.DrawHp(ability.hp, ability.maxHp);
        playerUI.DrawHit();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBullet")
        {
            Hit(collision.gameObject.GetComponentInParent<Bullet>().damage);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            checkWall = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            checkWall = false;
        }
    }

}
