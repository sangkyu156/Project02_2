 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class FirstItemManager : MonoBehaviour
{
    public TextMeshProUGUI playerMoney;
    GameObject[] items;

    string[] firstSkillArray; //첫 상점에서 나와야 하는 스킬 모와두는곳
    int[] firstSkillWeightedArray; //첫 상점에서 나와야 하는 스킬 가중치 모와두는곳
    public WRandom.WeightedRandomPicker<string> weightedRandomFirst = new WRandom.WeightedRandomPicker<string>(); //'가중치랜덤' 변수 생성 & 초기화

    private void Awake()
    {
        firstAddSkills();
        items = new GameObject[4];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = this.transform.GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        GameManager.Instance.PrintPlayerMoney();
        GameManager.Instance.storCount++;

        SetSkills();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OverlapRedraw();
        }
    }

    //첫 상점에서 랜덤으로 뽑을 스킬 추가
    void firstAddSkills()
    {
        firstSkillArray = new string[8];
        firstSkillArray[0] = "FireBall_Store";
        firstSkillArray[1] = "SawBlade_Store";
        firstSkillArray[2] = "WaveEnergy_Store";
        firstSkillArray[3] = "Tornado_Store";
        firstSkillArray[4] = "Spark_Store";
        firstSkillArray[5] = "Trident_Store";
        firstSkillArray[6] = "RageExplosion_Store";
        firstSkillArray[7] = "Volcano_Store";

        firstSkillWeightedArray = new int[8];
        firstSkillWeightedArray[0] = 1000;
        firstSkillWeightedArray[1] = 1001;
        firstSkillWeightedArray[2] = 1002;
        firstSkillWeightedArray[3] = 600;
        firstSkillWeightedArray[4] = 601;
        firstSkillWeightedArray[5] = 602;
        firstSkillWeightedArray[6] = 603;
        firstSkillWeightedArray[7] = 31;

        for (int i = 0; i < 8; i++)
        {
            weightedRandomFirst.Add($"{firstSkillArray[i]}", firstSkillWeightedArray[i]);
        }
    }

    //상점 팝업에 랜덤으로, 중복 안되게 스킬 배치
    void SetSkills()
    {
        GameObject prefeb;
        GameObject skill;
        string skillName = "";
        bool overlap = false;
        List<string> skillList = new List<string>();

        for (int i = 0; i < items.Length; i++)
        {
            skillName = weightedRandomFirst.GetRandomPick();
            skillList.Add(skillName);

            prefeb = Resources.Load<GameObject>($"Skills/{skillName}");//프리펩 찾음
            skill = Instantiate(prefeb);//프리펩 생성
            skill.transform.SetParent(items[i].transform, false);//부모 지정
            skill.transform.SetAsFirstSibling();//생성된 오브젝트 맨위로
        }

        overlap = skillList.GroupBy(x => x).All(g => g.Count() == 1);//뽑은 스킬리스트안에 모든 개별 요소의 개수가 1개씩인지 체크
        if (overlap == false)
        {
            OverlapRedraw();
        }
    }

    //생성할때 스킬이 중복으로 걸렸을때 다시뽑기
    public void OverlapRedraw()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }
        SetSkills();
    }

    public void ExitButton_DestroyItem()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }

        Time.timeScale = 1;

        GameManager.Instance.firstStore.SetActive(false);
        for (int i = 0; i < GameManager.Instance.fieldUI.Length; i++)
        {
            GameManager.Instance.fieldUI[i].SetActive(true);
        }

        GameManager.Instance.PrintPlayerMoney();
        Player.Instance.OnDamage();
    }
}
