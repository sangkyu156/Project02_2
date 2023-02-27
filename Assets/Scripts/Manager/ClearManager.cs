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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RewardPopupOn()
    {
        rewardPopup.SetActive(true);
        rewardBox.SetActive(false);

        clearRewarText.text = $"-    {GameManager.Instance.clearRewardDiamond}";
        totalText.text = $"-    {GameManager.Instance.stageDiamond + GameManager.Instance.clearRewardDiamond}";

        GameManager.Instance.mainDiamond += (GameManager.Instance.stageDiamond + GameManager.Instance.clearRewardDiamond);
    }

    public void ExitButton_Home()
    {
        SimpleSceneFader.ChangeSceneWithFade("Main");
        GameManager.Instance.state = GameManager.SceneState.Home;
        BGManager.Instance.countBG = 0;
        Time.timeScale = 1;
    }
}
