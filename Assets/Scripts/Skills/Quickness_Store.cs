using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Quickness_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    float extraSpeed = 0;

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
        if (Player.Instance.quicknessLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:quickness");
        }
        else if (Player.Instance.quicknessLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>신속</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n추가 속도 <#FF2D2D>{extraSpeed}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Quickness</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nExtra Speed <#FF2D2D>{extraSpeed}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>신속</color></size>\n<size=70%>Level {Player.Instance.quicknessLevel} -> <#3EFF3E>{Player.Instance.quicknessLevel + 1}</color></size>\n\n추가 속도 {extraSpeed} -> <#3EFF3E>{extraSpeed + 1}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Quickness</color></size>\n<size=70%>Level {Player.Instance.quicknessLevel} -> <#3EFF3E>{Player.Instance.quicknessLevel + 1}</color></size>\n\nExtra Speed {extraSpeed} -> <#3EFF3E>{extraSpeed + 1}</color>";
            }
        }
    }

    public void SetAbility()
    {
        switch (Player.Instance.quicknessLevel)
        {
            case 0:
                extraSpeed = 0;
                break;
            case 1:
                extraSpeed = 1;
                Player.Instance.moveSpeed = 8;
                break;
            case 2:
                extraSpeed = 2;
                Player.Instance.moveSpeed = 9;
                break;
            case 3:
                extraSpeed = 3;
                Player.Instance.moveSpeed = 10;
                break;
            case 4:
                extraSpeed = 4;
                Player.Instance.moveSpeed = 11;
                break;
            case 5:
                extraSpeed = 5;
                Player.Instance.moveSpeed = 12;
                break;
            case 6:
                extraSpeed = 6;
                Player.Instance.moveSpeed = 13;
                break;
            case 7:
                extraSpeed = 7f;
                Player.Instance.moveSpeed = 14;
                ItemManager.Instance.weightedRandom.Remove("Quickness_Store");//스킬 목록에서 삭제
                break;
        }
    }

    //구매
    public void QuicknessBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            //버튼 흔들리는 액션
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.quicknessLevel++;
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
            //buyButton.interactable = false;
        }
        else
        {
            price.color = Color.white;
            //buyButton.interactable = true;
        }
    }
}
