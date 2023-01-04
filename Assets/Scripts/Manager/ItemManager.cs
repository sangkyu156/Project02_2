using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ItemManager : Singleton<ItemManager>
{
    GameObject[] items;
    string[] skillNameArray; //��ų�� ��͵δ°�
    int[] skillWeightedArray; //��ų ����ġ ��͵δ°�
    WRandom.WeightedRandomPicker<string> weightedRandom = new WRandom.WeightedRandomPicker<string>(); //'����ġ����' ���� ���� & �ʱ�ȭ

    private void Awake()
    {
        items = new GameObject[4];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = this.transform.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        AddSkills();
        SetSkills();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            Redraw();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Item.itemAction();
        }
    }

    //�������� ���� ��ų �߰�
    void AddSkills()
    {
        skillNameArray = new string[Enum.GetValues(typeof(SkillData.Skills)).Length];
        skillWeightedArray = new int[Enum.GetValues(typeof(SkillData.Skills)).Length];

        Debug.Log($"��ų ���� = {Enum.GetValues(typeof(SkillData.Skills)).Length}");

        skillNameArray = typeof(SkillData.Skills).GetEnumNames();//'Skills'���� �̸� �ִ� �͵鸸 �̾ƿ��� (skillNameArray ����)
        SetSkillWeightedArray();//��ų ����ġ ��͵α� (skillWeightedArray ����)

        //��ų,����ġ �ϳ��� �߰� (����ġ�� �������� �߻���)
        for (int i = 0; i < skillNameArray.Length; i++)
        {
            Debug.Log($"[{i + 1}]��° ��ų �̸� = {skillNameArray[i]}\n[{i+1}]��° ��ų ����ġ = {skillWeightedArray[i]}");
            weightedRandom.Add($"{skillNameArray[i]}", skillWeightedArray[i]);
        }
    }

    //���� �˾��� ��������, �ߺ� �ȵǰ� ��ų ��ġ
    void SetSkills()
    {
        GameObject prefeb;
        GameObject skill;
        string skillName = "";
        bool overlap = false;
        List<string> skillList = new List<string>();

        for (int i = 0; i < items.Length; i++)
        {
            skillName = weightedRandom.GetRandomPick();
            skillList.Add(skillName);

            prefeb = Resources.Load<GameObject>($"Skills/{skillName}");//������ ã��
            skill = Instantiate(prefeb);//������ ����
            skill.transform.SetParent(items[i].transform, false);//�θ� ����
            skill.transform.SetAsFirstSibling();//��ư�� RectTransform �̶� ������ GameObject�� �� ������ �׷��� �׳� ���ӿ�����Ʈ�� �� ���� �ø�
        }

        overlap = skillList.GroupBy(x => x).All(g => g.Count() == 1);
        if(overlap == false)
        {
            OverlapRedraw();
        }
    }

    //'SkillData.Skills'���� key�� �ϳ��� �־ key�� �ش��ϴ� value�� �����ϴ� �Լ�
    void SetSkillWeightedArray()
    {
        for (int i = 0; i < skillNameArray.Length; i++)
        {
            skillWeightedArray[i] = (int)Enum.Parse(typeof(SkillData.Skills), skillNameArray[i]);
        }
    }

    //�ٽ� �̱�
    public void Redraw()
    {
        if (Player.Instance.money < 200)
        {
            //�����߶� ��ư ��鸮�� �ִϸ��̼�
            return;
        }

        Player.Instance.money -= 200; //������

        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }
        SetSkills();
        Item.itemAction();

        StoreManager.Instance.PlayerMoneyPrint(); //�� �ٽ� ���
    }

    //�����Ҷ� ��ų�� �ߺ����� �ɷ����� �ٽû̱�
    void OverlapRedraw()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i].transform.GetChild(0).gameObject);
        }
        SetSkills();
        Item.itemAction();
    }
}
