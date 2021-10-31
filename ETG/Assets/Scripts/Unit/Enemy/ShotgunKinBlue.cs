using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunKinBlue : Enemy
{
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    GameObject hand;

    [SerializeField]
    GameObject bulletPrefab;


    public override void Start()
    {
        base.Start();

        ability.SetAbility(30, 50);
        detectRange = 120;

        StartCoroutine(ShootDelay());
    }


    IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            Transform target = GameObject.Find("Player").transform;
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= 300)
                weapon.GetComponent<EnemyGun>().ShootReady(dir, 200, 1, 5, 60);
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

        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GameObject particle = Instantiate(dieParticle);
            particle.transform.position = (transform.position + new Vector3(0, 15, 0));

            for(int i = 0; i < 6; ++i)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = (transform.position + new Vector3(0, 15, 0));

                bullet.GetComponent<Bullet>().SetInfo(i * 60, 200, 1, Bullet.Team.Enemy);

            }
            Destroy(gameObject);
        }
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
