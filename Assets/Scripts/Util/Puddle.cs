using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    float playerSpeed = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "NoDamage")
        {
            playerSpeed = collision.gameObject.GetComponent<Player>().moveSpeed;
            collision.gameObject.GetComponent<Player>().moveSpeed = (collision.gameObject.GetComponent<Player>().moveSpeed - 4.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "NoDamage")
        {
            collision.gameObject.GetComponent<Player>().moveSpeed = playerSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}
