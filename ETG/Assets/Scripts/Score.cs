using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    Vector2 targetPos;

    bool grab;

    [SerializeField]
    int score;

    private void Awake()
    {
    }

    void Start()
    {
        targetPos = new Vector2(transform.position.x + Random.Range(-30, 30), transform.position.y + Random.Range(-30, 30));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!grab)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, 0.05f);

            if (Vector2.Distance(targetPos, transform.position) <= 1)
                grab = true;
        }
        else
        {
            targetPos = GameObject.Find("Player").transform.position;
            Vector2 dir = targetPos - new Vector2(transform.position.x, transform.position.y);
            transform.Translate(dir.normalized * 10);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.score += score;
            Destroy(gameObject);
        }
    }
}
