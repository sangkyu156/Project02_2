using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPotal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "NoDamage")
        {
            GameManager.Instance.PlayerClear();
            Destroy(gameObject);
        }
    }
}
