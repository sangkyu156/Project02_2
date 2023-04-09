using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.LowLevel;

public class GameManager : MonoBehaviour
{
    public GameObject store;
    public GameObject firstStore;
    public GameObject deadPopup;
    public GameObject clearPopup;
    public GameObject bossDistance;
    GameObject pausePopup;
    GameObject pauseButton;
    public GameObject[] fieldUI;
    public GameObject playerMoney;
    public GameObject playerDiamond;
    public GameObject distance;
    PoolManager poolManager; //오브젝트 풀링 매니져
    float reSpawnTime = 2f;
    public bool uiSet = false;
    public bool[] stageCheck = new bool[2];
    public int mainDiamond;
    public int stageDiamond;
    public int clearRewardDiamond;
    public int paymentGold;
    public int storCount;
    public int curStage;
    public int killCount;
    public System.Action buyCheckAction;//스킬 구매시 남은돈으로 다른스킬 구매 가능한지 색구분하도록
    public System.Action skillLockAction;//공격스킬 4개 모두 정해졌을때 다른 공격스킬 구매못하도록

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;
    int sfxCursor;
    public enum Sfx
    {
        BlackHole, Button01, Buy, ClearBox, ClearPopup, Coin, DonotBuy, EnemyDie, EnergyBall, FireBall, GameOver, Heal, LegendarySkill, PlayerHit,
        RageExplosion, Spark, Tornado, Trident, Upgrade, Volcano, DiamondReward
    };

    public enum SceneState
    {
        Home,Stage,Title,Intro
    }
    public SceneState state = SceneState.Title;

    private static GameManager instance = null;

    private void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

