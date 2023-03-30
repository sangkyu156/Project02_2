using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnergy_Skill : ProjectileSkill, IPoolObject
{
    float waveEnergySpeed = 0;
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
        waveEnergySpeed = 11;
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

        enemy.TakeDamage(curPower + Player.Instance.playerPower);
    }

    public override void RightShoot()
    {
        rigidbody2D.velocity = new Vector3(waveEnergySpeed, 0, 0);
    }

    public override void LeftShoot()
    {
        rigidbody2D.velocity = new Vector3(-waveEnergySpeed, 0, 0);
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
        switch (Player.Instance.waveEnergyLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 7;
                nextPower = 8;
                nextCooldown = 1.6f;
                break;
            case 2:
                curPower = 8;
                nextPower = 9;
                nextCooldown = 1.5f;
                break;
            case 3:
                curPower = 9;
                nextPower = 10;
                nextCooldown = 1.4f;
                break;
            case 4:
                curPower = 10;
                nextPower = 11;
                nextCooldown = 1.3f;
                break;
            case 5:
                curPower = 11;
                nextPower = 12;
                nextCooldown = 1.2f;
                break;
            case 6:
                curPower = 12;
                nextPower = 15;
                nextCooldown = 1.0f;
                break;
            case 7:
                curPower = 15;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }
}
