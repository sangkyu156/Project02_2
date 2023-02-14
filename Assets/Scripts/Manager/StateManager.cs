using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    public Action buyAction;

    public int state_PowerLevel = 0;
    public int state_Power = 0;
    public int state_HealthLevel = 0;
    public int state_Health = 0;
    public int state_StartGoldLevel = 0;
    public int state_StartGold = 0;
    public int state_PotionRecoverLevel = 0;
    public int state_PotionRecover = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
