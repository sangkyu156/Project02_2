using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.ParticleSystem;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI progressValue;
    public TextMeshProUGUI goldPaymentValue;
    public TextMeshProUGUI diamondAcquisitionValue;
    public TextMeshProUGUI storCountValue;

    public GameObject gameoverText;
    public GameObject[] smokeFX;
    bool first = false;

    void Start()
    {
        first = false;
        SmokeFX_Off();
        progressValue.text = $"{(BGManager.Instance.countBG * 100) / 30} %";
        goldPaymentValue.text = $"{GameManager.Instance.paymentGold}";
        diamondAcquisitionValue.text = $"{GameManager.Instance.stageDiamond}";
        storCountValue.text = $"{GameManager.Instance.storCount}";
    }

    void Update()
    {
        if (gameoverText.transform.localPosition.y <= 320 && first == false)
        {
            first = true;
            SmokeFX_On();
        }
    }

    void SmokeFX_Off()
    {
        for (int i = 0; i < smokeFX.Length; i++)
        {
            smokeFX[i].SetActive(false);
        }
    }

    void SmokeFX_On()
    {
        for (int i = 0; i < smokeFX.Length; i++)
        {
            smokeFX[i].SetActive(true);
        }
    }
}
