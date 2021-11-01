using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    Transform muzzle;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float attackSpeed;

    Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }


    public void ShootReady(Vector2 dir, float speed, int damage, int bulletCnt, float angleRagne)
    {
        ani.SetTrigger("shoot");

        if(gameObject.activeInHierarchy)
            StartCoroutine(Shoot(dir, speed, damage, bulletCnt, angleRagne));
    }

    public IEnumerator Shoot(Vector2 dir, float speed, int damage, int bulletCnt, float angleRange)
    {
        yield return new WaitForSeconds(attackSpeed);

        float bulletInterval = angleRange / bulletCnt;

        for (int i = 0; i < bulletCnt; ++i)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + bulletInterval * i + (bulletInterval / 2 - angleRange / 2);

            GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(transform.eulerAngles));
            bullet.GetComponent<Bullet>().SetInfo(angle, speed, damage, Bullet.Team.Enemy);
        }
    }
}
