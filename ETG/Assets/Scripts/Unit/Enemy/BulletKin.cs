using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKin : Enemy
{
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    GameObject hand;

    public override void Start()
    {
        base.Start();

        ability.SetAbility(15, 30);
        detectRange = 120;

        StartCoroutine(ShootDelay());
    }


    IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);

            Transform target = GameObject.Find("Player").transform;
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= 300)
                weapon.GetComponent<EnemyGun>().ShootReady(dir, 200, 1, 1, 0);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        SetHandPos();
    }

    public override void DieEnter()
    {
        base.DieEnter();

        hand.SetActive(false);
        StopCoroutine(ShootDelay());
    }

    public override void DieLogic()
    {
        base.DieLogic();

        if(ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GameObject particle = Instantiate(dieParticle);
            particle.transform.position = (transform.position + new Vector3(0, 15, 0));

            DieExit();
        }
    }

    public override void DieExit()
    {
        int rand = Random.Range(2, 4);

        for (int i = 0; i < rand; ++i)
        {
            Instantiate(scoreParticle, transform.position, Quaternion.identity);
        }

        base.DieExit();
    }

    void SetHandPos()
    {
        float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        hand.transform.eulerAngles = new Vector3(0, 0, degree);

        float prevScaleY = hand.transform.localScale.y;
        hand.transform.localScale = new Vector3(1, (dir.x > 0.0) ? 1 : -1, 0);

        if (prevScaleY != hand.transform.localScale.y)
            hand.transform.localPosition = new Vector3(hand.transform.localScale.y * 11, 7, 0);
    }
}
