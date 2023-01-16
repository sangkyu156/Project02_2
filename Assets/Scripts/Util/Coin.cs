using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Coin : MonoBehaviour
{
    int speed = 10;

    void Start()
    {
        speed = 10;
    }

    void Update()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(10, 10), 0);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].tag == "Player" || targets[i].tag == "NoDamage")
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position + new Vector3(0, 1.5f, 0), speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "NoDamage")
        {
            Player.Instance.money += Random.Range(5, 7);
            GameManager.Instance.PrintPlayerMoney();
            Destroy(this.gameObject);
        }
    }
}
