using System;
using TMPro;

public class FireBall : FireBall_Skill
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

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.fireBallLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:fireball");
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>���̾</color></size>\n<size=70%>Level {Player.Instance.fireBallLevel} -> <#3EFF3E>{Player.Instance.fireBallLevel + 1}</color></size>\n\n���ݷ� {curPower} -> <#3EFF3E>{nextPower}</color>\n���ݼӵ� {Player.Instance.fireBallCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#E7E7E7>FireBall</color></size>\n<size=70%>Level {Player.Instance.fireBallLevel} -> <#3EFF3E>{Player.Instance.fireBallLevel + 1}</color></size>\n\nPower {curPower} -> <#3EFF3E>{nextPower}</color>\nCooldown {Player.Instance.fireBallCooldown} -> <#3EFF3E>{nextCooldown}</color>";
            }
        }
    }

    public void FireBallBuy()
    {
        if (Player.Instance.fireBallLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        Player.Instance.fireBallLevel++;

        switch (Player.Instance.fireBallLevel)
        {
            case 1:
            case 2:
                Player.Instance.fireBallCooldown = 1f; break;
            case 3:
            case 4:
                Player.Instance.fireBallCooldown = 0.9f; break;
            case 5:
            case 6:
                Player.Instance.fireBallCooldown = 0.8f; break;
            case 7:
                Player.Instance.fireBallCooldown = 0.7f; break;

        }

        buyButton.interactable = false;
        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();

        Player.Instance.FireBallAction();
    }
}
