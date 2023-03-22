using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    public Action buyCheckAction;

    GameObject[] items;
    string[] skillNameArray; //��ų�� ��͵δ°�
    int[] skillWeightedArray; //��ų ����ġ ��͵δ°�
    public WRandom.WeightedRandomPicker<string> weightedRandom = new WRandom.WeightedRandomPicker<string>(); //'����ġ����' ���� ���� & �ʱ�ȭ

    public enum Potion
    {
        HP_Potion, HP_Potion2, HP_Potion3
    }

    private static ItemManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        AddSkills();
        items = new GameObject[4];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = this.transform.GetChild(i).gameObject;
        }
    }

    public static ItemManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        SetSkills();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OverlapRedraw();
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
            skill.transform.SetAsFirstSibling();//������ ������Ʈ ������
        }

        overlap = skillList.GroupBy(x => x).All(g => g.Count() == 1);//���� ��ų����Ʈ�ȿ� ��� ���� ����� ������ 1�������� üũ
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

        StoreManager.Instance.PrintPlayerMoney(); //�� �ٽ� ���
    }

    //�����Ҷ� ��ų�� �ߺ����� �ɷ����� �ٽû̱�
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

        GameManager.Instance.store.SetActive(false);
        for (int i = 0; i < GameManager.Instance.fieldUI.Length; i++)
        {
            GameManager.Instance.fieldUI[i].SetActive(true);
        }

        GameManager.Instance.PrintPlayerMoney();
        Player.Instance.OnDamage();
    }

}
