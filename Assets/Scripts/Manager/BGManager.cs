using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public GameObject[] map;
    public GameObject player;
    float dist = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < map.Length; i++)
        {
            dist = player.transform.position.x - 60;

            if(dist > map[i].transform.position.x)
            {
                map[i].transform.position += new Vector3(150, 0, 0);
            }
        }
    }
}
