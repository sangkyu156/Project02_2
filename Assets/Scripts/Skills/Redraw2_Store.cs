using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Redraw2_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        price.text = "0";
        priceValue = Int32.Parse(price.text);

        GameManager.Instance.buyCheckAction += BuyCheck;
        GameManager.Instance.buyCheckAction();

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    private void OnDestroy()
    {
        GameManager.Instance.buyCheckAction -= BuyCheck;
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        explanation.text = TextUtil.GetText("game:skill:explanation:redraw2");
    }

    //구매
    public void Redraw2Buy()
    {
        GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        GameManager.Instance.PrintPlayerMoney();
        GameManager.Instance.buyCheckAction();

        ItemManager.Instance.OverlapRedraw();
    }

    //구매가능여부체크
    public void BuyCheck()
    {
        if (priceValue > Player.Instance.money)
        {
            price.color = Color.red;
        }
        else
        {
            price.color = Color.white;
        }
    }
}
