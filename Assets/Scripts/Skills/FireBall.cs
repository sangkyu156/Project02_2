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

        buyButton.transform.SetAsLastSibling();//버튼제일 아래로 위치

        PrintExplanation();
    }

    //설명 텍스트 출력
    void PrintExplanation()
    {
        if(Player.Instance.fireBallLevel == 0)
        {
            explanation.text = TextUtil.GetText("game:skill:explanation:fireball0");
        }
        else
        {
            SetAbility();
            
            string asd = TextUtil.GetText("game:skill:explanation:fireball1");
            explanation.text = asd;
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
