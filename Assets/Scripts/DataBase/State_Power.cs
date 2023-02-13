using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class State_Power : MonoBehaviour
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

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        StateManager.Instance.buyAction -= ButtenSet;
    }

    //레벨별 색 값  =  FFFFFF , FFE1E1 , FFC3C3 , FFA5A5 , FF8787 , FF6969 , FF4B4B , FF2D2D , FF0F0F , FF0F5F , FF00FA
    public void ListSet()
    {
        switch (StateManager.Instance.state_PowerLevel)
        {
            case 0:
                stateLevel.text = "Lv.<#FFFFFF>1</color>";
                stateValue.text = "0  ->  1";
                diaValue.text = "3";
                diaValue2.text = "3";
                break;
            case 1:
                stateLevel.text = "Lv.<#FFE1E1>2</color>";
                stateValue.text = "1  ->  2";
                diaValue.text = "4";
                diaValue2.text = "4";
                break;
            case 2:
                stateLevel.text = "Lv.<#FFC3C3>3</color>";
                stateValue.text = "2  ->  3";
                diaValue.text = "5";
                diaValue2.text = "5";
                break;
            case 3:
                stateLevel.text = "Lv.<#FFA5A5>4</color>";
                stateValue.text = "3  ->  4";
                diaValue.text = "6";
                diaValue2.text = "6";
                break;
            case 4:
                stateLevel.text = "Lv.<#FF8787>5</color>";
                stateValue.text = "4  ->  5";
                diaValue.text = "8";
                diaValue2.text = "8";
                break;
            case 5:
                stateLevel.text = "Lv.<#FF6969>6</color>";
                stateValue.text = "5  ->  6";
                diaValue.text = "10";
                diaValue2.text = "10";
                break;
            case 6:
                stateLevel.text = "Lv.<#FF4B4B>7</color>";
                stateValue.text = "6  ->  7";
                diaValue.text = "12";
                diaValue2.text = "12";
                break;
            case 7:
                stateLevel.text = "Lv.<#FF2D2D>8</color>";
                stateValue.text = "7  ->  8";
                diaValue.text = "14";
                diaValue2.text = "14";
                break;
            case 8:
                stateLevel.text = "Lv.<#FF0F0F>9</color>";
                stateValue.text = "8  ->  9";
                diaValue.text = "16";
                diaValue2.text = "16";
                break;
            case 9:
                stateLevel.text = "Lv.<#FF0F5F>10</color>";
                stateValue.text = "9  ->  10";
                diaValue.text = "20";
                diaValue2.text = "20";
                break;
            case 10:
                stateLevel.text = "Lv.<#FF00FA>MAX</color>";
                stateValue.text = "10";
                diaValue.text = "0";
                diaValue2.text = "0";
                break;
        }
    }

    //구매할때마다 모든 버튼이 이 함수를 호출해야함 (Action 으로)
    public void ButtenSet()
    {
        if(GameManager.Instance.mainDiamond < int.Parse(diaValue.text) || StateManager.Instance.state_PowerLevel == 10)
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

    public void PowerBuy()
    {
        GameManager.Instance.mainDiamond -= int.Parse(diaValue.text);
        StateManager.Instance.state_PowerLevel++;
        PowerSet();
        HomeManager.Instance.PrintDiamond();

        ListSet();

        //Action호출
        StateManager.Instance.buyAction();
    }

    //실제 파워 적용
    void PowerSet()
    {
        StateManager.Instance.state_Power = StateManager.Instance.state_PowerLevel;
    }
}
