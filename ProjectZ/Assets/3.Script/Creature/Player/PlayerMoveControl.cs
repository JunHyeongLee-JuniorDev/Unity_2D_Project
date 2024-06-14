using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    private Movement2D movement2D;
    private SpriteRenderer _renderer;
    private PlayerGhostControl ghostControl;
    private Animator ob_Ani;
    private MoveAni MoveAni;
    private float x;
    private float y;
    public float X => x;
    // �뽬
    //***********************************************
    private float NormalSpeed = 2f;
    private float DashSpeed = 20f;
    private float DashCount = 0.05f;
    private readonly float DashTime = 0.05f;
    private bool isDashing = false;
    //***********************************************
    /*
     �÷��̾� �̵� ���� ��������

    Ű���� ������ �� �������� ���ݾ� �����̰� ��
    */

    private void Awake()
    {
        ghostControl = transform.GetComponent<PlayerGhostControl>();
        _renderer = transform.GetComponent<SpriteRenderer>();
        ob_Ani = transform.GetComponent<Animator>();
        movement2D = transform.GetComponent<Movement2D>();
        MoveAni = transform.GetComponent<MoveAni>();
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

        if (isDashing)
        {
            StartCoroutine(ghostControl.TryDash_co());
        }
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

    public void TryAttack() // �⺻ ���� 
    {
        // �ִϸ��̼� ���� �ð� Ȯ�� �޼ҵ�
        ob_Ani.SetTrigger("Attack");
    }

    public void TryDash() // �뽬 ���
    {
        if (Input.GetKeyUp(KeyCode.Space) && DashCount <= 0)
        {
            isDashing = true;
            StartCoroutine(ghostControl.TryDash_co());
            movement2D.Move_Speed = DashSpeed;
            DashCount = DashTime;
        }


        if (DashCount > 0)
        {
            DashCount -= Time.deltaTime;
            if (DashCount <= 0)
            {
                isDashing = false;
                movement2D.Move_Speed = NormalSpeed;
            }
        }
    }
}
