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
        if (Player.Instance.quicknessLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:quickness");
        }
        else if (Player.Instance.quicknessLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>�ż�</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n�߰� �ӵ� <#FF2D2D>{extraSpeed}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Quickness</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nExtra Speed <#FF2D2D>{extraSpeed}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>�ż�</color></size>\n<size=70%>Level {Player.Instance.quicknessLevel} -> <#3EFF3E>{Player.Instance.quicknessLevel + 1}</color></size>\n\n�߰� �ӵ� {extraSpeed} -> <#3EFF3E>{extraSpeed + 1}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
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
                ItemManager.Instance.weightedRandom.Remove("Quickness_Store");//��ų ��Ͽ��� ����
                break;
        }
    }

    //����
    public void QuicknessBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.quicknessLevel++;

        GameManager.Instance.bossDistance.SetActive(true);

        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();
        ItemManager.Instance.buyCheckAction();
        PrintExplanation();

        buyButton.interactable = false;
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