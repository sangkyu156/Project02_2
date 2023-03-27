using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slowdown_Store : MonoBehaviour
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;
    float reducesSpeed = 0;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.Three * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.Three * 1.1f);

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
        if (Player.Instance.slowdownLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:slowdown");
        }
        else if (Player.Instance.slowdownLevel == 4)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>����</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n���� �̵��ӵ� ���ҷ� <#FF2D2D>{reducesSpeed}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>Slowdown</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nReduction amount of boss movement speed <#FF2D2D>{reducesSpeed}</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>����</color></size>\n<size=70%>Level {Player.Instance.slowdownLevel} -> <#3EFF3E>{Player.Instance.slowdownLevel + 1}</color></size>\n\n���� �̵��ӵ� ���ҷ� {reducesSpeed} -> <#3EFF3E>{reducesSpeed + 0.5f}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>Slowdown</color></size>\n<size=70%>Level {Player.Instance.slowdownLevel} -> <#3EFF3E>{Player.Instance.slowdownLevel + 1}</color></size>\n\nReduction amount of boss movement speed {reducesSpeed} -> <#3EFF3E>{reducesSpeed + 0.5f}</color>";
            }
        }
    }

    public void SetAbility()
    {
        switch (Player.Instance.slowdownLevel)
        {
            case 0:
                reducesSpeed = 0;
                break;
            case 1:
                reducesSpeed = 0.5f;
                Boss.Instance.bossSpeed = 6.5f;
                break;
            case 2:
                reducesSpeed = 1;
                Boss.Instance.bossSpeed = 6;
                break;
            case 3:
                reducesSpeed = 1.5f;
                Boss.Instance.bossSpeed = 5.5f;
                break;
            case 4:
                reducesSpeed = 2;
                Boss.Instance.bossSpeed = 5;
                ItemManager.Instance.weightedRandom.Remove("Slowdown_Store");//��ų ��Ͽ��� ����
                break;
        }
    }

    //����
    public void SlowdownBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.slowdownLevel++;

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
