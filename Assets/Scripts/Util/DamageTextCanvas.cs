using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextCanvas : MonoBehaviour
{
    public float destoryTime;
    public float textUpSpeed;

    void Start()
    {
        Invoke("DestroyDamageText", destoryTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, textUpSpeed * Time.deltaTime));
    }

    void DestroyDamageText()
    {
        Destroy(gameObject);
    }
}
