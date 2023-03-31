using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.ParticleSystem;
using System;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI progressValue;
    public TextMeshProUGUI killValue;
    public TextMeshProUGUI goldPaymentValue;
    public TextMeshProUGUI diamondAcquisitionValue;
    public TextMeshProUGUI storCountValue;

    GameObject homeCanvas;
    public GameObject gameoverText;
    public GameObject[] smokeFX;
    bool first = false;

    private void OnEnable()
    {
        first = false;
        SmokeFX_Off();
        Calculate();//보상 지급
        progressValue.text = $"{(BGManager.Instance.countBG * 100) / 30} %";
        killValue.text = $"{GameManager.Instance.killCount}";
        goldPaymentValue.text = $"{GameManager.Instance.paymentGold}";
        diamondAcquisitionValue.text = $"{GameManager.Instance.stageDiamond}";
        storCountValue.text = $"{GameManager.Instance.storCount}";
    }
    void Start()
    {

    }

    void Update()
    {
        if (gameoverText.transform.localPosition.y <= 320 && first == false)
        {
            first = true;
            SmokeFX_On();
        }
    }

    void SmokeFX_Off()
    {
        for (int i = 0; i < smokeFX.Length; i++)
        {
            smokeFX[i].SetActive(false);
        }
    }

    void SmokeFX_On()
    {
        for (int i = 0; i < smokeFX.Length; i++)
        {
            smokeFX[i].SetActive(true);
        }
    }

    public void ExitButton_Home()
    {
        GameManager.Instance.uiSet = false;
        GameManager.Instance.state = GameManager.SceneState.Home;
        Player.Instance.PlayerStop_1();
        GameManager.Instance.TimeScaleSet();
        AchievementManager.Instance.AchievementCheck();
        BGManager.Instance.countBG = 0;
        Time.timeScale = 1;
        SimpleSceneFader.ChangeSceneWithFade("Main");
    }

    public void Calculate()
    {
        int A = 0;
        int B = 0;
        if (((BGManager.Instance.countBG * 100) / 30) < 10)
            A = 0;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 10 && ((BGManager.Instance.countBG * 100) / 30) < 20)
            A = 1;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 20 && ((BGManager.Instance.countBG * 100) / 30) < 30)
            A = 2;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 30 && ((BGManager.Instance.countBG * 100) / 30) < 40)
            A = 3;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 40 && ((BGManager.Instance.countBG * 100) / 30) < 50)
            A = 4;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 50 && ((BGManager.Instance.countBG * 100) / 30) < 60)
            A = 5;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 70 && ((BGManager.Instance.countBG * 100) / 30) < 80)
            A = 6;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 80 && ((BGManager.Instance.countBG * 100) / 30) < 90)
            A = 7;
        else if (((BGManager.Instance.countBG * 100) / 30) >= 90 && ((BGManager.Instance.countBG * 100) / 30) <= 100)
            A = 8;

        if (GameManager.Instance.killCount < 100)
            B = 0;
        else
            B = (int)(GameManager.Instance.killCount * 0.01);

        GameManager.Instance.stageDiamond = A + B;

        GameManager.Instance.mainDiamond += GameManager.Instance.stageDiamond;
    }
}
