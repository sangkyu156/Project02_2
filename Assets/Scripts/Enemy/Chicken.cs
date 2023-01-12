using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyBase, IPoolObject
{
    public string idName;
    public int maxHealth = 10;

    CapsuleCollider2D collider;
    //public GameObject body;

    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            collider.enabled = false;
            PositionStop();
            animator.SetBool("Death", true);
            Invoke("OnTargetReached", 0.7f);
        }
        else
        {

            TargetConfirm();
        }

        //if (Player.Instance.transform.position.x < transform.position.x)
        //{
        //    Debug.Log("플레이어가 나보다 왼쪽에있음");
        //    body.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //}
        //else
        //{
        //    Debug.Log("플레이어가 나보다 오른쪽에있음");
        //    body.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        //}
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
        if (gameObject.activeSelf)
            GameManager.Instance.ReturnPool(this);
    }

    //처음생성됬을때 실행되는 메소드
    public void OnCreatedInPool()
    {
        SetAbility();

        int ranPosX = Random.Range(30, 60);
        float ranPosY = Random.Range(0, -4);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }

    //재사용되서 다시한번 실행될때마다 실행되는 메소드
    public void OnGettingFromPool()
    {
        SetAbility();

        int ranPosX = Random.Range(30, 60);
        float ranPosY = Random.Range(0, -4);
        transform.position = Player.Instance.skillPos.transform.position + new Vector3(ranPosX, ranPosY, 0);
    }
}
