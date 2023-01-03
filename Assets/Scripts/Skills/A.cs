using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class A : MonoBehaviour
{
    public TextMeshProUGUI price;

    void Start()
    {
        int min = (int)Math.Round((int)SkillData.SkillPrice.One * 0.9f);
        int max = (int)Math.Round((int)SkillData.SkillPrice.One * 1.1f);

        price.text = UnityEngine.Random.Range(min, max).ToString();
    }
}
