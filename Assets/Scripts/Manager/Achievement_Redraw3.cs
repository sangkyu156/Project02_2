using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_Redraw3 : MonoBehaviour
{
    public GameObject button1;//�Ϸ����� ����
    public GameObject button2;//�Ϸ� �ؼ� ������ ���� �غ��
    public GameObject button3;//�̹� ������ �Ϸ���
    public Slider slider;

    private void OnEnable()
    {
        SliderSet();
    }

    void Start()
    {
        slider.maxValue = 50;
        slider.value = AchievementManager.Instance.redrawCount;

        ButtonSet();
    }

    public void ButtonSet()
    {
        switch (AchievementManager.Instance.achievement07)
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

        //if (AchievementManager.Instance.reward07 == false)
        //{
        //    switch (AchievementManager.Instance.achievement07)
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

        AchievementManager.Instance.achievement07 = 2;
        GameManager.Instance.mainDiamond += 3;

        ButtonSet();
        HomeManager.Instance.PrintDiamond();

        //AchievementManager.Instance.reward07 = true;
    }

    public void SliderSet()
    {
        slider.value = AchievementManager.Instance.redrawCount;

        ButtonSet();
    }
}
