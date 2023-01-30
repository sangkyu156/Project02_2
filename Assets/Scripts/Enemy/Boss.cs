using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Singleton<Boss>
{
    Rigidbody2D rigidbody;
    Animator animator;
    public float bossSpeed = 7;
    bool attack = false;
    Vector3 moveDirectione;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bossSpeed = 7;
    }

    void Update()
    {
        if(attack == false)
            BossMove();
    }

    void BossMove()
    {
        moveDirectione = new Vector3(1, 0);
        rigidbody.velocity = moveDirectione * bossSpeed;
    }

    void BossAttackOff()
    {
        attack = false;
        animator.SetBool("Attack", false);
        //게임 오버 팝업
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "NoDamage")
        {
            animator.SetBool("Attack", true);
            Invoke("BossAttackOff", 0.6f);
        }
    }
}
