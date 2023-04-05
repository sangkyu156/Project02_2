using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePopup;

    public void PausePopupOn()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Time.timeScale = 0;
        pausePopup.SetActive(true);
    }

    public void PausePopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Time.timeScale = 1;
        pausePopup.SetActive(false);
    }

    public void RetryStage01()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        GameManager.Instance.curStage = 1;
        GameManager.Instance.Retry01();
    }

    public void RetryStage02()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        GameManager.Instance.curStage = 2;
        GameManager.Instance.Retry02();
    }

    public void GoMenu()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
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
