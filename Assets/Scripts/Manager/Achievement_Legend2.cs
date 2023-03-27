using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_Legend2 : MonoBehaviour
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
        slider.value = AchievementManager.Instance.legendSkillCount;

        ButtonSet();
    }

    public void ButtonSet()
    {
        if (AchievementManager.Instance.reward09 == false)
        {
            switch (AchievementManager.Instance.achievement09)
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
        AchievementManager.Instance.achievement09 = 2;
        GameManager.Instance.mainDiamond += 10;

        ButtonSet();
        HomeManager.Instance.PrintDiamond();

        AchievementManager.Instance.reward09 = true;
    }

    public void SliderSet()
    {
        slider.value = AchievementManager.Instance.legendSkillCount;

        ButtonSet();
    }
}