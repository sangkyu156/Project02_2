using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyBase, IPoolObject
{
    public string idName;

    void Start()
    {

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
}
