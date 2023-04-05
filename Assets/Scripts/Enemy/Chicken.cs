using Redcode.Pools;
using UnityEngine;

public class Chicken : EnemyBase, IPoolObject
{
    public string idName;
    public int maxHealth = 10;
    bool drop = false;

    public GameObject Shadow;

    void Update()
    {
        if (currentHealth <= 0 && drop == false)
        {
            Shadow.SetActive(false);//그림자 x
            collider.enabled = false;//콜라이더 x
            PositionStop();//이동 x
            animator.SetBool("Death", true);//죽는 애니메이션
            deadPostion = transform.position;

            if (drop == false)
            {
                drop = true;
                GameManager.Instance.killCount++;
                for (int i = 0; i < 1; i++)
                {
                    Drop();
                }
            }

            Invoke("OnTargetReached", 0.4f);//0.4초뒤 회수
        }
        else if (drop == false)
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
        maxHealth = 10;
        speed = 3;
        power = 1;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        drop = false;
        Shadow.SetActive(true);
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            GameManager.Instance.ReturnPool(this);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();

        float ranPosX = Random.Range(30f, 50f);
        float ranPosY = Random.Range(0f, -4f);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        SetAbility();
        collider.enabled = true;
        Shadow.SetActive(true);

        float ranPosX = Random.Range(30f, 50f);
        float ranPosY = Random.Range(0f, -4f);
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

    //드랍
    void Drop()
    {
        GameManager.Instance.CoinSpawn();
    }
}
