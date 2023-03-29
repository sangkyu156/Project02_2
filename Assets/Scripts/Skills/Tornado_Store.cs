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

        if (GameManager.Instance.storCount > 1)
        {
            ItemManager.Instance.buyCheckAction += BuyCheck;
            ItemManager.Instance.buyCheckAction();
        }

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.storCount > 1)
            ItemManager.Instance.buyCheckAction -= BuyCheck;
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.tornadoLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:tornado");
        }
        else if(Player.Instance.tornadoLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>����̵�</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n���ݷ� <#FF2D2D>{curPower}</color>\n���ݼӵ� <#FF2D2D>{Player.Instance.tornadoCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Tornado</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.tornadoCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>����̵�</color></size>\n<size=70%>Level {Player.Instance.tornadoLevel} -> <#3EFF3E>{Player.Instance.tornadoLevel + 1}</color></size>\n\n���ݷ� {curPower} -> <#3EFF3E>{nextPower}</color>\n���ݼӵ� {Player.Instance.tornadoCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
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
            //��ư ��鸮�� �׼�
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
                ItemManager.Instance.weightedRandom.Remove("Tornado_Store"); break;//������ ��ų ��Ͽ��� ����
        }

        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();
        if (GameManager.Instance.storCount > 1)
            ItemManager.Instance.buyCheckAction();
        buyButton.interactable = false;

        Player.Instance.TornadoAction();
    }

    //���Ű��ɿ���üũ
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
