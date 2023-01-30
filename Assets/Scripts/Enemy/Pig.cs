using Redcode.Pools;
using UnityEngine;

public class Pig : EnemyBase, IPoolObject
{
    public string idName;
    public int maxHealth = 9;
    bool drop = false;

    public GameObject Shadow;

    void Update()
    {
        if (currentHealth <= 0)
        {
            Shadow.SetActive(false);//�׸��� x
            collider.enabled = false;//�ݶ��̴� x
            PositionStop();//�̵� x
            animator.SetBool("Death", true);//�״� �ִϸ��̼�
            deadPostion = transform.position;

            if (drop == false)
            {
                drop = true;
                for (int i = 0; i < 1; i++)
                {
                    Drop();
                }

                if(Random.Range(0,20) == 7)
                {
                    PotionDrop();
                }
            }

            Invoke("OnTargetReached", 0.6f);//0.7�ʵ� ȸ��
        }
        else
        {
            TargetConfirm();
        }

        //�÷��̾� ��ġ�� ���� ȸ��(�ִϸ��̼��� �ٲٴ� ���)
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

        //���׷� ���� y���� �������� �Ѿ���� �ٽ� �ʱ�ȭ
        if (transform.position.y > -3.5f || transform.position.y < -11)
        {
            transform.position = new Vector3(transform.position.x, -7);
        }

        //�ѹ��� �и��� ���ؼ� ���
        if (knockback == true)
        {
            knockbackTime -= Time.deltaTime;
            if (knockbackTime <= 0)
            {
                knockbackTime = 1;
                knockback = false;
            }
        }
    }

    //�ɷ� ����
    void SetAbility()
    {
        maxHealth = 9;
        speed = 3f;
        power = 1;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        drop = false;
        Shadow.SetActive(true);
    }

    //������Ʈ ��Ȱ��ȭ
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            GameManager.Instance.ReturnPool(this);
    }

    //ó������������ ����Ǵ� �޼ҵ�
    public void OnCreatedInPool()
    {
        SetAbility();

        float ranPosX = Random.Range(30f, 60f);
        float ranPosY = Random.Range(0f, -4f);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //����Ǽ� �ٽ��ѹ� ����ɶ����� ����Ǵ� �޼ҵ�
    public void OnGettingFromPool()
    {
        SetAbility();
        collider.enabled = true;
        Shadow.SetActive(true);
        knockback = false;

        float ranPosX = Random.Range(30f, 60f);
        float ranPosY = Random.Range(0f, -4f);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //�÷��̾� ����
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

    //���
    void Drop()
    {
        GameManager.Instance.CoinSpawn();
    }

    //���ǵ��
    void PotionDrop()
    {
        GameObject potion = Instantiate(Resources.Load<GameObject>("Field/HP_Potion")) as GameObject;
        float posX = Random.Range(-0.5f, 0.3f);
        float posY = Random.Range(-0.5f, 0.3f);
        potion.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
    }
}
