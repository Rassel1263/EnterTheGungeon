using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    Player player;
    bool drawFade;

    public DieState(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        this.player = (Player)unit;
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0.0f;
        player.state = Player.PlayerState.Die;
        player.ani.SetInteger("state", (int)player.state);
        player.ani.updateMode = AnimatorUpdateMode.UnscaledTime;
        player.rigid.velocity = Vector2.zero;

        player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 7;

        player.weapon.SetActive(false);
        player.StartCoroutine(player.Fade());
    }
    public override void Update()
    {
        base.Update();

        Debug.Log("Update");

        if (player.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !drawFade)
        {
            drawFade = true;
            player.StartCoroutine(player.playerUI.DrawGameOver());
            Debug.Log("Fade");
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
