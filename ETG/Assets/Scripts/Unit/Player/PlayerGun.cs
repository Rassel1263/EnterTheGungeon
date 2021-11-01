using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    Transform gun;
    [SerializeField]
    Transform muzzle;

    [SerializeField]
    GameObject bulletPrefab;
   

    Animator ani;

    Vector2 lookDir;

    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RenderSetting();
        Shoot();
    }

    void RenderSetting()
    {
        lookDir = GetComponentInParent<Player>().lookDir;
        float degree = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        gun.eulerAngles = new Vector3(0, 0, degree);

        float prevScaleY = gun.localScale.y;
        gun.localScale = new Vector3(1, (lookDir.x > 0.0) ? 1 : -1, 0);

        if(prevScaleY != gun.localScale.y)
            transform.localPosition = new Vector3(gun.localScale.y * 11, 7, 0);
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && !ani.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {

            GetComponent<AudioSource>().Play();

            ani.SetTrigger("shoot");
            GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(gun.eulerAngles));

            Vector2 dir = GameManager.Instance.mouse.mousePos- new Vector2(muzzle.transform.position.x, muzzle.transform.position.y);
            dir.Normalize();

            bullet.GetComponent<Bullet>().SetInfo(dir, 200, 5, Bullet.Team.Player);
        }
    }
}
