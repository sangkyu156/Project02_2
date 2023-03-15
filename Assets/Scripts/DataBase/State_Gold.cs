using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class State_Gold : MonoBehaviour
{
    public GameObject butten01;
    public GameObject butten02;
    public TextMeshProUGUI stateLevel;
    public TextMeshProUGUI stateValue;
    public TextMeshProUGUI diaValue;
    public TextMeshProUGUI diaValue2;

    void Start()
    {
        StateManager.Instance.buyAction += ButtenSet;

        StateManager.Instance.buyAction();

        ListSet();
    }

    private void OnDestroy()
    {
        StateManager.Instance.buyAction -= ButtenSet;
    }

    //레벨별 색 값  =  FFFFFF , FFE1E1 , FFC3C3 , FFA5A5 , FF8787 , FF6969 , FF4B4B , FF2D2D , FF0F0F , FF0F5F , FF00FA
    public void ListSet()
    {
        switch (StateManager.Instance.state_StartGoldLevel)
        {
            case 0:
                stateLevel.text = "Lv.<#FFFFFF>1</color>";
                stateValue.text = "+20";
                diaValue.text = "3";
                diaValue2.text = "3";
                break;
            case 1:
                stateLevel.text = "Lv.<#FFE1E1>2</color>";
                stateValue.text = "+40";
                diaValue.text = "4";
                diaValue2.text = "4";
                break;
            case 2:
                stateLevel.text = "Lv.<#FFC3C3>3</color>";
                stateValue.text = "+60";
                diaValue.text = "5";
                diaValue2.text = "5";
                break;
            case 3:
                stateLevel.text = "Lv.<#FFA5A5>4</color>";
                stateValue.text = "+80";
                diaValue.text = "6";
                diaValue2.text = "6";
                break;
            case 4:
                stateLevel.text = "Lv.<#FF8787>5</color>";
                stateValue.text = "+100";
                diaValue.text = "8";
                diaValue2.text = "8";
                break;
            case 5:
                stateLevel.text = "Lv.<#FF6969>6</color>";
                stateValue.text = "+120";
                diaValue.text = "10";
                diaValue2.text = "10";
                break;
            case 6:
                stateLevel.text = "Lv.<#FF4B4B>7</color>";
                stateValue.text = "+140";
                diaValue.text = "12";
                diaValue2.text = "12";
                break;
            case 7:
                stateLevel.text = "Lv.<#FF2D2D>8</color>";
                stateValue.text = "+160";
                diaValue.text = "14";
                diaValue2.text = "14";
                break;
            case 8:
                stateLevel.text = "Lv.<#FF0F0F>9</color>";
                stateValue.text = "+180";
                diaValue.text = "16";
                diaValue2.text = "16";
                break;
            case 9:
                stateLevel.text = "Lv.<#FF0F5F>10</color>";
                stateValue.text = "+200";
                diaValue.text = "20";
                diaValue2.text = "20";
                break;
            case 10:
                stateLevel.text = "Lv.<#FF00FA>MAX</color>";
                stateValue.text = "+250";
                diaValue.text = "0";
                diaValue2.text = "0";
                break;
        }
    }

    //구매할때마다 모든 버튼이 이 함수를 호출해야함 (Action 으로)
    public void ButtenSet()
    {
        if (GameManager.Instance.mainDiamond < int.Parse(diaValue.text) || StateManager.Instance.state_StartGoldLevel == 10)
        {
            butten01.SetActive(true);
            butten02.SetActive(false);
        }
        else
        {
            butten01.SetActive(false);
            butten02.SetActive(true);
        }
    }

    public void StartGoldBuy()
    {
        GameManager.Instance.mainDiamond -= int.Parse(diaValue.text);
        StateManager.Instance.state_StartGoldLevel++;
        StartGoldSet();
        HomeManager.Instance.PrintDiamond();

        ListSet();

        //Action호출
        StateManager.Instance.buyAction();
    }

    //실제 시작골드 적용
    void StartGoldSet()
    {
        switch (StateManager.Instance.state_StartGoldLevel)
        {
            case 1:
                StateManager.Instance.state_StartGold = 20;
                break;
            case 2:
                StateManager.Instance.state_StartGold = 40;
                break;
            case 3:
                StateManager.Instance.state_StartGold = 80;
                break;
            case 4:
                StateManager.Instance.state_StartGold = 100;
                break;
            case 5:
                StateManager.Instance.state_StartGold = 120;
                break;
            case 6:
                StateManager.Instance.state_StartGold = 140;
                break;
            case 7:
                StateManager.Instance.state_StartGold = 160;
                break;
            case 8:
                StateManager.Instance.state_StartGold = 180;
                break;
            case 9:
                StateManager.Instance.state_StartGold = 200;
                break;
            case 10:
                StateManager.Instance.state_StartGold = 250;
                break;
        }
    }
}
