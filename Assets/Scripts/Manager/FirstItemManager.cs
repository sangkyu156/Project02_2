 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FirstItemManager : MonoBehaviour
{
    GameObject[] items;

    string[] firstSkillArray; //ù �������� ���;� �ϴ� ��ų ��͵δ°�
    int[] firstSkillWeightedArray; //ù �������� ���;� �ϴ� ��ų ����ġ ��͵δ°�
    public WRandom.WeightedRandomPicker<string> weightedRandomFirst = new WRandom.WeightedRandomPicker<string>(); //'����ġ����' ���� ���� & �ʱ�ȭ

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
        GameManager.Instance.CreateStore();
        ItemManager.Instance.ExitButton_DestroyItem();

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

    //ù �������� �������� ���� ��ų �߰�
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
            skillName = weightedRandomFirst.GetRandomPick();
            skillList.Add(skillName);

            prefeb = Resources.Load<GameObject>($"Skills/{skillName}");//������ ã��
            skill = Instantiate(prefeb);//������ ����
            skill.transform.SetParent(items[i].transform, false);//�θ� ����
            skill.transform.SetAsFirstSibling();//������ ������Ʈ ������
        }

        overlap = skillList.GroupBy(x => x).All(g => g.Count() == 1);//���� ��ų����Ʈ�ȿ� ��� ���� ����� ������ 1�������� üũ
        if (overlap == false)
        {
            OverlapRedraw();
        }
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
        GameManager.Instance.SFXPlay(GameManager.Sfx.Button01);

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
