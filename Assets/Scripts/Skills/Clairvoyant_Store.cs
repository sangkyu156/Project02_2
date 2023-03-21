using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clairvoyant_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.Two * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.Two * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
        priceValue = Int32.Parse(price.text);

        ItemManager.Instance.buyCheckAction += BuyCheck;
        ItemManager.Instance.buyCheckAction();

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    private void OnDestroy()
    {
        ItemManager.Instance.buyCheckAction -= BuyCheck;
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        explanation.text = TextUtil.GetText("game:skill:explanation:clairvoyant");
    }

    //구매
    public void ClairvoyantBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            //버튼 흔들리는 액션
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;

        GameManager.Instance.bossDistance.SetActive(true);

        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();
        ItemManager.Instance.buyCheckAction();

        buyButton.interactable = false;
        ItemManager.Instance.weightedRandom.Remove("Clairvoyant_Store");//스킬 목록에서 삭제
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
