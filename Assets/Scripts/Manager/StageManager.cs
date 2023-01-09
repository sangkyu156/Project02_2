using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum Stage
    {
        Stage01, Stage02, Stage03, Stage04, Stage05,
    }
    public Stage stage;

    void Start()
    {
        stage = Stage.Stage01;
    }

    void Update()
    {
        if(stage == Stage.Stage01)
        {

        }
    }
}
