using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveEnergy_Store : WaveEnergy_Skill
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.One * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.One * 1.1f);

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
        if (Player.Instance.waveEnergyLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:waveenergy");
        }
        else if (Player.Instance.waveEnergyLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>������ ��</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n���ݷ� <#FF2D2D>{curPower}</color>\n���ݼӵ� <#FF2D2D>{Player.Instance.waveEnergyCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Energy Ball</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{curPower}</color>\nCooldown <#FF2D2D>{Player.Instance.waveEnergyCooldown}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>������ ��</color></size>\n<size=70%>Level {Player.Instance.waveEnergyLevel} -> <#3EFF3E>{Player.Instance.waveEnergyLevel + 1}</color></size>\n\n���ݷ� {curPower} -> <#3EFF3E>{nextPower}</color>\n���ݼӵ� {Player.Instance.waveEnergyCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }   
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Energy Ball</color></size>\n<size=70%>Level {Player.Instance.waveEnergyLevel} -> <#3EFF3E>{Player.Instance.waveEnergyLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.waveEnergyCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void EnergyBallBuy()
    {
        if (Player.Instance.waveEnergyLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.waveEnergyLevel++;

        switch (Player.Instance.waveEnergyLevel)
        {
            case 1:
                Player.Instance.waveEnergyCooldown = 1.7f; break;
            case 2:
                Player.Instance.waveEnergyCooldown = 1.6f; break;
            case 3:
                Player.Instance.waveEnergyCooldown = 1.5f; break;
            case 4:
                Player.Instance.waveEnergyCooldown = 1.4f; break;
            case 5:
                Player.Instance.waveEnergyCooldown = 1.3f; break;
            case 6:
                Player.Instance.waveEnergyCooldown = 1.2f; break;
            case 7:
                Player.Instance.waveEnergyCooldown = 1f;
                ItemManager.Instance.weightedRandom.Remove("WaveEnergy_Store"); break;//������ ��ų ��Ͽ��� ����

        }

        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();
        ItemManager.Instance.buyCheckAction();
        buyButton.interactable = false;

        Player.Instance.WaveEnergyAction();
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
