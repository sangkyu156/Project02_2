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

        Invoke("GoStage01", 1.8f);

        GameObject portal = Instantiate(Resources.Load<GameObject>("Home/HomeManager"));
    }

    private void Update()
    {
        //if(stage == false)
        //{
        //    GameObject gameManager = GameObject.Find("GameManager");
        //    if(gameManager != null)
        //    {
        //        stage = true;
        //        GameManager.Instance.state = GameManager.SceneState.Stage;
        //    }
        //}
    }

    void GoStage01()
    {
        SimpleSceneFader.ChangeSceneWithFade("Stage01");
        GameManager.Instance.state = GameManager.SceneState.Stage;
    }
}
