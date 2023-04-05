using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulkingUp_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    int addHealth = 0;

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
        if (Player.Instance.bulkingUpLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:bulkingup");
        }
        else if (Player.Instance.bulkingUpLevel == 15)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>벌크 업</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n추가 최대체력 <#FF2D2D>{addHealth * Player.Instance.bulkingUpLevel}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Bulking Up</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nAdditional Max Health<#FF2D2D>{addHealth * Player.Instance.bulkingUpLevel}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>벌크 업</color></size>\n<size=70%>Level {Player.Instance.bulkingUpLevel} -> <#3EFF3E>{Player.Instance.bulkingUpLevel + 1}</color></size>\n\n추가 최대체력 {addHealth * Player.Instance.bulkingUpLevel} -> <#3EFF3E>{addHealth * Player.Instance.bulkingUpLevel + 3}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Bulking Up</color></size>\n<size=70%>Level {Player.Instance.bulkingUpLevel} -> <#3EFF3E>{Player.Instance.bulkingUpLevel + 1}</color></size>\n\nAdditional Max Health {addHealth * Player.Instance.bulkingUpLevel} -> <#3EFF3E>{addHealth * Player.Instance.bulkingUpLevel + 3}</color>";
            }
        }
    }

    public void SetAbility()
    {
        addHealth = 3;
    }

    //구매
    public void BulkingUpBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            GameManager.Instance.SFXPlay(GameManager.Sfx.DonotBuy);
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        GameManager.Instance.SFXPlay(GameManager.Sfx.Buy);

        Player.Instance.bulkingUpLevel++;

        GameManager.Instance.PrintPlayerMoney();
        GameManager.Instance.buyCheckAction();
        PrintExplanation();

        Player.Instance.maxHealth += addHealth;
        Player.Instance.currentHealth += addHealth;
        Player.Instance.healthBar.SetMaxHealth(Player.Instance.maxHealth);
        Player.Instance.healthBar.SetHealth(Player.Instance.currentHealth);

        if (Player.Instance.bulkingUpLevel == 15)
            ItemManager.Instance.weightedRandom.Remove("BulkingUp_Store");//스킬 목록에서 삭제

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