        poolManager = GetComponent<PoolManager>();
    }

    public static GameManager Instance
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
        state = SceneState.Title;

        //스테이지씬 아니면 아래 함수 사용x
        if (GameManager.Instance.state != SceneState.Stage)
            return;

        reSpawnTime = 2f;

        TextUtil.languageNumber = 2;//언어 설정
        store.SetActive(false);
        for (int i = 0; i < fieldUI.Length; i++)
        {
            fieldUI[i].SetActive(false);
        }

        //Create_01();
        //Create_02();
        //Create_03();
        //Create_04();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            TextUtil.languageNumber = 2; //영어
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TextUtil.languageNumber = 1; //한국어
        }

        if (GameManager.Instance.state == SceneState.Stage)
        {
            if (uiSet == true)
            {
                //보스거리 아이템 활성화중일때
                if (bossDistance.activeSelf == true)
                {
                    float dis = Player.Instance.transform.position.x - Boss.Instance.transform.position.x;
                    distance.GetComponent<TextMeshProUGUI>().text = $"{((int)dis) - 14}";
                }
            }

            reSpawnTime -= Time.deltaTime;
            if (reSpawnTime <= 0)
            {
                reSpawnTime = 2f;
                if (1 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 8)
                {
                    switch (curStage)
                    {
                        case 1:
                            RepeatCreate_01();
                            break;
                        case 2:
                            RepeatCreate_02();
                            break;
                    }
                }
                else if (8 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 15)
                {
                    switch (curStage)
                    {
                        case 1:
                            RepeatCreate_02();
                            break;
                        case 2:
                            RepeatCreate_03();
                            break;
                    }
                }
                else if (15 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 23)
                {
                    switch (curStage)
                    {
                        case 1:
                            RepeatCreate_03();
                            break;
                        case 2:
                            RepeatCreate_04();
                            break;
                    }
                }
                else if (23 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 30)
                {
                    switch (curStage)
                    {
                        case 1:
                            RepeatCreate_04();
                            break;
                        case 2:
                            RepeatCreate_05();
                            break;
                    }
                }
            }
        }
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }

    #region 생성이벤트
    public void Create_01()
    {
        for (int i = 0; i < 15; i++) 
        {
            Spawn();
        }
    }
    public void Create_01_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn();
        }
    }
    public void Create_01_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn();
        }
    }
    public void Create_02()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn2();
        }
    }
    public void Create_02_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn2();
        }
    }
    public void Create_02_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn2();
        }
    }
    public void Create_03()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn3();
        }
    }
    public void Create_03_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn3();
        }
    }
    public void Create_03_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn3();
        }
    }
    public void Create_04()
    {
        for (int i = 0; i < 13; i++)
        {
            Spawn4();
        }
    }
    public void Create_04_1()
    {
        for (int i = 0; i < 18; i++)
        {
            Spawn4();
        }
    }
    public void Create_04_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn4();
        }
    }
    public void Create_Orc()
    {
        for (int i = 0; i < 1; i++)
        {
            SpawnOrc();
        }
    }
    public void Create_10()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn10();
        }
    }
    public void Create_10_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn10();
        }
    }
    public void Create_10_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn10();
        }
    }
    public void Create_11()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn11();
        }
    }
    public void Create_11_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn11();
        }
    }
    public void Create_11_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn11();
        }
    }
    public void Create_12()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn12();
        }
    }
    public void Create_12_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn12();
        }
    }
    public void Create_12_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn12();
        }
    }
    public void Create_13()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn13();
        }
    }
    public void Create_13_1()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn13();
        }
    }
    public void Create_13_2()
    {
        for (int i = 0; i < 25; i++)
        {
            Spawn13();
        }
    }
    public void Create_Orc2()
    {
        for (int i = 0; i < 1; i++)
        {
            SpawnOrc2();
        }
    }
    public void RepeatCreate_01()
    {
        for (int i = 0; i < 2; i++)
        {
            RepeatSpawn();
        }
    }
    public void RepeatCreate_02()
    {
        for (int i = 0; i < 2; i++)
        {
            RepeatSpawn2();
        }
    }
    public void RepeatCreate_03()
    {
        for (int i = 0; i < 2; i++)
        {
            RepeatSpawn3();
        }
    }
    public void RepeatCreate_04()
    {
        for (int i = 0; i < 2; i++)
        {
            RepeatSpawn4();
        }
    }
    public void RepeatCreate_05()
    {
        for (int i = 0; i < 2; i++)
        {
            RepeatSpawn5();
        }
    }
    #endregion

    #region 오브잭트 생성
    void Spawn()
    {
        Chicken chicken = poolManager.GetFromPool<Chicken>();
        Chick chick = poolManager.GetFromPool<Chick>();
    }
    void Spawn2()
    {
        Chicken2 chicken2 = poolManager.GetFromPool<Chicken2>();
        Chick2 chick2 = poolManager.GetFromPool<Chick2>();
    }
    void Spawn3()
    {
        Cow cow = poolManager.GetFromPool<Cow>();
        Cow2 cow2 = poolManager.GetFromPool<Cow2>();
    }
    void Spawn4()
    {
        Cow3 cow3 = poolManager.GetFromPool<Cow3>();
        Wolf wolf = poolManager.GetFromPool<Wolf>();
    }
    void SpawnOrc()
    {
        Orc orc = poolManager.GetFromPool<Orc>();
    }
    void Spawn10()
    {
        Larva larva = poolManager.GetFromPool<Larva>();
        Larva2 larva2 = poolManager.GetFromPool<Larva2>();
    }
    void Spawn11()
    {
        Larva3 larva3 = poolManager.GetFromPool<Larva3>();
        Larva4 larva4 = poolManager.GetFromPool<Larva4>();
    }
    void Spawn12()
    {
        Rat rat = poolManager.GetFromPool<Rat>();
        Rat2 rat2 = poolManager.GetFromPool<Rat2>();
    }
    void Spawn13()
    {
        Rat3 rat3 = poolManager.GetFromPool<Rat3>();
        Lizard orc = poolManager.GetFromPool<Lizard>();
    }
    void SpawnOrc2()
    {
        Orc2 orc = poolManager.GetFromPool<Orc2>();
    }
    void RepeatSpawn()
    {
        Pig pig = poolManager.GetFromPool<Pig>();
    }
    void RepeatSpawn2()
    {
        Pig2 pig2 = poolManager.GetFromPool<Pig2>();
    }
    void RepeatSpawn3()
    {
        Pig3 pig3 = poolManager.GetFromPool<Pig3>();
    }
    void RepeatSpawn4()
    {
        Pig4 pig4 = poolManager.GetFromPool<Pig4>();
    }
    void RepeatSpawn5()
    {
        Pig5 pig5 = poolManager.GetFromPool<Pig5>();
    }
    public void CoinSpawn()
    {
        Coin coin = poolManager.GetFromPool<Coin>();
    }
    public void Coin2Spawn()
    {
        Coin2 coin = poolManager.GetFromPool<Coin2>();
    }
    #endregion

    #region 오브젝트 회수
    public void ReturnPool(Chicken clone)
    {
        poolManager.TakeToPool<Chicken>(clone.idName, clone);
    }
    public void ReturnPool(Chick clone)
    {
        poolManager.TakeToPool<Chick>(clone.idName, clone);
    }
    public void ReturnPool(Chicken2 clone)
    {
        poolManager.TakeToPool<Chicken2>(clone.idName, clone);
    }
    public void ReturnPool(Chick2 clone)
    {
        poolManager.TakeToPool<Chick2>(clone.idName, clone);
    }
    public void ReturnPool(Cow clone)
    {
        poolManager.TakeToPool<Cow>(clone.idName, clone);
    }
    public void ReturnPool(Cow2 clone)
    {
        poolManager.TakeToPool<Cow2>(clone.idName, clone);
    }
    public void ReturnPool(Cow3 clone)
    {
        poolManager.TakeToPool<Cow3>(clone.idName, clone);
    }
    public void ReturnPool(Wolf clone)
    {
        poolManager.TakeToPool<Wolf>(clone.idName, clone);
    }
    public void ReturnPool(Pig clone)
    {
        poolManager.TakeToPool<Pig>(clone.idName, clone);
    }
    public void ReturnPool(Pig2 clone)
    {
        poolManager.TakeToPool<Pig2>(clone.idName, clone);
    }
    public void ReturnPool(Pig3 clone)
    {
        poolManager.TakeToPool<Pig3>(clone.idName, clone);
    }
    public void ReturnPool(Pig4 clone)
    {
        poolManager.TakeToPool<Pig4>(clone.idName, clone);
    }
    public void ReturnPool(Pig5 clone)
    {
        poolManager.TakeToPool<Pig5>(clone.idName, clone);
    }
    public void ReturnPool(Wasp clone)
    {
        poolManager.TakeToPool<Wasp>(clone.idName, clone);
    }
    public void ReturnPool(Coin clone)
    {
        poolManager.TakeToPool<Coin>(clone.idName, clone);
    }
    public void ReturnPool(Coin2 clone)
    {
        poolManager.TakeToPool<Coin2>(clone.idName, clone);
    }
    public void ReturnPool(Orc clone)
    {
        poolManager.TakeToPool<Orc>(clone.idName, clone);
    }
    public void ReturnPool(Larva clone)
    {
        poolManager.TakeToPool<Larva>(clone.idName, clone);
    }
    public void ReturnPool(Larva2 clone)
    {
        poolManager.TakeToPool<Larva2>(clone.idName, clone);
    }
    public void ReturnPool(Larva3 clone)
    {
        poolManager.TakeToPool<Larva3>(clone.idName, clone);
    }
    public void ReturnPool(Larva4 clone)
    {
        poolManager.TakeToPool<Larva4>(clone.idName, clone);
    }
    public void ReturnPool(Rat clone)
    {
        poolManager.TakeToPool<Rat>(clone.idName, clone);
    }
    public void ReturnPool(Rat2 clone)
    {
        poolManager.TakeToPool<Rat2>(clone.idName, clone);
    }
    public void ReturnPool(Rat3 clone)
    {
        poolManager.TakeToPool<Rat3>(clone.idName, clone);
    }
    public void ReturnPool(Lizard clone)
    {
        poolManager.TakeToPool<Lizard>(clone.idName, clone);
    }
    public void ReturnPool(Orc2 clone)
    {
        poolManager.TakeToPool<Orc2>(clone.idName, clone);
    }
    #endregion

    public void PrintPlayerMoney()
    {
        playerMoney.GetComponent<TextMeshProUGUI>().text = $"{Player.Instance.money}";
    }

    public void PrintPlayerStageDiamond()
    {
        playerDiamond.GetComponent<TextMeshProUGUI>().text = $"{stageDiamond}";
    }

    public void CreatePortal()
    {
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/Portal"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + 50, -3.5f, 0);
    }

    public void CreateClearPortal()
    {
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/ClearPotal"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + 20, -6.75f, 0);
    }

    public void CreateStore()
    {
        store.SetActive(true);
        for (int i = 1; i < fieldUI.Length; i++)
        {
            fieldUI[i].SetActive(false);
        }
    }

    public void CreateFirstStore()
    {
        firstStore.SetActive(true);
        for (int i = 1; i < fieldUI.Length; i++)
        {
            fieldUI[i].SetActive(false);
        }
    }

    public void PlayerClear()
    {
        Player.Instance.SkillReset();
        Player.Instance.ColliderOff();

        bgmPlayer.Stop();
        GameManager.Instance.SFXPlay(GameManager.Sfx.ClearPopup);

        try
        {
            if (clearPopup.activeSelf == false)
            {
                if (store.activeSelf == true)
                    store.SetActive(false);

                clearPopup.SetActive(true);
                Time.timeScale = 0;
            }
        }
        catch (MissingReferenceException e)
        {
            Time.timeScale = 0;
            if (clearPopup != null && clearPopup.activeSelf == false)
                clearPopup.SetActive(true);
            UnityEngine.Debug.Log($"{e}에러 뜸");
        }
    }

    public void CreateBox()
    {
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/Box1"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(30f, 60f), Random.Range(-4, -8), 0);
    }

    public void CreateBox2()
    {
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/Box2"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(30f, 60f), Random.Range(-4, -8), 0);
    }

    public void CreatePuddle()
    {
        int range = Random.Range(-5, -9);
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/Puddle"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(30f, 40f), range, 0);
    }

    public void CreateUpRock()
    {
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/UpRock"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(30f, 47f), -5, 0);
    }

    public void CreateDownRock()
    {
        GameObject portal = Instantiate(Resources.Load<GameObject>("Field/DownRock"));
        portal.transform.position = new Vector3(Player.Instance.transform.position.x + Random.Range(30f, 50f), -9, 0);
    }

    public void PlayerDeath()
    {
        Player.Instance.SkillReset();
        Player.Instance.ColliderOff();

        bgmPlayer.Stop();
        GameManager.Instance.SFXPlay(Sfx.GameOver);

        try
        {
            if(deadPopup.activeSelf == false)
            {
                if (store.activeSelf == true)
                    store.SetActive(false);

                deadPopup.SetActive(true);
                //Time.timeScale = 0;
            }
        }
        catch (MissingReferenceException e)
        {
            //Time.timeScale = 0;
            if (deadPopup != null && deadPopup.activeSelf == false)
                deadPopup.SetActive(true);
            UnityEngine.Debug.Log($"{e}에러 뜸");
        }
    }

    public void TimeScaleSet()
    {
        Invoke("TimeScaleSet_1", 1.5f);
    }
    
    void TimeScaleSet_1()
    {
        if (GameManager.Instance.state == GameManager.SceneState.Home)
        {
            Time.timeScale = 1;
            Player.Instance.PlayerStop();
            GameObject fader = GameObject.Find("Scene Fader Canvas(Clone)");
            Color color_ = fader.GetComponentInChildren<Image>().color;
            color_.a = 0;
            fader.GetComponentInChildren<Image>().color = color_;
            UnityEngine.Debug.Log("페이드 이미지 버그 수정 완료");
        }
    }
    
    void BGMPlay_1()
    {
        Invoke("BGMPlay", 0.3f);
    }

    public void BGMPlay()
    {
        switch (state)
        {
            case SceneState.Home:
                bgmPlayer.clip = Instantiate(Resources.Load<AudioClip>("Sound/HomeBGM"));
                bgmPlayer.Play();
                break;
            case SceneState.Stage:
                bgmPlayer.clip = Instantiate(Resources.Load<AudioClip>("Sound/StageBGM"));
                bgmPlayer.Play();
                break;
            case SceneState.Title:
                bgmPlayer.clip = Instantiate(Resources.Load<AudioClip>("Sound/TitleBGM"));
                bgmPlayer.Play();
                break;
            case SceneState.Intro:
                bgmPlayer.clip = Instantiate(Resources.Load<AudioClip>("Sound/IntroBGM"));
                bgmPlayer.Play();
                break;
        }
    }

    public void SFXPlay(Sfx type)
    {
        switch (type)
        {
            case Sfx.BlackHole:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case Sfx.Button01:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            case Sfx.Buy:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            case Sfx.ClearBox:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case Sfx.ClearPopup:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                break;
            case Sfx.Coin:
                sfxPlayer[sfxCursor].clip = sfxClip[5];
                break;
            case Sfx.DonotBuy:
                sfxPlayer[sfxCursor].clip = sfxClip[6];
                break;
            case Sfx.EnemyDie:
                sfxPlayer[sfxCursor].clip = sfxClip[7];
                break;
            case Sfx.EnergyBall:
                sfxPlayer[sfxCursor].clip = sfxClip[8];
                break;
            case Sfx.FireBall:
                sfxPlayer[sfxCursor].clip = sfxClip[9];
                break;
            case Sfx.GameOver:
                sfxPlayer[sfxCursor].clip = sfxClip[10];
                break;
            case Sfx.Heal:
                sfxPlayer[sfxCursor].clip = sfxClip[11];
                break;
            case Sfx.LegendarySkill:
                sfxPlayer[sfxCursor].clip = sfxClip[12];
                break;
            case Sfx.PlayerHit:
                sfxPlayer[sfxCursor].clip = sfxClip[13];
                break;
            case Sfx.RageExplosion:
                sfxPlayer[sfxCursor].clip = sfxClip[14];
                break;
            case Sfx.Spark:
                sfxPlayer[sfxCursor].clip = sfxClip[15];
                break;
            case Sfx.Tornado:
                sfxPlayer[sfxCursor].clip = sfxClip[16];
                break;
            case Sfx.Trident:
                sfxPlayer[sfxCursor].clip = sfxClip[17];
                break;
            case Sfx.Upgrade:
                sfxPlayer[sfxCursor].clip = sfxClip[18];
                break;
            case Sfx.Volcano:
                sfxPlayer[sfxCursor].clip = sfxClip[19];
                break;
            case Sfx.DiamondReward:
                sfxPlayer[sfxCursor].clip = sfxClip[20];
                break;
        }

        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BGMPlay();

        if (state != SceneState.Title)
            DataManager.Instance.SaveData();

        if (GameManager.Instance.state == SceneState.Stage)
        {
            bossDistance = GameObject.Find("Canvas2").transform.GetChild(1).gameObject;
            store = GameObject.Find("Canvas2").transform.GetChild(2).gameObject;
            firstStore = GameObject.Find("Canvas2").transform.GetChild(3).gameObject;
            fieldUI[0] = GameObject.Find("Canvas2").transform.GetChild(4).gameObject;
            fieldUI[1] = GameObject.Find("Canvas2").transform.GetChild(5).gameObject;
            fieldUI[2] = GameObject.Find("Canvas2").transform.GetChild(6).gameObject;
            deadPopup = GameObject.Find("Canvas2").transform.GetChild(7).gameObject;
            clearPopup = GameObject.Find("Canvas2").transform.GetChild(9).gameObject;

            playerMoney = fieldUI[0].transform.GetChild(0).GetChild(0).gameObject;
            playerDiamond = fieldUI[2].transform.GetChild(0).GetChild(0).gameObject;
            distance = bossDistance.transform.GetChild(0).gameObject;

            uiSet = true;
            bossDistance.SetActive(false);

            paymentGold = 0;
            storCount = 0;
            stageDiamond = 0;
            killCount = 0;

            switch (curStage)
            {
                case 1:
                    clearRewardDiamond = 5;
                    break;
                case 2:
                    clearRewardDiamond = 10;
                    break;
            }
        }
        else if (GameManager.Instance.state == SceneState.Home)
            //Player.Instance.PlayerStop();

        BGMPlay_1();
    }
}
