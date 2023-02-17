using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class HomeManager : MonoBehaviour
{
    public GameObject stagePopup;
    public GameObject set_upPopup;
    public GameObject storePopup;
    public GameObject achievementPopup;
    public GameObject creditPopup;
    public GameObject[] stage;
    GameObject homeCanvas;
    public TextMeshProUGUI playerMainDiamond;

    int curStage = 0;

    private static HomeManager instance = null;

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
    }

    public static HomeManager Instance
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
        if (onCount >= 2)
        {
            for (int i = 0; i < stage.Length; i++)
            {
                stage[i].SetActive(false);
            }

            stage[curStage].SetActive(true);
        }

        //모든 스테이지가 꺼져있으면 01스테이지만 킨다
        if (onCount <= 0)
        {
            stage[0].SetActive(true);
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

    public void AchievementPopupOn()
    {
        achievementPopup.SetActive(true);
    }

    public void AchievementPopupOff()
    {
        achievementPopup.SetActive(false);
    }

    public void CreditPopupOn()
    {
        creditPopup.SetActive(true);
    }

    public void CreditPopupOff()
    {
        creditPopup.SetActive(false);
    }

    public void NextStage()
    {
        for (int i = 0; i < stage.Length; i++)
        {
            if (stage[i].activeSelf == true)
                curStage = i;
        }

        if (curStage < 4)
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

    public void StageButton_Stage01()
    {
        GameManager.Instance.state = GameManager.SceneState.Stage;

        SimpleSceneFader.ChangeSceneWithFade("Stage01");
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.state == SceneState.Home)
        {
            //homeCanvas = GameObject.Find("Canvas0").gameObject;
            //GameObject menu = Instantiate(Resources.Load<GameObject>("Home/Menu"), homeCanvas.transform);
            //GameObject playButton = Instantiate(Resources.Load<GameObject>("Home/PlayButton"), homeCanvas.transform);
            //GameObject exitButton = Instantiate(Resources.Load<GameObject>("Home/ExitButton"), homeCanvas.transform);

            stagePopup = GameObject.Find("Popups").transform.GetChild(0).gameObject;
            set_upPopup = GameObject.Find("Popups").transform.GetChild(1).gameObject;
            storePopup = GameObject.Find("Popups").transform.GetChild(2).gameObject;
            achievementPopup = GameObject.Find("Popups").transform.GetChild(3).gameObject;
            creditPopup = GameObject.Find("Popups").transform.GetChild(4).gameObject;

            for (int i = 0; i < stage.Length; i++)
            {
                stage[i] = stagePopup.transform.GetChild(i).gameObject;
            }
        }
    }
}
