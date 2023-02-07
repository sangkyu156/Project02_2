using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : Singleton<StoreManager>
{
    public TextMeshProUGUI playerMoney;

    private void OnEnable()
    {
        PrintPlayerMoney();
        GameManager.Instance.storCount++;
    }

    public void PrintPlayerMoney()
    {
        playerMoney.text = $"{Player.Instance.money}";
    }
}
