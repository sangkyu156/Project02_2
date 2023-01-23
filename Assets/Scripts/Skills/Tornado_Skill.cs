using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado_Skill : ProjectileSkill, IPoolObject
{
    float fireballSpeed = 0;
    public string idName;

    SpriteRenderer playerFlip;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    BoxCollider2D skillCollider;

    private void Awake()
    {
        SetAbility();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skillCollider = GetComponent<BoxCollider2D>();
        playerFlip = Player.Instance.GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        fireballSpeed = 15;
        deadTiem = 0.8f;
    }

    void Update()
    {
        if (rigidbody2D != null)
        {
            if (first == false)
            {
                first = true;
                if (playerFlip.flipX == true)
                    right = true;
                else
                    left = true;
            }
        }

        if (right == true)
        {
            RightShoot();
        }

        if (left == true)
        {
            LeftShoot();
        }

        if (deadTiem > 0)
        {
            deadTiem -= Time.deltaTime;
        }

        if (deadTiem < 0)
        {
            deadTiem = 0.8f;
            OnTargetReached();
        }
    }

    //에너미랑 부딪쳤을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Attack(collision);
    }

    void Attack(Collider2D collider_)
    {
        EnemyBase enemy;
        enemy = collider_.GetComponent<EnemyBase>();

        if(enemy.knockback == false)
        {
            if(collider_.transform.position.x < transform.position.x)
                collider_.transform.position = new Vector3(collider_.transform.position.x - 2f, collider_.transform.position.y);
            else if(collider_.transform.position.x > transform.position.x)
                collider_.transform.position = new Vector3(collider_.transform.position.x + 2f, collider_.transform.position.y);

            enemy.TakeDamage(curPower);
            enemy.knockback = true;
        }
    }

    public override void RightShoot()
    {
        rigidbody2D.velocity = new Vector3(fireballSpeed, 0, 0);
    }

    public override void LeftShoot()
    {
        rigidbody2D.velocity = new Vector3(-fireballSpeed, 0, 0);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        first = false;
        right = false;
        left = false;
        spriteRenderer.flipX = false;
        SetAbility();
        if (rigidbody2D != null)
        {
            //생성위치 지정
            if (playerFlip.flipX == true)
                transform.position = Player.Instance.skillPos.transform.position;
            else
                transform.position = Player.Instance.skillPos.transform.position - new Vector3(1.57f, 0, 0);
        }
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        first = false;
        right = false;
        left = false;
        spriteRenderer.flipX = false;
        skillCollider.enabled = true;
        deadTiem = 0.8f;
        SetAbility();
        if (rigidbody2D != null)
        {
            //생성위치 지정
            if (playerFlip.flipX == true)
                transform.position = Player.Instance.skillPos.transform.position;
            else
                transform.position = Player.Instance.skillPos.transform.position - new Vector3(1.57f, 0, 0);
        }
    }

    //오브젝트 회수
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
    }


    public override void SetAbility()
    {
        switch (Player.Instance.tornadoLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 1;
                nextPower = 2;
                nextCooldown = 2;
                break;
            case 2:
                curPower = 2;
                nextPower = 3;
                nextCooldown = 1.9f;
                break;
            case 3:
                curPower = 3;
                nextPower = 4;
                nextCooldown = 1.8f;
                break;
            case 4:
                curPower = 4;
                nextPower = 5;
                nextCooldown = 1.7f;
                break;
            case 5:
                curPower = 5;
                nextPower = 6;
                nextCooldown = 1.6f;
                break;
            case 6:
                curPower = 6;
                nextPower = 7;
                nextCooldown = 1.5f;
                break;
            case 7:
                curPower = 7;
                nextPower = 8;
                nextCooldown = 1.3f;
                break;
        }
    }

}
