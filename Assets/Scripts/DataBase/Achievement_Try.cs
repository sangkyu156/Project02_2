using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_Try : MonoBehaviour
{
    public GameObject button1;//완료하지 못함
    public GameObject button2;//완료 해서 보상을 받을 준비됨
    public GameObject button3;//이미 보상을 완료함
    public Slider slider;

    void Start()
    {
        slider.maxValue = 3;
        slider.value = AchievementManager.Instance.tryCount;

        ButtonSet();
    }

    public void ButtonSet()
    {
        if (AchievementManager.Instance.reward02 == false)
        {
            switch (AchievementManager.Instance.achievement02)
            {
                case 0:
                    button1.SetActive(true);
                    button2.SetActive(false);
                    button3.SetActive(false);
                    break;
                case 1:
                    button1.SetActive(false);
                    button2.SetActive(true);
                    button3.SetActive(false);
                    break;
                case 2:
                    button1.SetActive(false);
                    button2.SetActive(false);
                    button3.SetActive(true);
                    break;
            }
        }
    }

    public void Reward()
    {
        AchievementManager.Instance.achievement02 = 2;
        GameManager.Instance.mainDiamond += 1;

        ButtonSet();
        HomeManager.Instance.PrintDiamond();

        AchievementManager.Instance.reward02 = true;
    }

    //임시 테스트 함수
    public void asdqwe()
    {
        slider.value = AchievementManager.Instance.tryCount;

        ButtonSet();
    }
}
