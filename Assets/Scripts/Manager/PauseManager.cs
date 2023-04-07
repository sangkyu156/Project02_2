using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePopup;
    public GameObject Canvas2;
    GameObject set_upPopup;

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

    public void Set_upPopupOn()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        set_upPopup = Instantiate(Resources.Load<GameObject>("Home/Set-upPopup_Title"), Canvas2.transform);
        set_upPopup.transform.SetAsLastSibling();
        set_upPopup.SetActive(true);
    }

    public void Set_upPopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(set_upPopup);
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
