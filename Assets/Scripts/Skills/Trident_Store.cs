using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trident_Store : Trident_Skill
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

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    private void OnDestroy()
    {
        ItemManager.Instance.buyCheckAction -= BuyCheck;
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.tridentLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:trident");
        }
        else if (Player.Instance.tridentLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>����â</color></size>\n<size=70%>Leve <#FF2D2D>MAX</color></size>\n\n���ݷ� <#FF2D2D>{curPower}</color>\n���ݼӵ� <#FF2D2D>{Player.Instance.tridentCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Trident</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.tridentCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>����â</color></size>\n<size=70%>Leve {Player.Instance.tridentLevel} -> <#3EFF3E>{Player.Instance.tridentLevel + 1}</color></size>\n\n���ݷ� {curPower} -> <#3EFF3E>{nextPower}</color>\n���ݼӵ� {Player.Instance.tridentCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Trident</color></size>\n<size=70%>Level {Player.Instance.tridentLevel} -> <#3EFF3E>{Player.Instance.tridentLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.tridentCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void TridentBuy()
    {
        if (Player.Instance.tridentLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.tridentLevel++;

        switch (Player.Instance.tridentLevel)
        {
            case 1:
                Player.Instance.tridentCooldown = 0.9f; break;
            case 2:
                Player.Instance.tridentCooldown = 0.9f; break;
            case 3:
                Player.Instance.tridentCooldown = 0.8f; break;
            case 4:
                Player.Instance.tridentCooldown = 0.8f; break;
            case 5:
                Player.Instance.tridentCooldown = 0.7f; break;
            case 6:
                Player.Instance.tridentCooldown = 0.7f; break;
            case 7:
                Player.Instance.tridentCooldown = 0.5f;
                ItemManager.Instance.weightedRandom.Remove("Trident_Store"); break;//������ ��ų ��Ͽ��� ����
        }

        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();
        ItemManager.Instance.buyCheckAction();
        buyButton.interactable = false;

        Player.Instance.TridentAction();
    }

    //���Ű��ɿ���üũ
    public void BuyCheck()
    {
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
    }
}
