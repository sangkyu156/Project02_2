using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public bool stage = false;

    private static IntroSceneManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static IntroSceneManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        //Invoke("GoStage01", 9.8f);

        Invoke("GoStage01", 0.3f);

        GameObject homeManager = Instantiate(Resources.Load<GameObject>("Home/HomeManager"));
    }

    private void Update()
    {

    }

    void GoStage01()
    {
        GameManager.Instance.state = GameManager.SceneState.Stage;
        Time.timeScale = 1.0f;

        GameManager.Instance.curStage = 1;
        SimpleSceneFader.ChangeSceneWithFade("Stage01");
    }

    void GoHome()
    {
        GameManager.Instance.uiSet = false;
        GameManager.Instance.state = GameManager.SceneState.Home;
        Player.Instance.PlayerStop_1();
        Player.Instance.SkillReset();
        GameManager.Instance.TimeScaleSet();
        AchievementManager.Instance.AchievementCheck();
        SimpleSceneFader.ChangeSceneWithFade("Main");
    }
}
