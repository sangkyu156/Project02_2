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

    //��ư ������Ʈ ���� �Ʒ��� ������ �׻� Ŭ�� �����ϵ��� �ϴ� �Լ� (������ Skill�� GameObject�� ��ư�� ������ ���λ��� GameObject �� ������������ �׷��� �׳� GameObject�� �÷�����)
    void SetButtonLayer()
    {
        buyButton.SetAsLastSibling();
    }
}
