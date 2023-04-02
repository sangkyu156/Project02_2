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
        switch (GameManager.Instance.curStage)
        {
            case 1:
                GameManager.Instance.stageCheck[0] = true;
                break;
            case 2:
                GameManager.Instance.stageCheck[1] = true;
                break;
        }
        rewardPopup.SetActive(true);
        rewardBox.SetActive(false);
        GameManager.Instance.stageDiamond = (int)(GameManager.Instance.killCount * 0.01) + 8;//8은 진행률 100% 보상

        clearRewarText.text = $"-    {GameManager.Instance.clearRewardDiamond}";
        totalText.text = $"-    {GameManager.Instance.stageDiamond + GameManager.Instance.clearRewardDiamond}";

        GameManager.Instance.mainDiamond += (GameManager.Instance.stageDiamond + GameManager.Instance.clearRewardDiamond);
    }

    public void ExitButton_Home()
    {
        switch (GameManager.Instance.curStage)
        {
            case 1:
                GameManager.Instance.stageCheck[0] = true;
                break;
            case 2:
                GameManager.Instance.stageCheck[1] = true;
                break;
        }
        GameManager.Instance.uiSet = false;
        GameManager.Instance.state = GameManager.SceneState.Home;
        Player.Instance.PlayerStop_1();
        Player.Instance.SkillReset();
        GameManager.Instance.TimeScaleSet();
        AchievementManager.Instance.AchievementCheck();
        BGManager.Instance.countBG = 0;
        Time.timeScale = 1;
        SimpleSceneFader.ChangeSceneWithFade("Main");
    }
}
