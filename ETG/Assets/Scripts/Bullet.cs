using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField]
    GameObject particle;

    Vector2 dir;

    public enum Team
    {
        Player,
        Enemy
    }

    public Team team;
    public int damage;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float camHeight = Camera.main.orthographicSize + 20;
        float camWidth = camHeight * Camera.main.aspect + 20;

        Vector2 camPos = Camera.main.transform.position;

        if (transform.position.x > camPos.x + camWidth || transform.position.x < camPos.x - camWidth ||
            transform.position.y > camPos.y + camHeight || transform.position.y < camPos.y - camHeight)
        {
            Destroy(gameObject);
        }

    }

    public void SetInfo(Vector2 dir, float speed, int damage, Team team)
    {
        this.dir = dir;
        rigid.AddForce(dir * speed, ForceMode2D.Impulse);
        this.damage = damage;
        this.team = team;
    }

    public void SetInfo(float angle, float speed, int damage, Team team)
    {
        angle *= Mathf.Deg2Rad;
        this.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rigid.AddForce(dir * speed, ForceMode2D.Impulse);
        this.damage = damage;
        this.team = team;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && team != Team.Enemy)
        {
            GameObject fx = Instantiate(particle);
            fx.transform.position = transform.position;
            Destroy(gameObject);

            collision.gameObject.GetComponentInParent<Enemy>().hitVec = dir;
        }

        if (collision.tag == "Player" && team != Team.Player)
        {
            if (collision.gameObject.GetComponentInParent<Player>().state == Player.PlayerState.Roll)
                return;

            GameObject fx = Instantiate(particle);
            fx.transform.position = transform.position;
            Destroy(gameObject);
        }

        if (collision.tag == "Wall")
        {
            GameObject fx = Instantiate(particle);
            fx.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
