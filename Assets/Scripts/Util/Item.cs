using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public RectTransform buyButton;
    public static Action itemAction;

    private void Awake()
    {
        itemAction = () => { SetButtonLayer(); };
    }
    void Start()
    {
        SetButtonLayer();
    }

    //버튼 오브젝트 제일 아래로 내려서 항상 클릭 가능하도록 하는 함수 (하지만 Skill은 GameObject라서 버튼을 내려도 새로생긴 GameObject 가 더내려가있음 그래서 그냥 GameObject를 올려줬음)
    void SetButtonLayer()
    {
        buyButton.SetAsLastSibling();
    }
}
