using UnityEngine;

public class TitleManager : MonoBehaviour
{
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

        //임시
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale++;
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
}
