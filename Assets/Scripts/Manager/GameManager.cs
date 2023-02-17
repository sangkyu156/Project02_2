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
    public GameObject[] fieldUI;
    public GameObject playerMoney;
    public GameObject playerDiamond;
    PoolManager poolManager; //오브젝트 풀링 매니져
    float reSpawnTime = 2f;
    public int mainDiamond;
    public int stageDiamond;
    public int paymentGold;
    public int storCount;

    public enum SceneState
    {
        Home,Stage,StartScene
    }
    public SceneState state = SceneState.Home;

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

        //스테이지씬 아니면 아래 함수 사용x
        if (GameManager.Instance.state != SceneState.Stage)
            return;

        reSpawnTime = 2f;
        //아래변수 3개는 스테이지 시작시 초기화 하는것으로 바꿔야함.
        paymentGold = 0;
        storCount = 0;
        stageDiamond = 0;

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
        //RepeatCreate_01();
        //RepeatCreate_02();
        //RepeatCreate_03();
        //RepeatCreate_04();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TextUtil.languageNumber = 2; //영어
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TextUtil.languageNumber = 1; //한국어
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Time.timeScale = 1;
            UnityEngine.Debug.Log($"현재 스텟 = {state}");
        }

        reSpawnTime -= Time.deltaTime;
        if(reSpawnTime <= 0)
        {
            reSpawnTime = 2f;
            if (1 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 8)
            {
                RepeatCreate_01();
            }
            else if(8 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 15)
            {
                RepeatCreate_02();
            }
            else if (15 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 23)
            {
                RepeatCreate_03();
            }
            else if (23 <= BGManager.Instance.countBG && BGManager.Instance.countBG < 30)
            {
                RepeatCreate_04();
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
        for (int i = 0; i < 5; i++) 
        {
            Spawn();
        }
    }
    public void Create_02()
    {
        Spawn2();
    }
    public void Create_03()
    {
        for (int i = 0; i < 3; i++)
        {
            Spawn3();
        }
    }
    public void Create_04()
    {
        for (int i = 0; i < 3; i++)
        {
            Spawn4();
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
        deadPopup.SetActive(true);
        Time.timeScale = 0;
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
            store = GameObject.Find("Canvas2").transform.GetChild(0).gameObject;
            deadPopup = GameObject.Find("Canvas2").transform.GetChild(4).gameObject;
            fieldUI[0] = GameObject.Find("Canvas2").transform.GetChild(1).gameObject;
            fieldUI[1] = GameObject.Find("Canvas2").transform.GetChild(2).gameObject;
            fieldUI[2] = GameObject.Find("Canvas2").transform.GetChild(3).gameObject;

            playerMoney = fieldUI[0].transform.GetChild(0).GetChild(0).gameObject;
            playerDiamond = fieldUI[2].transform.GetChild(0).GetChild(0).gameObject;
        }
    }
}
