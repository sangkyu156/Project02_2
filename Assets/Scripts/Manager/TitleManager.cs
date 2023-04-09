using DG.Tweening.Plugins.Core.PathCore;
using System.IO;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject savedFilePopup;
    public GameObject questionPopup;
    public GameObject titleCamera;
    public GameObject[] bg_1;
    public GameObject[] bg_2;
    public GameObject[] bg_3;
    public GameObject[] bg_4;
    public GameObject[] bg_5;
    public GameObject[] bg_6;
    public GameObject[] bg_7;
    public GameObject[] bg_8;
    public float time_;
    GameObject popups;
    GameObject set_upPopup;

    float a_Time;//배경 변화는 속도

    Vector3 cameraPosition;

    void Start()
    {
        cameraPosition = titleCamera.transform.position;
        time_ = 0;
        a_Time = 0.3f;
    }

    void Update()
    {
        cameraPosition.x += 4 * Time.deltaTime;

        titleCamera.transform.position = cameraPosition;

        time_ += Time.deltaTime;

        if (0 <= time_ && time_ < 4)
        {
            BG_1();
        }
        else if (4 <= time_ && time_ < 8)
        {
            BG_2();
        }
        else if (8 <= time_ && time_ < 12)
        {
            BG_3();
        }
        else if (12 <= time_ && time_ < 16)
        {
            BG_4();
        }
        else if (16 <= time_ && time_ < 20)
        {
            BG_5();
        }
        else if (20 <= time_ && time_ < 24)
        {
            BG_6();
        }
        else if (24 <= time_ && time_ < 28)
        {
            BG_7();
        }
        else if (28 <= time_ && time_ < 32)
        {
            BG_8();
        }
        else if (32 <= time_)
        {
            BGReset();
        }
    }

    void BG_1()
    {
        Color color_;
        for (int i = 0; i < bg_1.Length; i++)
        {
            color_ = bg_1[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_1[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }
    
    void BG_2()
    {
        Color color_;
        for (int i = 0; i < bg_2.Length; i++)
        {
            color_ = bg_2[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_2[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BG_3()
    {
        Color color_;
        for (int i = 0; i < bg_3.Length; i++)
        {
            color_ = bg_3[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_3[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BG_4()
    {
        Color color_;
        for (int i = 0; i < bg_4.Length; i++)
        {
            color_ = bg_4[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_4[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BG_5()
    {
        Color color_;
        for (int i = 0; i < bg_5.Length; i++)
        {
            color_ = bg_5[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_5[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BG_6()
    {
        Color color_;
        for (int i = 0; i < bg_6.Length; i++)
        {
            color_ = bg_6[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_6[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BG_7()
    {
        Color color_;
        for (int i = 0; i < bg_7.Length; i++)
        {
            color_ = bg_7[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_7[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BG_8()
    {
        Color color_;
        for (int i = 0; i < bg_8.Length; i++)
        {
            color_ = bg_8[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * a_Time;
            bg_8[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BGReset()
    {
        Color color_;
        for (int i = 0; i < bg_1.Length; i++)
        {
            color_ = bg_1[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_1[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_2.Length; i++)
        {
            color_ = bg_2[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_2[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_3.Length; i++)
        {
            color_ = bg_3[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_3[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_4.Length; i++)
        {
            color_ = bg_4[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_4[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_5.Length; i++)
        {
            color_ = bg_5[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_5[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_6.Length; i++)
        {
            color_ = bg_6[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_6[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_7.Length; i++)
        {
            color_ = bg_7[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_7[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_8.Length; i++)
        {
            color_ = bg_8[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_8[i].GetComponent<SpriteRenderer>().color = color_;
        }

        titleCamera.transform.position = new Vector3(0, -0.02f, -10);
        cameraPosition = titleCamera.transform.position;
        time_ = 0;
    }

    public void GoIntro()
    {
        DataManager.Instance.DataClear();

        GameManager.Instance.state = GameManager.SceneState.Intro;
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        Time.timeScale = 1.0f;

        SimpleSceneFader.ChangeSceneWithFade("Intro");
    }

    public void Set_upPopupOn()
    {
        popups = GameObject.Find("Canvas").gameObject;

        set_upPopup = Instantiate(Resources.Load<GameObject>("Home/Set-upPopup_Title"), popups.transform);
        set_upPopup.transform.SetAsLastSibling();
        set_upPopup.SetActive(true);
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
    }

    public void Set_upPopupOff()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);
        popups = GameObject.Find("Canvas").gameObject;
        for (int i = 0; i < popups.transform.childCount; i++)
        {
            if (popups.transform.GetChild(i).name == "Set-upPopup_Title(Clone)")
                set_upPopup = popups.transform.GetChild(i).gameObject;
        }
        Destroy(set_upPopup);
    }

    public void StartButton()
    {
        DataManager.Instance.nowSlot = 0;

        string subPath;
        subPath = DataManager.Instance.path.Substring(0, DataManager.Instance.path.Length - 1);//뒤에 마지막 문자 자르기

        if (File.Exists(subPath+0))
            savedFilePopup.SetActive(true);
        else if(File.Exists(subPath + 1))
            savedFilePopup.SetActive(true);
        else if (File.Exists(subPath + 2))
            savedFilePopup.SetActive(true);
        else
            GoIntro();
    }

    public void QuestionPopupOn(int slotNum)
    {
        DataManager.Instance.nowSlot = slotNum;
        DataManager.Instance.path = Application.persistentDataPath + slotNum.ToString();
        questionPopup.SetActive(true);
    }

    public void FileDelete()
    {
        File.Delete(DataManager.Instance.path);
        Debug.Log($"{DataManager.Instance.path} 삭제");
        questionPopup.SetActive(false);
        savedFilePopup.SetActive(false);
    }

    public void Exit()
    {
        questionPopup.SetActive(false);
    }
}
