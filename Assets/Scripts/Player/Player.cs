using Redcode.Pools;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class Player : MonoBehaviour
{
    #region 스킬변수
    public int fireBallLevel = 0;           //파이어볼
    public float fireBallCooldown = 0;
    public int tornadoLevel = 0;            //토네이도
    public float tornadoCooldown = 0;
    public int blackholeLevel = 0;          //블랙홀
    public float blackholeCooldown = 0;
    public int sawBladeLevel = 0;           //톱날
    public int sparkLevel = 0;              //스파크(연사)
    public float sparkCooldown = 0;
    public int waveEnergyLevel = 0;         //에너지볼(관통)
    public float waveEnergyCooldown = 0;
    public int volcanoLevel = 0;            //볼케이노
    public float volcanoCooldown = 0;
    public bool volcano = false;
    public int tridentLevel = 0;            //삼지창
    public float tridentCooldown = 0;
    public bool trident = false;
    public int quicknessLevel = 0;          //신속
    public int slowdownLevel = 0;           //감속
    public int regenerateLevel = 0;         //체력회복
    public bool regenerate = false;
    public float regenerateCooldown = 0;
    public GameObject rageExplosionSkill;   //분노폭발
    public int rageExplosionLevel = 0;
    public float rageExplosionTime = 0;
    public bool rageExplosion = false;
    public GameObject[] sawBlade;
    #endregion

    public int playerPower = 0;
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 7;
    public int money = 0;
    float dist = 0f;

    public PlayerHealthBar healthBar;
    public GameObject mainCamera;
    public Transform textPostion;
    public Transform volcanoPos;
    public Transform tridentPos;
    public GameObject skillPos;//발사스킬 시작지점
    public GameObject effect_Heal;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    PoolManager poolManager; //오브젝트 풀링 매니져
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
        // 델리게이트 체인 추가
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

        if (dist > 19) //왼쪽으로 더이상 못가게 막음
        {
            float posY = transform.position.y;
            transform.position = mainCamera.transform.position + new Vector3(-16.2f, posY, +10);//GuideTextCanvas

            //가이드Text 출력
            GameObject guideText = Instantiate(Resources.Load<GameObject>($"GuideTextCanvas")) as GameObject;
            guideText.transform.SetParent(textPostion, false);
        }

        //버그로 인해 y값이 일정범위 넘어갔을때 다시 초기화
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 1;
            GameManager.Instance.PlayerDeath();
        }

        //볼케이노 스킬 획득시
        if(volcano)
        {
            volcanoCooldown -= Time.deltaTime;
            if(volcanoCooldown < 0)
            {
                Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(10, 5), 0);

                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].tag == "Enemy" && targets[i].GetComponent<EnemyBase>().currentHealth > 0)
                    {
                        if (Mathf.Abs(transform.position.x - targets[i].transform.position.x) < 13)
                        {
                            volcanoPos = targets[i].transform;
                            Spawn6();
                        }

                        switch (volcanoLevel)
                        {
                            case 1:
                                volcanoCooldown = 2f; break;
                            case 2:
                                volcanoCooldown = 2f; break;
                            case 3:
                                volcanoCooldown = 1.9f; break;
                            case 4:
                                volcanoCooldown = 1.9f; break;
                            case 5:
                                volcanoCooldown = 1.8f; break;
                            case 6:
                                volcanoCooldown = 1.7f; break;
                            case 7:
                                volcanoCooldown = 1.6f; break;
                        }

                        return;
                    }
                }
            }
        }

        //삼지창 스킬 획득시
        if(trident)
        {
            tridentCooldown -= Time.deltaTime;
            if (tridentCooldown < 0)
            {
                Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(12, 5), 0);

                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].tag == "Enemy" && targets[i].GetComponent<EnemyBase>().currentHealth > 0)
                    {
                        if (Mathf.Abs(transform.position.x - targets[i].transform.position.x) < 13)
                        {
                            tridentPos = targets[i].transform;
                            Spawn7();
                        }

                        switch (tridentLevel)
                        {
                            case 1:
                                tridentCooldown = 0.9f; break;
                            case 2:
                                tridentCooldown = 0.9f; break;
                            case 3:
                                tridentCooldown = 0.8f; break;
                            case 4:
                                tridentCooldown = 0.8f; break;
                            case 5:
                                tridentCooldown = 0.7f; break;
                            case 6:
                                tridentCooldown = 0.7f; break;
                            case 7:
                                tridentCooldown = 0.5f; break;
                        }

                        return;
                    }
                }
            }
        }

        //체력회복 스킬 획득시
        if(regenerate)
        {
            regenerateCooldown -= Time.deltaTime;
            if(regenerateCooldown < 0)
                Heal();
        }

        //분노폭발 스킬 획득시
        if(rageExplosion)
        {
            rageExplosionTime -= Time.deltaTime;
            if(rageExplosionTime < 0)
            {
                rageExplosionSkill.SetActive(true);
                Invoke("RageExplosionOff", 0.7f);
                rageExplosionTime = rageExplosionSkill.GetComponent<RageExplosion_Skill>().rageExplosionCooldown;
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.state != GameManager.SceneState.Stage)
            return;

        //이동
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");

        rigidbody2D.velocity = vector2.normalized * moveSpeed;

        #region 방향전환
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            eirection = Eirection.Up;
            animator.SetBool("U_Walk", true);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            eirection = Eirection.Down;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", true);
            animator.SetBool("L_Walk", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            eirection = Eirection.Right;
            spriteRenderer.flipX = true;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            eirection = Eirection.Left;
            spriteRenderer.flipX = false;
            animator.SetBool("U_Walk", false);
            animator.SetBool("D_Walk", false);
            animator.SetBool("L_Walk", true);
        }
        #endregion

        // 멈춰있을때
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

    //파이어볼 발사 시작
    public void FireBallAction()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0, fireBallCooldown);
    }

    //토네이도 발사 시작
    public void TornadoAction()
    {
        CancelInvoke("Spawn2");
        InvokeRepeating("Spawn2", 0, tornadoCooldown);
    }

    //블랙홀 발사 시작
    public void BlackHoleAction()
    {
        CancelInvoke("Spawn3");
        InvokeRepeating("Spawn3", 0, blackholeCooldown);
    }

    //볼케이노 발사 시작
    public void VolcanoAction()
    {
        volcano = true;
    }

    //삼지창 발사 시작
    public void TridentAction()
    {
        trident = true;
    }

    //톱니 추가
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

    //스파크 발사 시작
    public void SparkAction()
    {
        CancelInvoke("Spawn4");
        InvokeRepeating("Spawn4", 0, sparkCooldown);
    }

    //에너지볼 발사 시작
    public void WaveEnergyAction()
    {
        CancelInvoke("Spawn5");
        InvokeRepeating("Spawn5", 0, waveEnergyCooldown);
    }

    //분노 폭발 시작
    public void RageExplosionAction()
    {
        rageExplosion = true;
    }

    //오브젝트풀링 생성
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
    void Spawn5()
    {
        WaveEnergy_Skill waveEnergy_Skill = poolManager.GetFromPool<WaveEnergy_Skill>();
    }
    void Spawn6()
    {
        Volcano_Skill volcano_Skill = poolManager.GetFromPool<Volcano_Skill>();
    }
    void Spawn7()
    {
        Trident_Skill volcano_Skill = poolManager.GetFromPool<Trident_Skill>();
    }

    //오브젝트 회수
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
    public void ReturnPool(WaveEnergy_Skill clone)
    {
        poolManager.TakeToPool<WaveEnergy_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Volcano_Skill clone)
    {
        poolManager.TakeToPool<Volcano_Skill>(clone.idName, clone);
    }
    public void ReturnPool(Trident_Skill clone)
    {
        poolManager.TakeToPool<Trident_Skill>(clone.idName, clone);
    }

    //데미지 받았을때
    public void TakeDamage(int damage_, bool direction)//매개변수 bool값 으로 오른쪽으로 밀려날지 왼쪽으로 밀려날지 정해야함
    {
        currentHealth -= damage_;
        healthBar.SetHealth(currentHealth);

        //데미지 출력
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


        if (direction)//오른쪽에서 데미지를 받음
            this.rigidbody2D.AddForce(new Vector2(-1, 0) * 5000);
        else
            this.rigidbody2D.AddForce(new Vector2(1, 0) * 5000);

        Invoke("OnDamage", 1f);
    }

    //무적 풀림
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

    //포션 먹었을때
    public void GetHP_Potion(ItemManager.Potion potion_)
    {
        effect_Heal.SetActive(true);

        switch (potion_)
        {
            case ItemManager.Potion.HP_Potion:
                currentHealth += (3 + StateManager.Instance.state_PotionRecover);
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI.GetComponentInChildren<HealText>().heal = $"+{(3 + StateManager.Instance.state_PotionRecover)}";
                healUI.transform.SetParent(textPostion, false);
                break;
            case ItemManager.Potion.HP_Potion2:
                currentHealth += (5 + StateManager.Instance.state_PotionRecover);
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI2 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI2.GetComponentInChildren<HealText>().heal = $"+{(5 + StateManager.Instance.state_PotionRecover)}";
                healUI2.transform.SetParent(textPostion, false);
                break;
            case ItemManager.Potion.HP_Potion3:
                currentHealth += (7 + StateManager.Instance.state_PotionRecover);
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI3 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI3.GetComponentInChildren<HealText>().heal = $"+{(7 + StateManager.Instance.state_PotionRecover)}";
                healUI3.transform.SetParent(textPostion, false);
                break;
        }

        healthBar.SetHealth(currentHealth);

        Invoke("HealOff", 0.7f);
    }

    //체력회복 스킬
    void Heal()
    {
        regenerateCooldown = 10;
        effect_Heal.SetActive(true);

        switch (Player.Instance.regenerateLevel)
        {
            case 0:
                currentHealth += 0;
                break;
            case 1:
                currentHealth += 1;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI.GetComponentInChildren<HealText>().heal = $"+1";
                healUI.transform.SetParent(textPostion, false);
                break;
            case 2:
                currentHealth += 3;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI2 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI2.GetComponentInChildren<HealText>().heal = $"+3";
                healUI2.transform.SetParent(textPostion, false);
                break;
            case 3:
                currentHealth += 5;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI3 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI3.GetComponentInChildren<HealText>().heal = $"+5";
                healUI3.transform.SetParent(textPostion, false);
                break;
            case 4:
                currentHealth += 7;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI4 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI4.GetComponentInChildren<HealText>().heal = $"+7";
                healUI4.transform.SetParent(textPostion, false);
                break;
            case 5:
                currentHealth += 9;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI5 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI5.GetComponentInChildren<HealText>().heal = $"+9";
                healUI5.transform.SetParent(textPostion, false);
                break;
            case 6:
                currentHealth += 11;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI6 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI6.GetComponentInChildren<HealText>().heal = $"+11";
                healUI6.transform.SetParent(textPostion, false);
                break;
            case 7:
                currentHealth += 15;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                //힐 출력
                GameObject healUI7 = Instantiate(Resources.Load<GameObject>($"HealTextCanvas")) as GameObject;
                healUI7.GetComponentInChildren<HealText>().heal = $"+15";
                healUI7.transform.SetParent(textPostion, false);
                break;
        }

        healthBar.SetHealth(currentHealth);

        Invoke("HealOff", 0.7f);
    }

    //분노폭발 리셋
    void RageExplosionReset()
    {
        rageExplosionLevel = 0;
        rageExplosionTime = 1.3f;
        rageExplosionSkill.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        rageExplosionSkill.transform.localScale = new Vector3(1, 1, 1);
        rageExplosionSkill.SetActive(false);
        rageExplosion = false;
    }

    void RageExplosionOff()
    {
        rageExplosionSkill.SetActive(false);
    }

    void HealOff()
    {
        effect_Heal.SetActive(false);
    }

    //플레이어 세팅
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.state == SceneState.Stage)
        {
            mainCamera = GameObject.Find("Main Camera");
            this.transform.position = new Vector3(-7.7f, -7, 0);

            playerPower = StateManager.Instance.state_Power + 0;
            maxHealth = StateManager.Instance.state_Health + 10;
            moveSpeed = 7;
            Boss.Instance.bossSpeed = 7;
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
        sparkLevel = 0;
        waveEnergyLevel = 0;
        volcanoLevel = 0;
        tridentLevel = 0;
        quicknessLevel = 0;
        slowdownLevel = 0;
        regenerateLevel = 0;
        RageExplosionReset();
        volcano = false;
        trident = false;
        regenerate = false;
    }

    //홈화면으로 왔을때 플레이어 정지시키기
    public void PlayerStop()
    {
        this.transform.position = new Vector3(-7.7f, -7, 0);
        rigidbody2D.velocity = new Vector2(0, 0);
        this.rigidbody2D.AddForce(new Vector2(0, 0));
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}