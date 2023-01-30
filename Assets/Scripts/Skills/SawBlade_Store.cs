using System;
using TMPro;

public class SawBlade_Store : SawBlade_Skill
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

        buyButton.transform.SetAsLastSibling();//��ư���� �Ʒ��� ��ġ

        PrintExplanation();
    }

    //���� �ؽ�Ʈ ���
    void PrintExplanation()
    {
        if (Player.Instance.sawBladeLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:sawblade");
        }
        else if(Player.Instance.sawBladeLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>�鳯</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n���ݷ� <#FF2D2D>{sb_CurPower}</color>\n�鳯 ���� <#FF2D2D>8</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>SawBlade</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{sb_CurPower}</color>\nNnumber Of Saw Blades <#FF2D2D>8</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //�ѱ�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>�鳯</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\n���ݷ� {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\n�鳯 ���� {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";

                if(Player.Instance.sawBladeLevel == 6)
                    explanation.text = $"<size=120%><#32FFC8>�鳯</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\n���ݷ� {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\n�鳯 ���� {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";
            }
            else if (TextUtil.languageNumber == 2) //�̱�
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>SawBlade</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\nPower {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\nNnumber Of Saw Blades {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel+1}</color>";

                if (Player.Instance.sawBladeLevel == 6)
                    explanation.text = $"<size=120%><#32FFC8>SawBlade</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\nPower {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\nNnumber Of Saw Blades {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";
            }
        }
    }

    public void SawBladeBuy()
    {
        if (Player.Instance.sawBladeLevel >= 7)
            return;

        if (Player.Instance.money < priceValue)
        {
            //��ư ��鸮�� �׼�
            return;
        }

        Player.Instance.money -= priceValue;
        Player.Instance.sawBladeLevel++;

        //������ ��ų ��Ͽ��� ����
        if(Player.Instance.sawBladeLevel == 7)
            ItemManager.Instance.weightedRandom.Remove("SawBlade_Store");

        buyButton.interactable = false;
        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();

        Player.Instance.SawBladeAdd();

    }
}
