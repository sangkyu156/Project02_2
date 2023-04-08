using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string d_Name;
    public bool[] d_StageCheck = new bool[2];
    public int d_MainDiamond;
    public int achievement01;
}


public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;

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
    }

    public static DataManager Instance
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
