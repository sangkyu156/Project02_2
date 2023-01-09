using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject store;
    PoolManager poolManager; //������Ʈ Ǯ�� �Ŵ���

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    void Start()
    {
        Time.timeScale = 0;

        TextUtil.languageNumber = 2;//��� ����
        store.SetActive(true);

        Create_01();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TextUtil.languageNumber = 2; //�̱���
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TextUtil.languageNumber = 1; //�ѱ���
        }
    }

    public void ExitButton()
    {
        Time.timeScale = 1;

        store.SetActive(false);
    }

    void Create_01()
    {
        for (int i = 0; i < 10; i++) 
        {
            Spawn();
        }
    }

    //������ƮǮ�� ����
    void Spawn()
    {
        Chicken chicken = poolManager.GetFromPool<Chicken>();
    }

    //������Ʈ ȸ��
    public void ReturnPool(Chicken clone)
    {
        poolManager.TakeToPool<Chicken>(clone.idName, clone);
    }
}
