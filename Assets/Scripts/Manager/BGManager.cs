using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class BGManager : MonoBehaviour
{
    public GameObject[] map;
    public GameObject player;
    public GameObject information;
    float dist = 0f;
    public int countBG = 0;


    private void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static BGManager instance = null;

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

    public static BGManager Instance
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
        //���������� �ƴϸ� �Ʒ� �Լ� ���x
        if (GameManager.Instance.state != SceneState.Stage)
            return;

        //GameManager.Instance.Create_01();
        //GameManager.Instance.Create_02();
    }

    void Update()
    {
        //���������� �ƴϸ� �Ʒ� �Լ� ���x
        if (GameManager.Instance.state != SceneState.Stage)
            return;

        for (int i = 0; i < map.Length; i++)
        {
            try
            {
                dist = player.transform.position.x - 60;
            }
            catch (MissingReferenceException e)
            {
                player = GameObject.Find("Player");
                Debug.Log($"{e}������ ���µ� �÷��̾� �ٽ� ã�Ƽ� ����");
            }

            if (dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);
                countBG++;
                //�ڽ� ����
                for (int q = 0; q < 2; q++)
                {
                    if (Random.Range(0, 3) == 2)
                    {
                        GameManager.Instance.CreateBox();
                    }
                }

                //��� 3�� �Ѿ������ 60%Ȯ���� ���� ����
                if (countBG % 3 == 0)
                {
                    int r = Random.Range(0, 10);
                    Debug.Log($"���� countBG = {countBG}, R = {r}");

                    if(r < 6)
                        GameManager.Instance.CreatePortal();
                }

                if (countBG % 5 == 0)
                {
                    GameManager.Instance.CreateBox2();
                }

                //����� ����
                if (GameManager.Instance.state == SceneState.Stage)
                    TargetSpot.Instance.SetProgress(countBG);

                //�̺�Ʈ
                if (countBG == 1)
                {
                    
                }
                else if (countBG == 2)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_01();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 4)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_01();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 6)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_01();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 8)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_02();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 10)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_02();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 12)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_02();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 14)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_03();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 16)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_03();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 18)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_03();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if (countBG == 20)
                {
                    switch (GameManager.Instance.curStage)
                    {
                        case 1:
                            GameManager.Instance.Create_04();
                            Destroy(information);
                            break;
                        case 2:
                            GameManager.Instance.Create_10();
                            break;
                    }
                }
                else if(countBG == 30)
                {
                    GameManager.Instance.PlayerClear();
                }
            }
        }
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.state == SceneState.Stage)
        {
            player = GameObject.Find("Player");
            information = GameObject.Find("Information");

            Time.timeScale = 1;
            countBG = 0;
            dist = 0f;

            TargetSpot.Instance.SetMaxProgress(30);

            map[0].transform.position = new Vector3(-50, 0, 0);
            map[1].transform.position = new Vector3(0, 0, 0);
            map[2].transform.position = new Vector3(50, 0, 0);
        }
    }
}