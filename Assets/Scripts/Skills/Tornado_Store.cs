using System;
using TMPro;
using UnityEngine;

public class Tornado_Store : Tornado_Skill
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
        if (Player.Instance.tornadoLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:tornado");
        }
        else if(Player.Instance.tornadoLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>토네이도</color></size>\n<size=70%>Leve <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{curPower}</color>\n공격속도 <#FF2D2D>{Player.Instance.tornadoCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Tornado</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.tornadoCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>토네이도</color></size>\n<size=70%>Leve {Player.Instance.tornadoLevel} -> <#3EFF3E>{Player.Instance.tornadoLevel + 1}</color></size>\n\n공격력 {curPower} -> <#3EFF3E>{nextPower}</color>\n공격속도 {Player.Instance.tornadoCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Tornado</color></size>\n<size=70%>Level {Player.Instance.tornadoLevel} -> <#3EFF3E>{Player.Instance.tornadoLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.tornadoCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void TornadoBuy()
    {
        if (Player.Instance.tornadoLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //버튼 흔들리는 액션
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.tornadoLevel++;

        switch (Player.Instance.tornadoLevel)
        {
            case 1:
                Player.Instance.tornadoCooldown = 2f; break;
            case 2:
                Player.Instance.tornadoCooldown = 1.9f; break;
            case 3:
                Player.Instance.tornadoCooldown = 1.8f; break;
            case 4:
                Player.Instance.tornadoCooldown = 1.7f; break;
            case 5:
                Player.Instance.tornadoCooldown = 1.6f; break;
            case 6:
                Player.Instance.tornadoCooldown = 1.5f; break;
            case 7:
                Player.Instance.tornadoCooldown = 1.3f;
                ItemManager.Instance.weightedRandom.Remove("Tornado_Store"); break;//만랩시 스킬 목록에서 삭제

        }

        buyButton.interactable = false;
        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();

        Player.Instance.TornadoAction();
    }
}
