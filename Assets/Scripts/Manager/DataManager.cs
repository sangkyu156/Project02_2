using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class PlayerData
{
    public string d_Name;
    public bool[] d_StageCheck = new bool[2];
    public int d_MainDiamond;
    public int d_Achievement01;
    public int d_Achievement02;
    public int d_Achievement03;
    public int d_Achievement04;
    public int d_Achievement05;
    public int d_Achievement06;
    public int d_Achievement07;
    public int d_Achievement08;
    public int d_Achievement09;
    public int d_Achievement10;
    public int d_PigCount;
    public int d_TryCount;
    public int d_RedrawCount;
    public int d_LegendSkillCount;
    public string d_DateTime0;
    public string d_DateTime1;
    public string d_DateTime2;
    public int d_State_PowerLevel;
    public int d_State_Power;
    public int d_State_HealthLevel;
    public int d_State_Health;
    public int d_State_StartGoldLevel;
    public int d_State_StartGold;
    public int d_State_PotionRecoverLevel;
    public int d_State_PotionRecover;
}


public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;
    public string data;
    public string dateTime0;
    public string dateTime1;
    public string dateTime2;

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

        path = Application.persistentDataPath + nowSlot.ToString();
    }

    public static DataManager Instance
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

    }


    public void SaveData()//    Slot 3\nSave date/time : 2023-04-09 (22:54)
    {
        string subPath;

        ToJson();

        data = JsonUtility.ToJson(nowPlayer);
        subPath = DataManager.Instance.path.Substring(0, DataManager.Instance.path.Length - 1);//뒤에 마지막 문자 자르기
        DataManager.Instance.path = subPath + $"{nowSlot}";
        File.WriteAllText(path, data);
    }

    public void ToJson()
    {
        nowPlayer.d_Name = "";
        nowPlayer.d_MainDiamond = GameManager.Instance.mainDiamond;
        nowPlayer.d_Achievement01 = AchievementManager.Instance.achievement01;
        nowPlayer.d_Achievement02 = AchievementManager.Instance.achievement02;
        nowPlayer.d_Achievement03 = AchievementManager.Instance.achievement03;
        nowPlayer.d_Achievement04 = AchievementManager.Instance.achievement04;
        nowPlayer.d_Achievement05 = AchievementManager.Instance.achievement05;
        nowPlayer.d_Achievement06 = AchievementManager.Instance.achievement06;
        nowPlayer.d_Achievement07 = AchievementManager.Instance.achievement07;
        nowPlayer.d_Achievement08 = AchievementManager.Instance.achievement08;
        nowPlayer.d_Achievement09 = AchievementManager.Instance.achievement09;
        nowPlayer.d_Achievement10 = AchievementManager.Instance.achievement10;
        nowPlayer.d_PigCount = AchievementManager.Instance.pigCount;
        nowPlayer.d_TryCount = AchievementManager.Instance.tryCount;
        nowPlayer.d_RedrawCount = AchievementManager.Instance.redrawCount;
        nowPlayer.d_LegendSkillCount = AchievementManager.Instance.legendSkillCount;
        nowPlayer.d_State_PowerLevel = StateManager.Instance.state_PowerLevel;
        nowPlayer.d_State_Power = StateManager.Instance.state_PowerLevel;
        nowPlayer.d_State_HealthLevel = StateManager.Instance.state_HealthLevel;
        nowPlayer.d_State_Health = StateManager.Instance.state_Health;
        nowPlayer.d_State_StartGoldLevel = StateManager.Instance.state_StartGoldLevel;
        nowPlayer.d_State_StartGold = StateManager.Instance.state_StartGold;
        nowPlayer.d_State_PotionRecoverLevel = StateManager.Instance.state_PotionRecoverLevel;
        nowPlayer.d_State_PotionRecover = StateManager.Instance.state_PotionRecover;
        for (int i = 0; i < nowPlayer.d_StageCheck.Length; i++)
        {
            nowPlayer.d_StageCheck[i] = GameManager.Instance.stageCheck[i];
        }
        switch (nowSlot)
        {
            case 0:
                nowPlayer.d_DateTime0 = DateTime.Now.ToString(("yyyy-MM-dd (HH:mm)"));
                break;
            case 1:
                nowPlayer.d_DateTime1 = DateTime.Now.ToString(("yyyy-MM-dd (HH:mm)"));
                break;
            case 2:
                nowPlayer.d_DateTime2 = DateTime.Now.ToString(("yyyy-MM-dd (HH:mm)"));
                break;
        }
    }

    public void LoadData()
    {
        data = File.ReadAllText(path);
        nowPlayer =  JsonUtility.FromJson<PlayerData>(data);

        FromJson();
    }

    public void FromJson()
    {
        GameManager.Instance.mainDiamond = nowPlayer.d_MainDiamond;
        AchievementManager.Instance.achievement01 = nowPlayer.d_Achievement01;
        AchievementManager.Instance.achievement02 = nowPlayer.d_Achievement02;
        AchievementManager.Instance.achievement03 = nowPlayer.d_Achievement03;
        AchievementManager.Instance.achievement04 = nowPlayer.d_Achievement04;
        AchievementManager.Instance.achievement05 = nowPlayer.d_Achievement05;
        AchievementManager.Instance.achievement06 = nowPlayer.d_Achievement06;
        AchievementManager.Instance.achievement07 = nowPlayer.d_Achievement07;
        AchievementManager.Instance.achievement08 = nowPlayer.d_Achievement08;
        AchievementManager.Instance.achievement09 = nowPlayer.d_Achievement09;
        AchievementManager.Instance.achievement10 = nowPlayer.d_Achievement10;
        AchievementManager.Instance.pigCount = nowPlayer.d_PigCount;
        AchievementManager.Instance.redrawCount = nowPlayer.d_RedrawCount;
        AchievementManager.Instance.legendSkillCount = nowPlayer.d_LegendSkillCount;
        StateManager.Instance.state_PowerLevel = nowPlayer.d_State_PowerLevel;
        StateManager.Instance.state_Power = nowPlayer.d_State_PowerLevel;
        StateManager.Instance.state_HealthLevel = nowPlayer.d_State_HealthLevel;
        StateManager.Instance.state_Health = nowPlayer.d_State_Health;
        StateManager.Instance.state_StartGoldLevel = nowPlayer.d_State_StartGoldLevel;
        StateManager.Instance.state_StartGold = nowPlayer.d_State_StartGold;
        StateManager.Instance.state_PotionRecoverLevel = nowPlayer.d_State_PotionRecoverLevel;
        StateManager.Instance.state_PotionRecover = nowPlayer.d_State_PotionRecover;
        for (int i = 0; i < GameManager.Instance.stageCheck.Length; i++)
        {
            GameManager.Instance.stageCheck[i] = nowPlayer.d_StageCheck[i];
        }
        for (int q = 0; q < 3; q++)
        {
            if(q == 0)
                dateTime0 = nowPlayer.d_DateTime0;
            if (q == 1)
                dateTime1 = nowPlayer.d_DateTime1;
            if (q == 2)
                dateTime2 = nowPlayer.d_DateTime2;
        }
    }
    
    public void SlotButton(int slotNum)
    {
        nowSlot = slotNum;
        path = Application.persistentDataPath + nowSlot.ToString();

        if (File.Exists(path))
        {
            LoadData();
            GoHome();
        }
        else
        {
            DataClear();
            GoIntro();
        }
    }

    public void GoIntro()
    {
        GameManager.Instance.state = GameManager.SceneState.Intro;
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Time.timeScale = 1.0f;

        SimpleSceneFader.ChangeSceneWithFade("Intro");
    }

    public void GoHome()
    {
        GameObject homeManager = Instantiate(Resources.Load<GameObject>("Home/HomeManager"));

        GameManager.Instance.state = GameManager.SceneState.Home;
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Time.timeScale = 1.0f;

        SimpleSceneFader.ChangeSceneWithFade("Main");
    }

    public void DataClear()
    {
        nowPlayer = new PlayerData();
        GameManager.Instance.mainDiamond = 0;
        AchievementManager.Instance.achievement01 = 0;
        AchievementManager.Instance.achievement02 = 0;
        AchievementManager.Instance.achievement03 = 0;
        AchievementManager.Instance.achievement04 = 0;
        AchievementManager.Instance.achievement05 = 0;
        AchievementManager.Instance.achievement06 = 0;
        AchievementManager.Instance.achievement07 = 0;
        AchievementManager.Instance.achievement08 = 0;
        AchievementManager.Instance.achievement09 = 0;
        AchievementManager.Instance.achievement10 = 0;
        AchievementManager.Instance.pigCount = 0;
        AchievementManager.Instance.redrawCount = 0;
        AchievementManager.Instance.legendSkillCount = 0;
        StateManager.Instance.state_PowerLevel = 0;
        StateManager.Instance.state_Power = 0;
        StateManager.Instance.state_HealthLevel = 0;
        StateManager.Instance.state_Health = 0;
        StateManager.Instance.state_StartGoldLevel = 0;
        StateManager.Instance.state_StartGold = 0;
        StateManager.Instance.state_PotionRecoverLevel = 0;
        StateManager.Instance.state_PotionRecover = 0;
        for (int i = 0; i < GameManager.Instance.stageCheck.Length; i++)
        {
            GameManager.Instance.stageCheck[i] = false;
        }
        //nowPlayer.d_Name = "";
        //nowPlayer.d_StageCheck[0] = false;
        //nowPlayer.d_StageCheck[1] = false;
        //nowPlayer.d_MainDiamond = 0;
        //nowPlayer.d_Achievement01 = 0;
        //nowPlayer.d_Achievement02 = 0;
        //nowPlayer.d_Achievement03 = 0;
        //nowPlayer.d_Achievement04 = 0;
        //nowPlayer.d_Achievement05 = 0;
        //nowPlayer.d_Achievement06 = 0;
        //nowPlayer.d_Achievement07 = 0;
        //nowPlayer.d_Achievement08 = 0;
        //nowPlayer.d_Achievement09 = 0;
        //nowPlayer.d_Achievement10 = 0;
        //nowPlayer.d_PigCount = 0;
        //nowPlayer.d_TryCount = 0;
        //nowPlayer.d_RedrawCount = 0;
        //nowPlayer.d_LegendSkillCount = 0;
        //nowPlayer.d_DateTime0 = "";
        //nowPlayer.d_DateTime1 = "";
        //nowPlayer.d_DateTime2 = "";
        //nowPlayer.d_State_PowerLevel = 0;
        //nowPlayer.d_State_Power = 0;
        //nowPlayer.d_State_HealthLevel = 0;
        //nowPlayer.d_State_Health = 0;
        //nowPlayer.d_State_StartGoldLevel = 0;
        //nowPlayer.d_State_StartGold = 0;
        //nowPlayer.d_State_PotionRecoverLevel = 0;
        //nowPlayer.d_State_PotionRecover = 0;
    }
}
