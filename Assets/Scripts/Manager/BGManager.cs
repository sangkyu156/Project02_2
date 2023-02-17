using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class BGManager : Singleton<BGManager>
{
    public GameObject[] map;
    public GameObject player;
    public GameObject information;
    float dist = 0f;
    public int countBG = 0;


    private void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        //스테이지씬 아니면 아래 함수 사용x
        if (GameManager.Instance.state != SceneState.Stage)
            return;

        //GameManager.Instance.Create_01();
        //GameManager.Instance.Create_02();
    }

    void Update()
    {
        //스테이지씬 아니면 아래 함수 사용x
        if (GameManager.Instance.state != SceneState.Stage)
            return;

        for (int i = 0; i < map.Length; i++)
        {
            dist = player.transform.position.x - 60;

            if(dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);
                countBG++;
                //박스 생성
                for (int q = 0; q < 2; q++)
                {
                    if (Random.Range(0, 3) == 2)
                    {
                        GameManager.Instance.CreateBox();
                    }
                }

                if(countBG % 5 == 0)
                {
                    GameManager.Instance.CreateBox2();
                }

                //진행률 증가
                TargetSpot.Instance.SetProgress(countBG);

                //이벤트
                if (countBG == 1)
                {
                    GameManager.Instance.CreatePortal();
                }
                else if(countBG == 2)
                {
                    GameManager.Instance.Create_01();
                    Destroy(information);
                }
                else if(countBG == 4)
                {
                    GameManager.Instance.Create_02();
                }
                else if (countBG == 5)
                {
                    GameManager.Instance.Create_03();
                }
                else if (countBG == 6)
                {
                    GameManager.Instance.Create_04();
                }
            }
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameManager.Instance.state == SceneState.Stage)
        {
            Time.timeScale = 1;
            countBG = 0;
            dist = 0f;

            TargetSpot.Instance.SetMaxProgress(30);
        }
    }
}
