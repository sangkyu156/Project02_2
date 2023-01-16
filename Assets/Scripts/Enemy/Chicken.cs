using Redcode.Pools;
using UnityEngine;

public class Chicken : EnemyBase, IPoolObject
{
    public string idName;
    public int maxHealth = 10;
    bool drop = false;

    CapsuleCollider2D collider;
    public GameObject Shadow;

    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Shadow.SetActive(false);//그림자 x
            collider.enabled = false;//콜라이더 x
            PositionStop();//이동 x
            animator.SetBool("Death", true);//죽는 애니메이션
            Invoke("OnTargetReached", 0.4f);//0.7초뒤 회수
        }
        else
        {
            TargetConfirm();
        }

        //플레이어 위치에 따라 회전(애니메이션을 바꾸는 방법)
        if (Player.Instance.transform.position.x < transform.position.x)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }

        //버그로 인해 y값이 일정범위 넘어갔을때 다시 초기화
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }
    }

    //능력 설정
    void SetAbility()
    {
        hp = 10;
        speed = 3;
        power = 1;
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        if (drop == false)
        {
            drop = true;
            GameObject coin = Instantiate(Resources.Load<GameObject>("Field/Coin_1")) as GameObject;
            float posX = Random.Range(-0.5f, 0.3f);
            float posY = Random.Range(-0.5f, 0.3f);
            coin.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
        }

        if (gameObject.activeSelf)
            GameManager.Instance.ReturnPool(this);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();

        drop = false;
        int ranPosX = Random.Range(30, 60);
        float ranPosY = Random.Range(0, -4);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        SetAbility();
        Shadow.SetActive(true);

        drop = false;
        int ranPosX = Random.Range(30, 60);
        float ranPosY = Random.Range(0, -4);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //플레이어 공격
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            bool Right = true;
            if (collision.transform.position.x < this.transform.position.x)
                Right = true;
            else
                Right = false;

            Player.Instance.TakeDamage(power, Right);
        }
    }
}
