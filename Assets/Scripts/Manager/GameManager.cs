using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameManager : MonoBehaviour
{
    public GameObject store;
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
    public int mainDiamond;
    public int stageDiamond;
    public int clearRewardDiamond;
    public int paymentGold;
    public int storCount;
    public int curStage;

    public enum SceneState
    {
        Home,Stage,StartScene
    }
    public SceneState state = SceneState.StartScene;

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
        //임시 테스트
        //mainDiamond = 10;

        state = SceneState.StartScene;


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
                            RepeatCreate_10();
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
                            RepeatCreate_10();
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
                            RepeatCreate_10();
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
                            RepeatCreate_10();
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
        for (int i = 0; i < 20; i++) 
        {
            Spawn();
        }
    }
    public void Create_02()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn2();
        }
    }
    public void Create_03()
    {
        for (int i = 0; i < 20; i++)
        {
            Spawn3();
        }
    }
    public void Create_04()
    {
        for (int i = 0; i < 16; i++)
        {
            Spawn4();
        }
    }
    public void Create_10()
    {
        for (int i = 0; i < 3; i++)
        {
            Spawn10();
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
    public void RepeatCreate_10()
    {
        for (int i = 0; i < 2; i++)
        {
            RepeatSpawn10();
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
        Cow2 cow2 = poolManager.GetFromPool<Cow2>();
        Cow3 cow3 = poolManager.GetFromPool<Cow3>();
    }
    void Spawn10()
    {
        Wasp cow2 = poolManager.GetFromPool<Wasp>();
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
    void RepeatSpawn10()
    {

    }
    public void CoinSpawn()
    {
        Coin coin = poolManager.GetFromPool<Coin>();
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
    public void ReturnPool(Wasp clone)
    {
        poolManager.TakeToPool<Wasp>(clone.idName, clone);
    }
    public void ReturnPool(Coin clone)
    {
        poolManager.TakeToPool<Coin>(clone.idName, clone);
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

    public void CreateStore()
    {
        store.SetActive(true);
        for (int i = 0; i < fieldUI.Length; i++)
        {
            fieldUI[i].SetActive(false);
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

    public void PlayerDeath()
    {
        try
        {
            if(deadPopup.activeSelf == false)
            {
                if (store.activeSelf == true)
                    store.SetActive(false);

                deadPopup.SetActive(true);
                Time.timeScale = 0;
            }
        }
        catch (MissingReferenceException e)
        {
            Time.timeScale = 0;
            if (deadPopup != null && deadPopup.activeSelf == false)
                deadPopup.SetActive(true);
            UnityEngine.Debug.Log($"{e}에러 뜸");
        }
    }

    public void PlayerClear()
    {
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

    public void Retry01()
    {
        uiSet = false;
        SimpleSceneFader.ChangeSceneWithFade("Main");
        GameManager.Instance.state = GameManager.SceneState.Home;
        AchievementManager.Instance.AchievementCheck();
        BGManager.Instance.countBG = 0;
        Time.timeScale = 1;

        Invoke("GoStage01", 0.68f);
    }

    void GoStage01()
    {
        HomeManager.Instance.StageButton_Stage01();
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.state == SceneState.Stage)
        {
            bossDistance = GameObject.Find("Canvas2").transform.GetChild(1).gameObject;
            store = GameObject.Find("Canvas2").transform.GetChild(2).gameObject;
            fieldUI[0] = GameObject.Find("Canvas2").transform.GetChild(3).gameObject;
            fieldUI[1] = GameObject.Find("Canvas2").transform.GetChild(4).gameObject;
            fieldUI[2] = GameObject.Find("Canvas2").transform.GetChild(5).gameObject;
            deadPopup = GameObject.Find("Canvas2").transform.GetChild(6).gameObject;
            clearPopup = GameObject.Find("Canvas2").transform.GetChild(8).gameObject;

            playerMoney = fieldUI[0].transform.GetChild(0).GetChild(0).gameObject;
            playerDiamond = fieldUI[2].transform.GetChild(0).GetChild(0).gameObject;
            distance = bossDistance.transform.GetChild(0).gameObject;

            uiSet = true;
            bossDistance.SetActive(false);

            paymentGold = 0;
            storCount = 0;
            stageDiamond = 0;
            curStage = 1;

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
    }
}
