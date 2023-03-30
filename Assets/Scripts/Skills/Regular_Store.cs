using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class Regular_Store : MonoBehaviour
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

        GameManager.Instance.buyCheckAction += BuyCheck;
        GameManager.Instance.buyCheckAction();

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    private void OnDestroy()
    {
        GameManager.Instance.buyCheckAction -= BuyCheck;
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.regularLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:regular");
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>�ܰ�</color></size>\n<size=70%>Level {Player.Instance.regularLevel} -> <#3EFF3E>{Player.Instance.regularLevel + 1}</color></size>\n\n'����' ���� Ȯ���� �����߽��ϴ�.";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>Regular</color></size>\n<size=70%>Level {Player.Instance.regularLevel} -> <#3EFF3E>{Player.Instance.regularLevel + 1}</color></size>\n\nThe probability of a 'Store' appearing has increased.";
            }
        }
    }

    public void SetAbility()
    {
        if (Player.Instance.regularLevel >= 7)
            ItemManager.Instance.weightedRandom.Remove("Regular_Store");//��ų ��Ͽ��� ����        
    }

    //����
    public void RegularBuy()
    {
        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        GameManager.Instance.paymentGold += priceValue;
        Player.Instance.regularLevel++;

        GameManager.Instance.PrintPlayerMoney();
        GameManager.Instance.buyCheckAction();
        PrintExplanation();

        buyButton.interactable = false;
    }

    //���Ű��ɿ���üũ
    public void BuyCheck()
    {
        if (priceValue > Player.Instance.money)
        {
            price.color = Color.red;
        }
        else
        {
            price.color = Color.white;
        }
    }
}
