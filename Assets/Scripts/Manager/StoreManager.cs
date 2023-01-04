using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : Singleton<StoreManager>
{
    public TextMeshProUGUI playerMoney;

    private void OnEnable()
    {
        PlayerMoneyPrint();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void PlayerMoneyPrint()
    {
        playerMoney.text = $"{Player.Instance.money}";
    }
}
