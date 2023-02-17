using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    //0 -> 완료하지 못함, 1 -> 완료 해서 보상을 받을 준비됨, 2 -> 이미 보상을 완료함

    //돼지 1000마리 잡기
    public int achievement01 = 0;
    public int pigCount = 0;
    public bool reward01 = false;

    //3번 도전하기
    public int achievement02 = 0;
    public int tryCount = 0;
    public bool reward02 = false;

    private static AchievementManager instance = null;

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

    public static AchievementManager Instance
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

    public void AchievementCheck()
    {
        if (pigCount >= 1000)
            achievement01 = 1;

        if (tryCount >= 3)
            achievement02 = 1;
    }









    public GameObject asd;
    //임시 테스트
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            tryCount++;
            AchievementCheck();
            asd.GetComponent<Achievement_Try>().asdqwe();
        }
    }
}
