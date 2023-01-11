using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int hp;
    protected int currentHealth;
    protected float speed;
    protected float power;

    public HealthBar healthBar;

    Transform target = null;
    protected Animator animator;

    private void Awake()
    {
        target = Player.Instance.transform;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    //플레이어 한테 이동
    protected void TargetConfirm()
    {
        if (target != null)
        {
            //Vector3 direction = transform.position - target.position;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    
    //멈춤
    protected void PositionStop()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 0 * Time.deltaTime);
    }

    //데미지 받았을때
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
