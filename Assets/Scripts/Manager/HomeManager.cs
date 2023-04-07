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
    public GameObject stage;
    GameObject homeCanvas;
    GameObject popups;
    GameObject cancelButton;
    public GameObject playerMainDiamond;

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
        if (GameManager.Instance.state != SceneState.Home)
            return;

        Time.timeScale = 1.0f;
    }

    void Update()
    {

    }

    public void StagePopupOn()
    {
        PopupParentsDesignate();

        stagePopup = Instantiate(Resources.Load<GameObject>("Home/StagePopup"), popups.transform);
        popups.transform.SetAsLastSibling();
        stagePopup.SetActive(true);

        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);

        for (int i = 0; i < 5; i++)//스테이지 개수만큼 반복
        {
            if (curStage == i)
            {
                stage = Instantiate(Resources.Load<GameObject>($"Home/Stage0{i + 1}"), stagePopup.transform);
                stage.transform.SetAsLastSibling();
                cancelButton = GameObject.Find("CancelButton");
                cancelButton.transform.SetAsLastSibling();

                GameManager.Instance.curStage = i + 1;
            }
        }
    }

    public void StagePopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(stagePopup);
    }

    public void Set_upPopupOn()
    {
        PopupParentsDesignate();

        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        set_upPopup = Instantiate(Resources.Load<GameObject>("Home/Set-upPopup_Title"), popups.transform);
        popups.transform.SetAsLastSibling();
        set_upPopup.SetActive(true);
    }

    public void Set_upPopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(set_upPopup);
    }

    public void StorePopupOn()
    {
        PopupParentsDesignate();

        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        storePopup = Instantiate(Resources.Load<GameObject>("Home/DiaStorePopup"), popups.transform);
        popups.transform.SetAsLastSibling();
        storePopup.SetActive(true);
    }

    public void StorePopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(storePopup);
    }

    public void AchievementPopupOn()
    {
        PopupParentsDesignate();

        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        achievementPopup = Instantiate(Resources.Load<GameObject>("Home/AchievementPopup"), popups.transform);
        popups.transform.SetAsLastSibling();
        achievementPopup.SetActive(true);
    }

    public void AchievementPopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(achievementPopup);
    }

    public void CreditPopupOn()
    {
        PopupParentsDesignate();

        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        creditPopup = Instantiate(Resources.Load<GameObject>("Home/CreditPopup"), popups.transform);
        popups.transform.SetAsLastSibling();
        creditPopup.SetActive(true);
    }

    public void CreditPopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Destroy(creditPopup);
    }

    public void NextStage()
    {
        Time.timeScale = 1.0f;
        Destroy(stage);

        curStage++;
        if (curStage == 5)
            curStage--;

        for (int i = 0; i < 5; i++)//스테이지 개수만큼 반복
        {
            if (curStage == i)
            {
                stage = Instantiate(Resources.Load<GameObject>($"Home/Stage0{i + 1}"), stagePopup.transform);
                stage.transform.SetAsLastSibling();
                cancelButton = GameObject.Find("CancelButton");
                cancelButton.transform.SetAsLastSibling();
            }
        }
    }

    public void PreviousStage()
    {
        Time.timeScale = 1.0f;
        Destroy(stage);

        curStage--;
        if (curStage == -1)
            curStage++;

        for (int i = 0; i < 5; i++)//스테이지 개수만큼 반복
        {
            if (curStage == i)
            {
                stage = Instantiate(Resources.Load<GameObject>($"Home/Stage0{i + 1}"), stagePopup.transform);
                stage.transform.SetAsLastSibling();
                cancelButton = GameObject.Find("CancelButton");
                cancelButton.transform.SetAsLastSibling();
            }
        }
    }

    public void PrintDiamond()
    {
        playerMainDiamond.GetComponent<TextMeshProUGUI>().text = (GameManager.Instance.mainDiamond).ToString();
    }

    public void LanguageEnglishChoice()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        TextUtil.languageNumber = 2;
    }

    public void LanguageKoreanChoice()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        TextUtil.languageNumber = 1;
    }

    public void StageButton_Stage01()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        GameManager.Instance.state = GameManager.SceneState.Stage;
        Time.timeScale = 1.0f;

        AchievementManager.Instance.tryCount++;
        GameManager.Instance.curStage = 1;
        SimpleSceneFader.ChangeSceneWithFade("Stage01");
    }

    public void StageButton_Stage02()
    {
        if (GameManager.Instance.stageCheck[0] == false)
            return;

        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        GameManager.Instance.state = GameManager.SceneState.Stage;
        Time.timeScale = 1.0f;

        AchievementManager.Instance.tryCount++;
        GameManager.Instance.curStage = 2;
        SimpleSceneFader.ChangeSceneWithFade("Stage02");
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
            HomeSceneSet();

            PrintDiamond();
        }
    }

    public void HomeSet()
    {
        Invoke("HomeSceneSet", 2f);
    }

    //홈화면 구현
    void HomeSceneSet()
    {
        homeCanvas = GameObject.Find("Canvas0").gameObject;
        for (int i = 0; i < homeCanvas.transform.childCount; i++)
        {
            if (homeCanvas.transform.GetChild(i).gameObject.name == "Popups")
                popups = homeCanvas.transform.GetChild(i).gameObject;
        }

        GameObject menu = Instantiate(Resources.Load<GameObject>("Home/Menu"), homeCanvas.transform);
        GameObject playButton = Instantiate(Resources.Load<GameObject>("Home/PlayButton"), homeCanvas.transform);
        GameObject exitButton = Instantiate(Resources.Load<GameObject>("Home/ExitButton"), homeCanvas.transform);
        GameObject mainDiamond = Instantiate(Resources.Load<GameObject>("Home/MainDiamond"), homeCanvas.transform);
        playerMainDiamond = mainDiamond.transform.GetChild(0).GetChild(0).gameObject;

        PrintDiamond();
    }

    void PopupParentsDesignate()
    {
        homeCanvas = GameObject.Find("Canvas0").gameObject;
        for (int i = 0; i < homeCanvas.transform.childCount; i++)
        {
            if (homeCanvas.transform.GetChild(i).name == "Popups")
            {
                popups = homeCanvas.transform.GetChild(i).gameObject;
            }
        }
    }
}
