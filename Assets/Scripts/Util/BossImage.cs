using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class BossImage : MonoBehaviour
{
    public float speed = 1f; 
    private Image img;
    public GameObject i;
    public GameObject ii;
    float dis;

    void Start()
    {
        img = GetComponent<Image>(); // ��� �̹����� ������
    }

    void Update()
    {
        dis = Player.Instance.transform.position.x - Boss.Instance.transform.position.x;
        if (200 <= dis)
        {
            i.SetActive(false);
            ii.SetActive(false);
            float alpha = Mathf.PingPong(Time.time * speed, 1.5f);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }
        else if (200 > dis && dis >= 100)
        {
            i.SetActive(true);
            ii.SetActive(false);
            float alpha = Mathf.PingPong(Time.time * speed, 1f);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }
        else if(100> dis)
        {
            i.SetActive(true);
            ii.SetActive(true);
            float alpha = Mathf.PingPong(Time.time * speed, 0.5f);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }
    }
}
