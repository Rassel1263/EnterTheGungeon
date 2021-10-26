using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;

    Vector2 MoveDir;

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtPointer();
        SetAni();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveDir = new Vector2(h, v);
        transform.Translate(new Vector2(h, v) * Time.deltaTime * 100);
    }

    void LookAtPointer()
    {
        Vector2 lookDir = GameManager.Instance.mouse.transform.position - transform.position;
        lookDir.Normalize();

        Debug.Log(lookDir);

        MoveDir = lookDir;
    }

    void SetAni()
    {
        animator.SetFloat("dirX", MoveDir.x);
        animator.SetFloat("dirY", MoveDir.y);
    }
}
