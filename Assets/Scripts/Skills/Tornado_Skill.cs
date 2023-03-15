using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado_Skill : ProjectileSkill, IPoolObject
{
    float tornadoSpeed = 0;
    public string idName;

    SpriteRenderer playerFlip;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        SetAbility();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerFlip = Player.Instance.GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        tornadoSpeed = 15;
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

    //���ʹ̶� �ε����� ��
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

            enemy.TakeDamage(curPower + Player.Instance.playerPower);
            enemy.knockback = true;
            enemy.KnockbackSet();
        }
    }

    public override void RightShoot()
    {
        rigidbody2D.velocity = new Vector3(tornadoSpeed, 0, 0);
    }

    public override void LeftShoot()
    {
        rigidbody2D.velocity = new Vector3(-tornadoSpeed, 0, 0);
    }

    //ó������������ ����Ǵ� �޼ҵ�
    public void OnCreatedInPool()
    {
        first = false;
        right = false;
        left = false;
        spriteRenderer.flipX = false;
        SetAbility();
        if (rigidbody2D != null)
        {
            //������ġ ����
            if (playerFlip.flipX == true)
                transform.position = Player.Instance.skillPos.transform.position;
            else
                transform.position = Player.Instance.skillPos.transform.position - new Vector3(1.57f, 0, 0);
        }
    }

    //����Ǽ� �ٽ��ѹ� ����ɶ����� ����Ǵ� �޼ҵ�
    public void OnGettingFromPool()
    {
        first = false;
        right = false;
        left = false;
        spriteRenderer.flipX = false;
        deadTiem = 0.8f;
        SetAbility();
        if (rigidbody2D != null)
        {
            //������ġ ����
            if (playerFlip.flipX == true)
                transform.position = Player.Instance.skillPos.transform.position;
            else
                transform.position = Player.Instance.skillPos.transform.position - new Vector3(1.57f, 0, 0);
        }
    }

    //������Ʈ ȸ��
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
                curPower = 9;
                nextPower = 10;
                nextCooldown = 1.9f;
                break;
            case 2:
                curPower = 10;
                nextPower = 11;
                nextCooldown = 1.8f;
                break;
            case 3:
                curPower = 11;
                nextPower = 12;
                nextCooldown = 1.7f;
                break;
            case 4:
                curPower = 12;
                nextPower = 13;
                nextCooldown = 1.6f;
                break;
            case 5:
                curPower = 13;
                nextPower = 14;
                nextCooldown = 1.5f;
                break;
            case 6:
                curPower = 14;
                nextPower = 15;
                nextCooldown = 1.3f;
                break;
            case 7:
                curPower = 15;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }
}
