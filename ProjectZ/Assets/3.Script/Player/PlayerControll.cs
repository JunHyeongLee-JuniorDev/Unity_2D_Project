using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Movement2D movement2D;
    private Animator Player_Ani;
    private AudioSource Player_Audio;
    private SpriteRenderer Player_Renderer;
    /*
     플레이어 이동 먼저 구현하자

    키보드 눌리면 그 방향으로 조금씩 움직이게 끔
    */

    // Update is called once per frame

    private void Awake()
    {
        movement2D = transform.GetComponent<Movement2D>();
        Player_Ani = transform.GetComponent<Animator>();
        Player_Audio = transform.GetComponent<AudioSource>();
        Player_Renderer = transform.GetComponent<SpriteRenderer>();
    }

    private void Start()
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

        PlayerMoveAni(x, y);

        if (Input.GetMouseButtonDown(0))
        {
            Player_Ani.SetBool("Attack", true);
        }

        Player_Ani.SetBool("Attack", false);

        movement2D.MoveTo(new Vector3(x, y, 0));
    }



    public void PlayerMoveAni(float x, float y)
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


}
