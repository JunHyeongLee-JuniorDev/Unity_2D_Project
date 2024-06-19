using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //이동관련
    //****************************************************
    private Movement2D movement2D;
    private SpriteRenderer _renderer;
    private Animator ob_Ani;
    private PlayerMoveAni MoveAni; 
    private float x;
    private float y;
    public float X => x;
    //*****************************************************

    //능력치
    /*
     * 공격력
     * 피
     * 방어력
     * 이동속도
     */
    //*****************************************************
    private float basicDamage = 5f;
    public float BasicDamage => basicDamage;
    private float NormalSpeed = 3f;
    private float MaxHP = 200f;
    private float CurHP = 200f;
    public float TakeDamage
    {
        set
        {
            CurHP -= value;
        }
    }
    private int level;
    public int Level => level;

    
    //*****************************************************
    // 대쉬
    //*****************************************************
    private PlayerGhostControl ghostControl;
    public readonly float DashSpeed = 10f;
    public float DashCount = 0.1f;
    private readonly float DashTime = 0.1f;
    //*****************************************************

    private void Awake()
    {
        ghostControl = transform.GetComponent<PlayerGhostControl>();
        _renderer = transform.GetComponent<SpriteRenderer>();
        ob_Ani = transform.GetComponent<Animator>();
        movement2D = transform.GetComponent<Movement2D>();
        MoveAni = transform.GetComponent<PlayerMoveAni>();
    }

    void Start()
    {
        if (movement2D.Move_Speed <= 0f)
        {
            movement2D.Move_Speed = NormalSpeed;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryAttack();

        PlayerMove();

        if (movement2D.Move_Speed.Equals(DashSpeed))
            ghostControl.TryDash();
    }

    public void PlayerMove()
    {
        x = (float)Input.GetAxisRaw("Horizontal");
        y = (float)Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, y, 0);

        if (!IsAttack())
        {

                TryDash();
            MoveAni.MoveMotion(x, y);
            movement2D.MoveTo(new Vector3(x, y, 0));
        }

        else
        {
            movement2D.MoveTo(Vector3.zero);
        }

        if (x.Equals(1))
        {
            _renderer.flipX = false;
        }

        else if (x.Equals(-1))
        {
            _renderer.flipX = true;
        }
    }

    private void LevelUp()
    {
        level += 1;
    }

    public bool IsAttack()
    {
        if (ob_Ani.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Player_Attack1") || ob_Ani.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Player_Attack2"))
            return true;

        else
            return false;
    }

    private void TryAttack() // 기본 공격 
    {
        // 애니메이션 동작 시간 확인 메소드
        ob_Ani.SetTrigger("Attack");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyHitBox"))
            ob_Ani.SetTrigger("Hit");
    }

    private void TryDash() // 대쉬 기능
    {

        if (Input.GetKeyUp(KeyCode.Space) && DashCount <= 0)
        {
            movement2D.Move_Speed = DashSpeed;
            DashCount = DashTime;
        }


        if (DashCount > 0)
        {
            DashCount -= Time.deltaTime;
            if (DashCount <= 0)
            {
                movement2D.Move_Speed = NormalSpeed;
            }
        }
    }
}
