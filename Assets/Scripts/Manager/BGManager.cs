using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public GameObject[] map;
    public GameObject player;
    float dist = 0f;
    int countBG = 0;

    void Start()
    {
        countBG = 0;
        dist = 0f;
    }

    void Update()
    {
        for (int i = 0; i < map.Length; i++)
        {
            dist = player.transform.position.x - 60;

            if(dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);
                countBG++;
                if(countBG == 1)
                {
                    GameManager.Instance.CreatePortal();
                }
            }
        }
    }
}
