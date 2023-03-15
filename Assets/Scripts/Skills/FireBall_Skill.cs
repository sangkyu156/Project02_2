using Redcode.Pools;
using UnityEngine;

public class FireBall_Skill : ProjectileSkill, IPoolObject
{
    float fireballSpeed = 0;
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
        fireballSpeed = 17;
        deadTiem = 1.4f;
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
            deadTiem = 1.4f;
            OnTargetReached();
        }
    }

    //������ ���� �ɷ�ġ ����
    public override void SetAbility()
    {
        switch (Player.Instance.fireBallLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 15;
                nextPower = 19;
                nextCooldown = 1;
                break;
            case 2:
                curPower = 19;
                nextPower = 23;
                nextCooldown = 0.9f;
                break;
            case 3:
                curPower = 23;
                nextPower = 27;
                nextCooldown = 0.9f;
                break;
            case 4:
                curPower = 27;
                nextPower = 31;
                nextCooldown = 0.8f;
                break;
            case 5:
                curPower = 31;
                nextPower = 35;
                nextCooldown = 0.8f;
                break;
            case 6:
                curPower = 35;
                nextPower = 40;
                nextCooldown = 0.7f;
                break;
            case 7:
                curPower = 40;
                nextPower = 0;
                nextCooldown = 0f;
                break;
        }
    }

    //���������� �߻�
    public override void RightShoot()
    {
        if (animator.GetBool("hit") == false)
            rigidbody2D.velocity = new Vector3(fireballSpeed, 0, 0);
        else if (animator.GetBool("hit") == true)
            rigidbody2D.velocity = Vector3.zero;
    }

    //�������� �߻�
    public override void LeftShoot()
    {
        if (animator.GetBool("hit") == false)
        {
            rigidbody2D.velocity = new Vector3(-fireballSpeed, 0, 0);
            spriteRenderer.flipX = true;
        }
        else if (animator.GetBool("hit") == true)
            rigidbody2D.velocity = Vector3.zero;
    }

    //������Ʈ ȸ��
    public override void OnTargetReached()
    {
        if (this.gameObject.activeSelf)
            Player.Instance.ReturnPool(this);
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
        animator.SetBool("hit", false);
        skillCollider.enabled = true;
        deadTiem = 1.4f;
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

    //���ʹ̶� �ε����� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            animator.SetBool("hit", true);
            Attack();

            if (deadTiem > 0.43f)
                Invoke("OnTargetReached", 0.43f);//����Ʈ �ִϸ��̼� ���� ��ŭ ��ٷȴ� ȸ��
        }
    }

    void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(7, 5), 0);
        skillCollider.enabled = false;
        EnemyBase enemy;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].tag == "Enemy")
            {
                enemy = targets[i].GetComponent<EnemyBase>();
                enemy.TakeDamage(curPower + Player.Instance.playerPower);
            }
        }
    }
}
