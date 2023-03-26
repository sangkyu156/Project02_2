using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Regenerate_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    float healingAmount = 0;

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
        if (Player.Instance.regenerateLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:regenerate");
        }
        else if (Player.Instance.regenerateLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>ü�� ȸ��</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nȸ���� <#FF2D2D>{healingAmount}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Regenerate</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nHealing Amount <#FF2D2D>{healingAmount}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>ü�� ȸ��</color></size>\n<size=70%>Level {Player.Instance.regenerateLevel} -> <#3EFF3E>{Player.Instance.regenerateLevel + 1}</color></size>\n\nȸ���� {healingAmount} -> <#3EFF3E>{healingAmount + 1}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>Regenerate</color></size>\n<size=70%>Level {Player.Instance.regenerateLevel} -> <#3EFF3E>{Player.Instance.regenerateLevel + 1}</color></size>\n\nHealing Amount {healingAmount} -> <#3EFF3E>{healingAmount + 1}</color>";
            }
        }
    }

    public void SetAbility()
    {
        switch (Player.Instance.regenerateLevel)
        {
            case 0:
                healingAmount = 0;
                break;
            case 1:
                healingAmount = 1;
                break;
            case 2:
                healingAmount = 3;
                break;
            case 3:
                healingAmount = 5;
                break;
            case 4:
                healingAmount = 7;
                break;
            case 5:
                healingAmount = 9;
                break;
            case 6:
                healingAmount = 11;
                break;
            case 7:
                healingAmount = 15;
                ItemManager.Instance.weightedRandom.Remove("Regenerate_Store");//��ų ��Ͽ��� ����
                break;
        }
    }

    //����
    public void RegeneratesBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.regenerateLevel++;
        Player.Instance.regenerateCooldown = 10;
        Player.Instance.regenerate = true;

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