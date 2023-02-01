using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{

    void Start()
    {
        Invoke("GoStage01", 9.8f);
    }

    void GoStage01()
    {
        SimpleSceneFader.ChangeSceneWithFade("Stage01");
    }
}
