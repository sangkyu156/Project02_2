using Redcode.Pools;
using UnityEngine;

public class Player : Singleton<Player>
{
    #region 스킬변수
    public int fireBallLevel = 0;
    public float fireBallCooldown = 0;
    #endregion

    public float moveSpeed = 10;
    public int money = 0;
    float dist = 0f;

    public Transform mainCamera;
    public GameObject skillPos;//발사스킬 시작지점
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    PoolManager poolManager; //오브젝트 풀링 매니져
    Animator animator;
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
    }

    void Start()
    {
        money = 999;
        StoreManager.Instance.PrintPlayerMoney();
    }

    void Update()
    {
        dist = Vector2.Distance(mainCamera.position, transform.position);
        if (dist > 23) //왼쪽으로 더이상 못가게 막음
        {
            float posY = transform.position.y;
            transform.position = mainCamera.position + new Vector3(-21f, posY, +10);
        }
    }

    private void FixedUpdate()
    {
        //이동
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");

        rigidbody2D.velocity = vector2.normalized * moveSpeed;

        #region 방향전환
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
        //InvokeRepeating("Spawn", 0, fireBallCooldown);
        InvokeRepeating("Spawn", 0, 0.05f);
    }

    //오브젝트풀링 생성
    void Spawn()
    {
        FireBall_Skill fireBall_Skill = poolManager.GetFromPool<FireBall_Skill>();
    }

    //오브젝트 회수
    public void ReturnPool(FireBall_Skill clone)
    {
        poolManager.TakeToPool<FireBall_Skill>(clone.idName, clone);
    }


}
