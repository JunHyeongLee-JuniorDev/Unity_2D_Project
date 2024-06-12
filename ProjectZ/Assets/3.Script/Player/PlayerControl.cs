using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Movement2D movement2D;
    private Animator Player_Ani;
    private AudioSource Player_Audio;
    private SpriteRenderer Player_Renderer;

    private bool isAttacking = false;
    private bool isFristAttack = true;
    /*
     플레이어 이동 먼저 구현하자

    키보드 눌리면 그 방향으로 조금씩 움직이게 끔
    */

    private void Awake()
    {
        movement2D = transform.GetComponent<Movement2D>();
        Player_Ani = transform.GetComponent<Animator>();
        Player_Audio = transform.GetComponent<AudioSource>();
        Player_Renderer = transform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if(movement2D.Move_Speed <= 0f)
        {
            movement2D.Move_Speed = 2f;
        }
    }

    void Update()
    {
        float x = (float)Input.GetAxisRaw("Horizontal");
        float y = (float)Input.GetAxisRaw("Vertical");


        PlayerMoveMotion(x, y);
        PlayerAttackMotion();

        if(!isAttacking)
        movement2D.MoveTo(new Vector3(x, y, 0));

        else
        {
            PlayerStop();
        }
    }



    public void PlayerMoveMotion(float x, float y) // 이동 모션 작동 메소드
    {
        Player_Ani.SetBool("Run", false);
        Player_Ani.SetBool("Idle", false);

        if (x.Equals(1) || x.Equals(-1) || y.Equals(1) || y.Equals(-1))
            Player_Ani.SetBool("Run", true);

        else if (x.Equals(0) && y.Equals(0))
            Player_Ani.SetBool("Idle", true);

        if (x.Equals(-1))
            Player_Renderer.flipX = true;

        else if (x.Equals(1))
            Player_Renderer.flipX = false;
    }   

    public void PlayerAttackMotion()// 공격 모션 작동 메소드
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (isFristAttack && isAttacking)
                isFristAttack = false;

            else if (!isAttacking)
            {
                StartCoroutine("TryFirstAttack_co");
            }
        }
    }

    public IEnumerator TryFirstAttack_co() // 첫 기본 공격 
    {
        isAttacking = true;

        if (isFristAttack)
        {
            Player_Ani.SetBool("Attack1", true);
        }
        

        yield return new WaitForSeconds(0.2857143f);
        Player_Ani.SetBool("Attack2", false);

        if (!isFristAttack)
            StartCoroutine("TrySeconAttack_co");

        else
        {
            Player_Ani.SetBool("Attack1", false);
            isAttacking = false;
            isFristAttack = true;
        }
    }

    public IEnumerator TrySeconAttack_co() // 두 번째 기본 공격
    {
        Player_Ani.SetBool("Attack2", true);
        yield return new WaitForSeconds(0.2857143f);
        Player_Ani.SetBool("Attack1", false);

        
        isAttacking = false;
        isFristAttack = true;
    }

    public void PlayerStop()
    {
        movement2D.MoveTo(Vector3.zero);
    }
}
