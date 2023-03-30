using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spark_Store : Spark_Skill
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

        GameManager.Instance.buyCheckAction += BuyCheck;
        GameManager.Instance.buyCheckAction();

        GameManager.Instance.skillLockAction += SkillLock;

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    private void OnDestroy()
    {
        GameManager.Instance.buyCheckAction -= BuyCheck;
        GameManager.Instance.skillLockAction -= SkillLock;
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        if (Player.Instance.sparkLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:spark");
        }
        else if (Player.Instance.sparkLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>스파크</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{curPower}</color>\n공격속도 <#FF2D2D>{Player.Instance.sparkCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Spark</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.sparkCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>스파크</color></size>\n<size=70%>Level {Player.Instance.sparkLevel} -> <#3EFF3E>{Player.Instance.sparkLevel + 1}</color></size>\n\n공격력 {curPower} -> <#3EFF3E>{nextPower}</color>\n공격속도 {Player.Instance.sparkCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Spark</color></size>\n<size=70%>Level {Player.Instance.sparkLevel} -> <#3EFF3E>{Player.Instance.sparkLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.sparkCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void SparkBuy()
    {
        if (Player.Instance.sparkLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //버튼 흔들리는 액션
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        if (Player.Instance.sparkLevel == 0)
        {
            Player.Instance.sparkLevel++;
            Player.Instance.attackSkillCount++;
            if (Player.Instance.attackSkillCount >= 4)
            {
                GameManager.Instance.skillLockAction();
                Player.Instance.AttackSkillCheck();
            }
            Player.Instance.sparkLevel--;
        }
        Player.Instance.sparkLevel++;

        switch (Player.Instance.sparkLevel)
        {
            case 1:
            case 2:
            case 3:
                Player.Instance.sparkCooldown = 0.3f; break;
            case 4:
            case 5:
            case 6:
                Player.Instance.sparkCooldown = 0.2f; break;
            case 7:
                Player.Instance.sparkCooldown = 0.1f;
                ItemManager.Instance.weightedRandom.Remove("Spark_Store"); break;//만랩시 스킬 목록에서 삭제
        }

        PrintExplanation();
        GameManager.Instance.PrintPlayerMoney();
        GameManager.Instance.buyCheckAction();
        buyButton.interactable = false;

        Player.Instance.SparkAction();
    }

    //구매가능여부체크
    public void BuyCheck()
    {
        if (priceValue > Player.Instance.money)
        {
            price.color = Color.red;
            //buyButton.interactable = false;
        }
        else
        {
            price.color = Color.white;
            //buyButton.interactable = true;
        }
    }

    //공격스킬 잠그기(공격스킬 4개 모두 정해졌을때)
    void SkillLock()
    {
        if (Player.Instance.sparkLevel == 0)
            buyButton.interactable = false;
    }
}
