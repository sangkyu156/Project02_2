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
}
