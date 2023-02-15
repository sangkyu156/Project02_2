using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    //0 -> �Ϸ����� ����, 1 -> �Ϸ� �ؼ� ������ ���� �غ��, 2 -> �̹� ������ �Ϸ���

    //���� 1000���� ���
    public int achievement01 = 0;
    public int pigCount = 0;
    public bool reward01 = false;

    //3�� �����ϱ�
    public int achievement02 = 0;
    public int tryCount = 0;
    public bool reward02 = false;

    public void AchievementCheck()
    {
        if (pigCount >= 1000)
            achievement01 = 1;

        if (tryCount >= 3)
            achievement02 = 1;
    }









    public GameObject asd;
    //�ӽ� �׽�Ʈ
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
