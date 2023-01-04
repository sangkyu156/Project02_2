using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject store;

    void Start()
    {
        Time.timeScale = 0;

        TextUtil.languageNumber = 2;//언어 설정
        store.SetActive(true);
    }

    void Update()
    {
        
    }

    public void ExitButton()
    {
        Time.timeScale = 1;

        store.SetActive(false);
    }
}
