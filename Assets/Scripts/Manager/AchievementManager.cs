using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
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

    //���� 100��������
    public int achievement03 = 0;
    public bool reward03 = false;

    //���� 500��������
    public int achievement04 = 0;
    public bool reward04 = false;

    //'�ٽû̱�'3�� ����
    public int achievement05 = 0;
    public int redrawCount = 0;
    public bool reward05 = false;

    //'�ٽû̱�'10�� ����
    public int achievement06 = 0;
    public bool reward06 = false;

    //'�ٽû̱�'50�� ����
    public int achievement07 = 0;
    public bool reward07 = false;

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
        if (pigCount >= 1000 && achievement01 == 0)
            achievement01 = 1;

        if (tryCount >= 3 && achievement02 == 0)
            achievement02 = 1;

        if (pigCount >= 100 && achievement03 == 0)
            achievement03 = 1;

        if (pigCount >= 500 && achievement04 == 0)
            achievement04 = 1;
    }
}
