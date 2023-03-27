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
        SimpleSceneFader.ChangeSceneWithFade("Main");
        GameManager.Instance.state = GameManager.SceneState.Home;
        AchievementManager.Instance.AchievementCheck();
        BGManager.Instance.countBG = 0;
        Time.timeScale = 1;
    }
}
