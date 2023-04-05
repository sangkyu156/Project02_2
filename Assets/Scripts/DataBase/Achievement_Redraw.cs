using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_Redraw : MonoBehaviour
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
        slider.maxValue = 3;
        slider.value = AchievementManager.Instance.redrawCount;

        ButtonSet();
    }

    public void ButtonSet()
    {
        if (AchievementManager.Instance.reward05 == false)
        {
            switch (AchievementManager.Instance.achievement05)
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
        GameManager.Instance.SFXPlay(GameManager.Sfx.DiamondReward);

        AchievementManager.Instance.achievement05 = 2;
        GameManager.Instance.mainDiamond += 1;

        ButtonSet();
        HomeManager.Instance.PrintDiamond();

        AchievementManager.Instance.reward05 = true;
    }

    public void SliderSet()
    {
        slider.value = AchievementManager.Instance.redrawCount;

        ButtonSet();
    }
}
