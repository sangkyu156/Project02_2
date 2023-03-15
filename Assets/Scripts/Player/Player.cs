using Redcode.Pools;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class Player : MonoBehaviour
{
    #region ��ų����
    public int fireBallLevel = 0;
    public float fireBallCooldown = 0;
    public int tornadoLevel = 0;
    public float tornadoCooldown = 0;
    public int blackholeLevel = 0;
    public float blackholeCooldown = 0;
    public int sawBladeLevel = 0;
    public int sparkLevel = 0;
    public float sparkCooldown = 0;
    public GameObject[] sawBlade;
    #endregion

    public int playerPower = 0;
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 10;
    public int money = 0;
    float dist = 0f;

    public PlayerHealthBar healthBar;
    public GameObject mainCamera;
    public Transform textPostion;
    public GameObject skillPos;//�߻罺ų ��������
    public GameObject effect_Heal;
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


    private static Player instance = null;

    private void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        poolManager = GetComponent<PoolManager>();
        collider = GetComponent<BoxCollider2D>();
    }

    public static Player Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.state != GameManager.SceneState.Stage)
            return;
        try
        {
            dist = Vector2.Distance(mainCamera.transform.position, transform.position);
        }
        catch (System.Exception e)
        {
            mainCamera = GameObject.Find("Main Camera");
        }

        if (dist > 19) //�������� ���̻� ������ ����
        {
            float posY = transform.position.y;
            transform.position = mainCamera.transform.position + new Vector3(-16.2f, posY, +10);//GuideTextCanvas

            //���̵�Text ���
            GameObject guideText = Instantiate(Resources.Load<GameObject>($"GuideTextCanvas")) as GameObject;
            guideText.transform.SetParent(textPostion, false);
        }

        //���׷� ���� y���� �������� �Ѿ���� �ٽ� �ʱ�ȭ
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 1;
            GameManager.Instance.PlayerDeath();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.state != GameManager.SceneState.Stage)
            return;

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

    //��� �߰�
    public void SawBladeAdd()
    {
        switch (sawBladeLevel)
        {
            case 1:
                sawBlade[0].SetActive(true); break;
            case 2:
                sawBlade[1].SetActive(true); break;
            case 3:
                sawBlade[2].SetActive(true); break;
            case 4:
                sawBlade[3].SetActive(true); break;
            case 5:
                sawBlade[4].SetActive(true); break;
            case 6:
                sawBlade[5].SetActive(true); break;
            case 7:
                sawBlade[6].SetActive(true);
                sawBlade[7].SetActive(true); break;
        }
    }

    //����ũ �߻� ����
    public void SparkAction()
    {
        CancelInvoke("Spawn4");
        InvokeRepeating("Spawn4", 0, sparkCooldown);
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
    void Spawn4()
    {
        Spark_Skill spark_Skill = poolManager.GetFromPool<Spark_Skill>();
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
    public void ReturnPool(Spark_Skill clone)
    {
        poolManager.TakeToPool<Spark_Skill>(clone.idName, clone);
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

    //���� �Ծ�����
    public void GetHP_Potion(ItemManager.Potion potion_)
    {
        effect_Heal.SetActive(true);

        switch (potion_)
        {
            case ItemManager.Potion.HP_Potion:
                currentHealth += (3 + StateManager.Instance.state_PotionRecover);
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //�� ���
                GameObject healUI = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI.GetComponentInChildren<HealText>().heal = $"+{(3 + StateManager.Instance.state_PotionRecover)}";
                healUI.transform.SetParent(textPostion, false);
                break;
            case ItemManager.Potion.HP_Potion2:
                currentHealth += (5 + StateManager.Instance.state_PotionRecover);
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //�� ���
                GameObject healUI2 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI2.GetComponentInChildren<HealText>().heal = $"+{(5 + StateManager.Instance.state_PotionRecover)}";
                healUI2.transform.SetParent(textPostion, false);
                break;
            case ItemManager.Potion.HP_Potion3:
                currentHealth += (7 + StateManager.Instance.state_PotionRecover);
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //�� ���
                GameObject healUI3 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI3.GetComponentInChildren<HealText>().heal = $"+{(7 + StateManager.Instance.state_PotionRecover)}";
                healUI3.transform.SetParent(textPostion, false);
                break;
        }

        healthBar.SetHealth(currentHealth);

        Invoke("HealOff", 0.7f);
    }

    void HealOff()
    {
        effect_Heal.SetActive(false);
    }

    //�÷��̾� ����
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.state == SceneState.Stage)
        {
            mainCamera = GameObject.Find("Main Camera");
            this.transform.position = new Vector3(-16, -7, 0);

            playerPower = StateManager.Instance.state_Power + 0;
            maxHealth = StateManager.Instance.state_Health + 10;
            //StateManager.Instance.state_StartGold = 400;
            StateManager.Instance.state_StartGold = 40000;
            money = StateManager.Instance.state_StartGold;
            GameManager.Instance.PrintPlayerMoney();

            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            OnDamage();

            SkillReset();
            PlayerStop();
        }
    }

    void SkillReset()
    {
        CancelInvoke();
        for (int i = 0; i < sawBlade.Length; i++)
        {
            sawBlade[i].SetActive(false);
        }
        fireBallLevel = 0;
        tornadoLevel = 0;
        blackholeLevel = 0;
        sawBladeLevel = 0;
    }

    //Ȩȭ������ ������ �÷��̾� ������Ű��
    public void PlayerStop()
    {
        this.transform.position = new Vector3(-16, -7, 0);
        rigidbody2D.velocity = new Vector2(0, 0);
        this.rigidbody2D.AddForce(new Vector2(0, 0));
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}