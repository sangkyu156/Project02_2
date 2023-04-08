using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_Legend : MonoBehaviour
{
    public GameObject button1;//완료하지 못함
    public GameObject button2;//완료 해서 보상을 받을 준비됨
    public GameObject button3;//이미 보상을 완료함
    public Slider slider;

    private void OnEnable()
    {
        SliderSet();
    }

    void Start()
    {
        slider.maxValue = 10;
        slider.value = AchievementManager.Instance.legendSkillCount;

        ButtonSet();
    }

    public void ButtonSet()
    {
        switch (AchievementManager.Instance.achievement08)
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
        //if (AchievementManager.Instance.reward08 == false)
        //{
        //    switch (AchievementManager.Instance.achievement08)
        //    {
        //        case 0:
        //            button1.SetActive(true);
        //            button2.SetActive(false);
        //            button3.SetActive(false);
        //            break;
        //        case 1:
        //            button1.SetActive(false);
        //            button2.SetActive(true);
        //            button3.SetActive(false);
        //            break;
        //        case 2:
        //            button1.SetActive(false);
        //            button2.SetActive(false);
        //            button3.SetActive(true);
        //            break;
        //    }
        //}
    }

    public void Reward()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.DiamondReward);

        AchievementManager.Instance.achievement08 = 2;
        GameManager.Instance.mainDiamond += 3;

        ButtonSet();
        HomeManager.Instance.PrintDiamond();

        //AchievementManager.Instance.reward08 = true;
    }

    public void SliderSet()
    {
        slider.value = AchievementManager.Instance.legendSkillCount;

        ButtonSet();
    }
}
