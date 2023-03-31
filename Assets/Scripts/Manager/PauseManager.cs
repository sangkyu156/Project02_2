using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePopup;

    public void PausePopupOn()
    {
        Time.timeScale = 0;
        pausePopup.SetActive(true);
    }

    public void PausePopupOff()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
    }

    public void RetryStage01()
    {
        GameManager.Instance.Retry01();
    }

    public void GoMenu()
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
}
