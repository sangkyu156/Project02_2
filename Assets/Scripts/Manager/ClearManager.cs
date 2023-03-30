using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    public GameObject rewardPopup;
    public GameObject rewardBox;
    public TextMeshProUGUI clearRewarText;
    public TextMeshProUGUI totalText;


    public void RewardPopupOn()
    {
        rewardPopup.SetActive(true);
        rewardBox.SetActive(false);
        GameManager.Instance.stageDiamond = (int)(GameManager.Instance.killCount * 0.01) + 8;//8은 진행률 100% 보상

        clearRewarText.text = $"-    {GameManager.Instance.clearRewardDiamond}";
        totalText.text = $"-    {GameManager.Instance.stageDiamond + GameManager.Instance.clearRewardDiamond}";

        GameManager.Instance.mainDiamond += (GameManager.Instance.stageDiamond + GameManager.Instance.clearRewardDiamond);
    }

    public void ExitButton_Home()
    {
        GameManager.Instance.uiSet = false;
        SimpleSceneFader.ChangeSceneWithFade("Main");
        GameManager.Instance.state = GameManager.SceneState.Home;
        GameManager.Instance.TimeScaleSet();
        AchievementManager.Instance.AchievementCheck();
        BGManager.Instance.countBG = 0;
        Time.timeScale = 1;
    }
}
