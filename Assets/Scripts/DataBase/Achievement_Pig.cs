using UnityEngine;
using UnityEngine.UI;

public class Achievement_Pig : MonoBehaviour
{
    public GameObject button1;//완료하지 못함
    public GameObject button2;//완료 해서 보상을 받을 준비됨
    public GameObject button3;//이미 보상을 완료함
    public Slider slider;

    void Start()
    {
        slider.maxValue = 1000;
        slider.value = AchievementManager.Instance.pigCount;

        ButtonSet();
    }

    public void ButtonSet()
    {
        if (AchievementManager.Instance.reward01 == false)
        {
            switch (AchievementManager.Instance.achievement01)
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
        AchievementManager.Instance.achievement01 = 2;
        GameManager.Instance.mainDiamond += 3;

        ButtonSet();
        HomeManager.Instance.PrintDiamond();

        AchievementManager.Instance.reward01 = true;
    }

    //임시 테스트 함수
    public void asdqwe()
    {
        slider.value = AchievementManager.Instance.pigCount;

        ButtonSet();
    }
}
