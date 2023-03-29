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
    public int goldChestCount = 0;


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

                //��ȭ���� ����
                for (int q = 0; q < goldChestCount; q++)
                {
                    if (Random.Range(0, 5) == 2) //20% Ȯ��
                    {
                        GameManager.Instance.CreateBox();
                    }
                }
                //���ǻ��� ����
                if(Random.Range(0,13) <= Player.Instance.potionChestLevel)
                    GameManager.Instance.CreateBox2();

                //��� 2�� �Ѿ������ 50%Ȯ���� ���� ����
                if (countBG % 2 == 0)
                {
                    if (Random.Range(0, 21) <= 10 + Player.Instance.regularLevel)
                        GameManager.Instance.CreatePortal();
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
                    GameManager.Instance.CreateClearPortal();
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
            goldChestCount = 2;

            TargetSpot.Instance.SetMaxProgress(30);

            map[0].transform.position = new Vector3(-50, 0, 0);
            map[1].transform.position = new Vector3(0, 0, 0);
            map[2].transform.position = new Vector3(50, 0, 0);
        }
    }
}