using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : Singleton<IntroSceneManager>
{
    public bool stage = false;

    void Start()
    {
        Invoke("GoStage01", 9.8f);
    }

    private void Update()
    {
        if(stage == false)
        {
            GameObject gameManager = GameObject.Find("GameManager");
            if(gameManager != null)
            {
                stage = true;
                GameManager.Instance.state = GameManager.SceneState.Stage;
            }
        }
    }

    void GoStage01()
    {
        SimpleSceneFader.ChangeSceneWithFade("Stage01");
    }
}
