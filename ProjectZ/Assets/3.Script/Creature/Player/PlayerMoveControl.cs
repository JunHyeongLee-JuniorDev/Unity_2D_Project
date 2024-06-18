using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    private Movement2D movement2D;
    private SpriteRenderer _renderer;
    private PlayerGhostControl ghostControl;
    private Animator ob_Ani;
    private PlayerMoveAni MoveAni;
    private float x;
    private float y;
    public float X => x;
    // 대쉬
    //***********************************************
    private float NormalSpeed = 3f;
    public readonly float DashSpeed = 10f;
    public float DashCount = 0.1f;
    private readonly float DashTime = 0.1f;
    //***********************************************
    /*
     플레이어 이동 먼저 구현하자

    키보드 눌리면 그 방향으로 조금씩 움직이게 끔
    */

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
            _renderer.flipX = false;

        else if(x.Equals(-1))
            _renderer.flipX = true;
    }

    public bool IsAttack()
    {
        if (ob_Ani.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Player_Attack1") || ob_Ani.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Player_Attack2"))
            return true;

        else
            return false;
    }

    public void TryAttack() // 기본 공격 
    {
        // 애니메이션 동작 시간 확인 메소드
        ob_Ani.SetTrigger("Attack");
    }

    public void TryDash() // 대쉬 기능
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
