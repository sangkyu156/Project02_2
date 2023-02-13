using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeManager : Singleton<HomeManager>
{
    public GameObject stagePopup;
    public GameObject set_upPopup;
    public GameObject storePopup;
    public GameObject[] stage;
    public TextMeshProUGUI playerMainDiamond;

    int curStage = 0;

    void Start()
    {
        //임시 테스트
        GameManager.Instance.mainDiamond = 200;

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

    public void Set_upPopupOn()
    {
        set_upPopup.SetActive(true);
    }

    public void Set_upPopupOff()
    {
        set_upPopup.SetActive(false);
    }

    public void StorePopupOn()
    {
        storePopup.SetActive(true);
    }

    public void StorePopupOff()
    {
        storePopup.SetActive(false);
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
        playerMainDiamond.text = $"{GameManager.Instance.mainDiamond}";
    }

    public void LanguageEnglishChoice()
    {
        TextUtil.languageNumber = 2;
    }

    public void LanguageKoreanChoice()
    {
        TextUtil.languageNumber = 1;
    }
}
