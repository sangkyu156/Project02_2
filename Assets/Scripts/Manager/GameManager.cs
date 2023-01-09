using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject store;
    PoolManager poolManager; //오브젝트 풀링 매니져

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    void Start()
    {
        Time.timeScale = 0;

        TextUtil.languageNumber = 2;//언어 설정
        store.SetActive(true);

        Create_01();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TextUtil.languageNumber = 2; //미국어
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TextUtil.languageNumber = 1; //한국어
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

    //오브젝트풀링 생성
    void Spawn()
    {
        Chicken chicken = poolManager.GetFromPool<Chicken>();
    }

    //오브젝트 회수
    public void ReturnPool(Chicken clone)
    {
        poolManager.TakeToPool<Chicken>(clone.idName, clone);
    }
}
