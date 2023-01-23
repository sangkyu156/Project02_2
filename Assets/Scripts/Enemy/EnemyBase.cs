using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int currentHealth;
    protected float speed;
    protected int power;
    public float knockbackTime = 1;
    public bool knockback = false;

    public HealthBar healthBar;
    public Transform textPostion;

    public CapsuleCollider2D collider;
    Transform target = null;
    protected Animator animator;

    private void Awake()
    {
        target = Player.Instance.transform;
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {

    }

    //플레이어 한테 이동
    protected void TargetConfirm()
    {
        if (target != null)
        {
            //Vector3 direction = transform.position - target.position;
            transform.position = Vector2.MoveTowards(transform.position, target.position + new Vector3(0,1,0), speed * Time.deltaTime);
        }
    }
    
    //멈춤
    protected void PositionStop()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 0 * Time.deltaTime);
    }

    //데미지 받았을때
    public void TakeDamage(int damage_)
    {
        currentHealth -= damage_;

        //데미지 출력
        GameObject damageUI = Instantiate(Resources.Load<GameObject>($"DamageTextCanvas")) as GameObject;
        damageUI.GetComponentInChildren<DamageText>().damage = damage_;
        damageUI.transform.SetParent(textPostion, false);

        healthBar.SetHealth(currentHealth);
    }
}
