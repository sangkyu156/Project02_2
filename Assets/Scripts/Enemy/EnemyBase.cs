using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected float hp;
    protected float speed;
    protected float power;

    Transform target = null;

    private void Awake()
    {
        target = Player.Instance.transform;
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    //반경안에 들어오면 플레이어 한테 이동
    protected void TargetConfirm()
    {
        if (target != null)
        {
            //Vector3 direction = transform.position - target.position;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
