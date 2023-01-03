using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 0;

        TextUtil.languageNumber = 2;
    }

    void Update()
    {
        
    }

    public void Exit()
    {
        Time.timeScale = 1;


    }
}
