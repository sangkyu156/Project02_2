using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackHole_Store : BlackHole_Skill
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public UnityEngine.UI.Button buyButton;

    int priceValue = 0;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.Three * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.Three * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
        priceValue = Int32.Parse(price.text);

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.blackholeLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:blackhole");
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>��Ȧ</color></size>\n<size=70%>Level {Player.Instance.blackholeLevel} -> <#3EFF3E>{Player.Instance.blackholeLevel + 1}</color></size>\n\n���ݷ� {curPower} -> <#3EFF3E>{nextPower}</color>\n���ݼӵ� {Player.Instance.blackholeCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#FFFF32>BlackHole</color></size>\n<size=70%>Level {Player.Instance.blackholeLevel} -> <#3EFF3E>{Player.Instance.blackholeLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.blackholeCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void BlackHoleBuy()
    {
        if (Player.Instance.blackholeLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        Player.Instance.blackholeLevel++;

        switch (Player.Instance.blackholeLevel)
        {
            case 1:
                Player.Instance.blackholeCooldown = 2f; break;
            case 2:
                Player.Instance.blackholeCooldown = 2f; break;
            case 3:
                Player.Instance.blackholeCooldown = 1.9f; break;
            case 4:
                Player.Instance.blackholeCooldown = 1.9f; break;
            case 5:
                Player.Instance.blackholeCooldown = 1.8f; break;
            case 6:
                Player.Instance.blackholeCooldown = 1.7f; break;
            case 7:
                Player.Instance.blackholeCooldown = 1.6f; break;

        }

        buyButton.interactable = false;
        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();

        Player.Instance.BlackHoleAction();
    }
}
