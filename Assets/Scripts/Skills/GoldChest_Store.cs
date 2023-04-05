using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldChest_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    int probability = 0;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.One * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.One * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
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
        if (Player.Instance.goldChestLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:goldchest");
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>금화상자</color></size>\n<size=70%>Level {Player.Instance.goldChestLevel} -> <#3EFF3E>{Player.Instance.goldChestLevel + 1}</color></size>\n\n'금화상자' 나올 확률이 증가했습니다.";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Gold Chest</color></size>\n<size=70%>Level {Player.Instance.goldChestLevel} -> <#3EFF3E>{Player.Instance.goldChestLevel + 1}</color></size>\n\nThe probability of a 'Gold Chest' appearing has increased.";
            }
        }
    }

    public void SetAbility()
    {
        switch (Player.Instance.goldChestLevel)
        {
            case 1:
                BGManager.Instance.goldChestCount = 4;
                break;
            case 2:
                BGManager.Instance.goldChestCount = 5;
                break;
            case 3:
                BGManager.Instance.goldChestCount = 6;
                break;
            case 4:
                BGManager.Instance.goldChestCount = 7;
                break;
            case 5:
                BGManager.Instance.goldChestCount = 8;
                break;
            case 6:
                BGManager.Instance.goldChestCount = 9;
                break;
            case 7:
                BGManager.Instance.goldChestCount = 10;
                break;
            case 8:
                BGManager.Instance.goldChestCount = 11;
                break;
            case 9:
                BGManager.Instance.goldChestCount = 12;
                break;
            case 10:
                BGManager.Instance.goldChestCount = 13;
                break;
            case 11:
                BGManager.Instance.goldChestCount = 14;
                break;
            case 12:
                BGManager.Instance.goldChestCount = 15;
                ItemManager.Instance.weightedRandom.Remove("GoldChest_Store");//스킬 목록에서 삭제
                break;
        }
    }

    //구매
    public void GoldChestBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Player.Instance.goldChestLevel++;
        GameManager.Instance.PrintPlayerMoney();
        GameManager.Instance.buyCheckAction();
        PrintExplanation();

        buyButton.interactable = false;
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
