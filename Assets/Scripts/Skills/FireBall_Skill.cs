using Redcode.Pools;
using UnityEngine;

public class FireBall_Skill : MonoBehaviour, IPoolObject
{
    protected int curPower = 0;
    protected int nextPower = 0;
    protected float curCooldown = 0;
    protected float nextCooldown = 0;
    float fireballSpeed = 0;
    float deadTiem = 0;
    bool first = false;
    bool right = false;
    bool left = false;

    public string idName;

    SpriteRenderer playerFlip;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        SetAbility();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerFlip = Player.Instance.GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        fireballSpeed = Player.Instance.moveSpeed + 2;
        deadTiem = 5f;
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
            deadTiem = 5f;
            OnTargetReached();
        }
    }

    //레벨에 따라서 능력치 세팅
    protected void SetAbility()
    {
        switch (Player.Instance.fireBallLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                curCooldown = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 3;
                nextPower = 4;
                curCooldown = 1;
                nextCooldown = 1;
                break;
            case 2:
                curPower = 4;
                nextPower = 5;
                curCooldown = 1;
                nextCooldown = 0.9f;
                break;
            case 3:
                curPower = 5;
                nextPower = 6;
                curCooldown = 0.9f;
                nextCooldown = 0.9f;
                break;
            case 4:
                curPower = 6;
                nextPower = 7;
                curCooldown = 0.9f;
                nextCooldown = 0.8f;
                break;
            case 5:
                curPower = 7;
                nextPower = 8;
                curCooldown = 0.8f;
                nextCooldown = 0.8f;
                break;
            case 6:
                curPower = 8;
                nextPower = 9;
                curCooldown = 0.8f;
                nextCooldown = 0.7f;
                break;
            case 7:
                curPower = 10;
                nextPower = 10;
                curCooldown = 0.7f;
                nextCooldown = 0.7f;
                break;
        }
    }

    //오른쪽으로 발사
    void RightShoot()
    {
        rigidbody2D.velocity = new Vector3(fireballSpeed, 0, 0);
    }

    //왼쪽으로 발사
    void LeftShoot()
    {
        rigidbody2D.velocity = new Vector3(-fireballSpeed, 0, 0);
        spriteRenderer.flipX = true;
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        Debug.Log("들어옴");
        Player.Instance.ReturnPool(this);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        first = false;
        right = false;
        left = false;
        spriteRenderer.flipX = false;
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        first = false;
        right = false;
        left = false;
        spriteRenderer.flipX = false;
        if (rigidbody2D != null)
        {
            //생성위치 지정
            if (playerFlip.flipX == true)
                transform.position = Player.Instance.skillPos.transform.position;
            else
                transform.position = Player.Instance.skillPos.transform.position - new Vector3(1.57f, 0, 0);
        }
    }
}
