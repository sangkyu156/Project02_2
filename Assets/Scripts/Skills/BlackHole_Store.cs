using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BlackHole_Store : BlackHole_Skill
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.Three * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.Three * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
        priceValue = Int32.Parse(price.text);

        if (priceValue > Player.Instance.money)
        {
            price.color = Color.red;
            buyButton.interactable = false;
        }
        else
        {
            price.color = Color.white;
            buyButton.interactable = true;
        }

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        if (Player.Instance.blackholeLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:blackhole");
        }
        else if(Player.Instance.blackholeLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>블랙홀</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{curPower}</color>\n공격속도 <#FF2D2D>{Player.Instance.blackholeCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>BlackHole</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.blackholeCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>블랙홀</color></size>\n<size=70%>Level {Player.Instance.blackholeLevel} -> <#3EFF3E>{Player.Instance.blackholeLevel + 1}</color></size>\n\n공격력 {curPower} -> <#3EFF3E>{nextPower}</color>\n공격속도 {Player.Instance.blackholeCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>BlackHole</color></size>\n<size=70%>Level {Player.Instance.blackholeLevel} -> <#3EFF3E>{Player.Instance.blackholeLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.blackholeCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void BlackHoleBuy()
    {
        if (Player.Instance.blackholeLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //버튼 흔들리는 액션
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.blackholeLevel++;

        switch (Player.Instance.blackholeLevel)
        {
            case 1:
                Player.Instance.blackholeCooldown = 2f; break;
            case 2:
                Player.Instance.blackholeCooldown = 2f; break;
            case 3:
                Player.Instance.blackholeCooldown = 1.9f; break;
            case 4:
                Player.Instance.blackholeCooldown = 1.9f; break;
            case 5:
                Player.Instance.blackholeCooldown = 1.8f; break;
            case 6:
                Player.Instance.blackholeCooldown = 1.7f; break;
            case 7:
                Player.Instance.blackholeCooldown = 1.6f;
                ItemManager.Instance.weightedRandom.Remove("BlackHole_Store"); break;//만랩시 스킬 목록에서 삭제

        }

        buyButton.interactable = false;
        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();

        Player.Instance.BlackHoleAction();
    }
}
