using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class Boss : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    public float bossSpeed = 7;
    bool attack = false;
    Vector3 moveDirectione;

    private static Boss instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bossSpeed = 7;
    }

    public static Boss Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = new Vector3(-100f, -8.5f, 0);
    }

    void Update()
    {
        if (attack == false)
            BossMove();
    }

    void BossMove()
    {
        if (GameManager.Instance.state == GameManager.SceneState.Stage)
        {
            moveDirectione = new Vector3(1, 0);
            rigidbody.velocity = moveDirectione * bossSpeed;
        }
        else
        {
            moveDirectione = new Vector3(0, 0);
            rigidbody.velocity = moveDirectione * bossSpeed;
        }
    }

    void BossAttackOff()
    {
        attack = false;
        animator.SetBool("Attack", false);
        Invoke("PlayerKill", 0);
    }

    void PlayerKill()
    {
        GameManager.Instance.PlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "NoDamage")
        {
            animator.SetBool("Attack", true);
            Invoke("BossAttackOff", 0.6f);
        }
    }
}
