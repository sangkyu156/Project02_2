using Redcode.Pools;
using UnityEngine;

public class Player : Singleton<Player>
{
    #region ��ų����
    public int fireBallLevel = 0;
    public float fireBallCooldown = 0;
    public int tornadoLevel = 0;
    public float tornadoCooldown = 0;
    public int blackholeLevel = 0;
    public float blackholeCooldown = 0;
    #endregion

    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 10;
    public int money = 0;
    float dist = 0f;

    public PlayerHealthBar healthBar;
    public Transform mainCamera;
    public Transform textPostion;
    public GameObject skillPos;//�߻罺ų ��������
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    PoolManager poolManager; //������Ʈ Ǯ�� �Ŵ���
    Animator animator;
    BoxCollider2D collider;
    Vector2 vector2;

    public enum Eirection
    {
        Up, Down, Left, Right
    }
    Eirection eirection = Eirection.Down;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        money = 3999;
        StoreManager.Instance.PrintPlayerMoney();
    }

    void Update()
    {
        dist = Vector2.Distance(mainCamera.position, transform.position);
        if (dist > 19) //�������� ���̻� ������ ����
        {
            float posY = transform.position.y;
            transform.position = mainCamera.position + new Vector3(-16.2f, posY, +10);//GuideTextCanvas

            //���̵�Text ���
            GameObject guideText = Instantiate(Resources.Load<GameObject>($"GuideTextCanvas")) as GameObject;
            guideText.transform.SetParent(textPostion, false);
        }

        //���׷� ���� y���� �������� �Ѿ���� �ٽ� �ʱ�ȭ
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }
    }

    private void FixedUpdate()
    {
        //�̵�
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");

        rigidbody2D.velocity = vector2.normalized * moveSpeed;

        #region ������ȯ
        if (Input.GetKey(KeyCode.UpArrow))
        {
            eirection = Eirection.Up;
            animator.SetBool("U_Walk", true);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            eirection = Eirection.Down;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", true);
            animator.SetBool("L_Walk", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            eirection = Eirection.Right;
            spriteRenderer.flipX = true;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            eirection = Eirection.Left;
            spriteRenderer.flipX = false;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", true);
        }
        #endregion

        // ����������
        if (rigidbody2D.velocity.x == 0 && rigidbody2D.velocity.y == 0)
        {
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", false);
            switch (eirection)
            {
                case Eirection.Up:
                    animator.SetBool("U_Idle", true);
                    animator.SetBool("D_Idle", false);
                    animator.SetBool("L_Idle", false);
                    break;
                case Eirection.Down:
                    animator.SetBool("U_Idle", false);
                    animator.SetBool("D_Idle", true);
                    animator.SetBool("L_Idle", false);
                    break;
                case Eirection.Left:
                case Eirection.Right:
                    animator.SetBool("U_Idle", false);
                    animator.SetBool("D_Idle", false);
                    animator.SetBool("L_Idle", true);
                    break;
            }
        }
        else
        {
            animator.SetBool("U_Idle", false);
            animator.SetBool("D_Idle", false);
            animator.SetBool("L_Idle", false);
        }
    }

    //���̾ �߻� ����
    public void FireBallAction()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0, fireBallCooldown);
    }

    //����̵� �߻� ����
    public void TornadoAction()
    {
        CancelInvoke("Spawn2");
        InvokeRepeating("Spawn2", 0, tornadoCooldown);
    }

    //��Ȧ �߻� ����
    public void BlackHoleAction()
    {
        CancelInvoke("Spawn3");
        InvokeRepeating("Spawn3", 0, blackholeCooldown);
    }

    //������ƮǮ�� ����
    void Spawn()
    {
        FireBall_Skill fireBall_Skill = poolManager.GetFromPool<FireBall_Skill>();
    }
    void Spawn2()
    {
        Tornado_Skill tornado_Skill = poolManager.GetFromPool<Tornado_Skill>();
    }
    void Spawn3()
    {
        BlackHole_Skill blackHole_Skill = poolManager.GetFromPool<BlackHole_Skill>();
    }

    //������Ʈ ȸ��
    public void ReturnPool(FireBall_Skill clone)
    {
        poolManager.TakeToPool<FireBall_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Tornado_Skill clone)
    {
        poolManager.TakeToPool<Tornado_Skill>(clone.idName, clone);
    }
    public void ReturnPool(BlackHole_Skill clone)
    {
        poolManager.TakeToPool<BlackHole_Skill>(clone.idName, clone);
    }

    //������ �޾�����
    public void TakeDamage(int damage_, bool direction)//�Ű����� bool�� ���� ���������� �з����� �������� �з����� ���ؾ���
    {
        currentHealth -= damage_;
        healthBar.SetHealth(currentHealth);

        //������ ���
        GameObject damageUI = Instantiate(Resources.Load<GameObject>($"DamageTextCanvas")) as GameObject;
        damageUI.GetComponentInChildren<DamageText>().damage = damage_;
        damageUI.transform.SetParent(textPostion, false);

        gameObject.tag = "NoDamage";
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("effect1", 0.2f);
        Invoke("effect2", 0.3f); 
        Invoke("effect1", 0.4f);
        Invoke("effect2", 0.5f); 
        Invoke("effect1", 0.6f);
        Invoke("effect2", 0.7f);
        Invoke("effect1", 0.8f);
        Invoke("effect2", 0.9f);


        if (direction)//�����ʿ��� �������� ����
            this.rigidbody2D.AddForce(new Vector2(-1, 0) * 5000);
        else
            this.rigidbody2D.AddForce(new Vector2(1, 0) * 5000);

        Invoke("OnDamage", 1f);
    }

    //���� Ǯ��
    public void OnDamage()
    {
        gameObject.tag = "Player";
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void effect1()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.6f);
    }

    void effect2()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
    }
}
