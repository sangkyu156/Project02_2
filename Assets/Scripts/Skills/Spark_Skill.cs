using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Spark_Skill : ProjectileSkill, IPoolObject
{
    float sparkSpeed = 0;
    public string idName;

    Animator animator;
    SpriteRenderer playerFlip;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    BoxCollider2D skillCollider;

    private void Awake()
    {
        SetAbility();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        skillCollider = GetComponent<BoxCollider2D>();
        playerFlip = Player.Instance.GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        sparkSpeed = 22;
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

    //레벨에 따라서 능력치 세팅
    public override void SetAbility()
    {
        switch (Player.Instance.sparkLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 10;
                nextPower = 12;
                nextCooldown = 0.4f;
                break;
            case 2:
                curPower = 12;
                nextPower = 14;
                nextCooldown = 0.4f;
                break;
            case 3:
                curPower = 14;
                nextPower = 16;
                nextCooldown = 0.3f;
                break;
            case 4:
                curPower = 16;
                nextPower = 18;
                nextCooldown = 0.3f;
                break;
            case 5:
                curPower = 20;
                nextPower = 22;
                nextCooldown = 0.3f;
                break;
            case 6:
                curPower = 22;
                nextPower = 25;
                nextCooldown = 0.2f;
                break;
            case 7:
                curPower = 25;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }

    //오른쪽으로 발사
    public override void RightShoot()
    {
        if (animator.GetBool("hit") == false)
            rigidbody2D.velocity = new Vector3(sparkSpeed, 0, 0);
        else if (animator.GetBool("hit") == true)
            rigidbody2D.velocity = Vector3.zero;
    }

    //왼쪽으로 발사
    public override void LeftShoot()
    {
        if (animator.GetBool("hit") == false)
        {
            rigidbody2D.velocity = new Vector3(-sparkSpeed, 0, 0);
            spriteRenderer.flipX = true;
        }
        else if (animator.GetBool("hit") == true)
            rigidbody2D.velocity = Vector3.zero;
    }

    //오브젝트 회수
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
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
        animator.SetBool("hit", false);
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

    //에너미랑 부딪쳤을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            animator.SetBool("hit", true);
            skillCollider.enabled = false;
            EnemyBase enemy;

            enemy = collision.GetComponent<EnemyBase>();
            enemy.TakeDamage(curPower + Player.Instance.playerPower);


            if (deadTiem > 0.33f)
                Invoke("OnTargetReached", 0.33f);//이펙트 애니메이션 길이 만큼 기다렸다 회수
        }
    }
}
