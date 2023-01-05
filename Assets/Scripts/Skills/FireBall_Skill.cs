using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Skill : MonoBehaviour
{
    protected int curPower = 0;
    protected int nextPower = 0;
    protected float curCooldown = 0;
    protected float nextCooldown = 0;

    void Start()
    {
        SetAbility();
    }

    void Update()
    {
        
    }

    //레벨에 따라서 능력치 세팅
    protected void SetAbility()
    {
        switch (Player.Instance.fireBallLevel)
        {
            case 0:
                curPower = 0;
                nextPower = 0;
                curCooldown = 0;
                nextCooldown = 0;
                break;
            case 1:
                curPower = 3;
                nextPower = 4;
                curCooldown = 1;
                nextCooldown = 1;
                break;
            case 2:
                curPower = 4;
                nextPower = 5;
                curCooldown = 1;
                nextCooldown = 0.9f;
                break;
            case 3:
                curPower = 5;
                nextPower = 6;
                curCooldown = 0.9f;
                nextCooldown = 0.9f;
                break;
            case 4:
                curPower = 6;
                nextPower = 7;
                curCooldown = 0.9f;
                nextCooldown = 0.8f;
                break;
            case 5:
                curPower = 7;
                nextPower = 8;
                curCooldown = 0.8f;
                nextCooldown = 0.8f;
                break;
            case 6:
                curPower = 8;
                nextPower = 9;
                curCooldown = 0.8f;
                nextCooldown = 0.7f;
                break;
            case 7:
                curPower = 10;
                nextPower = 10;
                curCooldown = 0.7f;
                nextCooldown = 0.7f;
                break;
        }
    }
}
