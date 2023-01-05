using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FireBall : FireBall_Skill
{
    public TextMeshProUGUI price;
    public TextMeshProUGUI explanation;
    public GameObject buyButton;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.One * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.One * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if(Player.Instance.fireBallLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:fireball");
        }
        else
        {
            if(TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>���̾</color></size>\n<size=70%>Level {Player.Instance.fireBallLevel} -> <#3EFF3E>{Player.Instance.fireBallLevel + 1}</color></size>\n\n���ݷ� {curPower} -> <#3EFF3E>{nextPower}</color>\n���ݼӵ� {curCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if(TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>FireBall</color></size>\n<size=70%>Level {Player.Instance.fireBallLevel} -> <#3EFF3E>{Player.Instance.fireBallLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {curCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void FireBallBuy()
    {
        if (Player.Instance.fireBallLevel >= 7)
            return;

        Player.Instance.fireBallLevel++;
        PrintExplanation();
    }
}
