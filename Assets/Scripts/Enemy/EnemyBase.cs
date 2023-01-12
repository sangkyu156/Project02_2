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
    public Transform textPostion;

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
        //Debug.Log($"플레이어 x 값 = {target.transform.position.x}\n내 x값 = {transform.position.x}");
        ////플레이어 위치에 따라 회전
        //if (target.transform.position.x < transform.position.x)
        //{
        //    Debug.Log("플레이어가 나보다 왼쪽에있음");
        //    this.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else
        //{
        //    Debug.Log("플레이어가 나보다 오른쪽에있음");
        //    this.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        //}
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
    public void TakeDamage(int damage_)
    {
        currentHealth -= damage_;

        GameObject damageUI = Instantiate(Resources.Load<GameObject>($"DamageTextCanvas")) as GameObject;
        damageUI.GetComponentInChildren<DamageText>().damage = damage_;//GetComponent<DamageText>().damage = damage_;
        damageUI.transform.SetParent(textPostion, false);

        healthBar.SetHealth(currentHealth);
    }
}
