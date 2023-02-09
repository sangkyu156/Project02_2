using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public GameObject stagePopup;
    public GameObject[] stage;
    public TextMeshProUGUI playerDiamond;

    int curStage = 0;

    void Start()
    {
        PrintDiamond();
    }

    void Update()
    {
        
    }

    public void StagePopupOn()
    {
        int onCount = 0;

        stagePopup.SetActive(true);

        for (int i = 0; i < stage.Length; i++)
        {
            if (stage[i].activeSelf == true)
            {
                curStage = i;
                onCount++;
            }
        }

        //여러 스테이지가 켜져있으면 다끄고 최고 스테이지만 킨다
        if(onCount >= 2)
        {
            for (int i = 0; i < stage.Length; i++)
            {
                stage[i].SetActive(false);
            }

            stage[curStage].SetActive(true);
        }
    }

    public void StagePopupOff()
    {
        stagePopup.SetActive(false);
    }

    public void NextStage()
    {
        for (int i = 0; i < stage.Length; i++)
        {
            if (stage[i].activeSelf == true)
                curStage = i;
        }

        if(curStage < 4)
        {
            for (int i = 0; i < stage.Length; i++)
            {
                stage[i].SetActive(false);
            }

            stage[curStage + 1].SetActive(true);
        }
    }

    public void PreviousStage()
    {
        for (int i = 0; i < stage.Length; i++)
        {
            if (stage[i].activeSelf == true)
                curStage = i;
        }

        if (curStage > 0)
        {
            for (int i = 0; i < stage.Length; i++)
            {
                stage[i].SetActive(false);
            }

            stage[curStage - 1].SetActive(true);
        }
    }

    public void PrintDiamond()
    {
        playerDiamond.text = $"{GameManager.Instance.mainDiamond}";
    }
}
