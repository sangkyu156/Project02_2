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

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        if (Player.Instance.sawBladeLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:sawblade");
        }
        else if(Player.Instance.sawBladeLevel == 7)
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>톱날</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\n공격력 <#FF2D2D>{sb_CurPower}</color>\n톱날 개수 <#FF2D2D>8</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>SawBlade</color></size>\n<size=70%>Level <#FF2D2D>MAX</color></size>\n\nPower <#FF2D2D>{sb_CurPower}</color>\nNnumber Of Saw Blades <#FF2D2D>8</color>";
            }
        }
        else
        {
            if (TextUtil.languageNumber == 0 || TextUtil.languageNumber == 1) //한국
            {
                SetAbility();
                explanation.text = $"<size=120%><#32FFC8>톱날</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\n공격력 {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\n톱날 개수 {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";

                if(Player.Instance.sawBladeLevel == 6)
                    explanation.text = $"<size=120%><#32FFC8>톱날</color></size>\n<size=70%>Level {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 1}</color></size>\n\n공격력 {sb_CurPower} -> <#3EFF3E>{sb_NextPower}</color>\n톱날 개수 {Player.Instance.sawBladeLevel} -> <#3EFF3E>{Player.Instance.sawBladeLevel + 2}</color>";
            }
            else if (TextUtil.languageNumber == 2) //미국
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
            //버튼 흔들리는 액션
            return;
        }

        Player.Instance.money -= priceValue;
        Player.Instance.sawBladeLevel++;

        //만랩시 스킬 목록에서 삭제
        if(Player.Instance.sawBladeLevel == 7)
            ItemManager.Instance.weightedRandom.Remove("SawBlade_Store");

        buyButton.interactable = false;
        PrintExplanation();
        StoreManager.Instance.PrintPlayerMoney();
        GameManager.Instance.PrintPlayerMoney();

        Player.Instance.SawBladeAdd();

    }
}
