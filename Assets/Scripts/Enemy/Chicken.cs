using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyBase, IPoolObject
{
    public string idName;
    public int maxHealth = 10;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        TargetConfirm();
    }

    //�ɷ� ����
    void SetAbility()
    {
        hp = 10;
        speed = 3;
        power = 1;
    }

    //������Ʈ ��Ȱ��ȭ
    void OnTargetReached()
    {
        GameManager.Instance.ReturnPool(this);
    }

    //ó������������ ����Ǵ� �޼ҵ�
    public void OnCreatedInPool()
    {
        SetAbility();

        int ranPosX = Random.Range(30, 60);
        float ranPosY = Random.Range(0, -4);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //����Ǽ� �ٽ��ѹ� ����ɶ����� ����Ǵ� �޼ҵ�
    public void OnGettingFromPool()
    {
        SetAbility();

        int ranPosX = Random.Range(30, 60);
        float ranPosY = Random.Range(0, -4);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //������ �޾�����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetMaxHealth(currentHealth);
    }
}
