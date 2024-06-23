using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GageName
{
    AP = 0,
    ST = 1
};

public class PlayerControl : MonoBehaviour
{
    //UI ����
    //****************************************************
    private PlayerBar playerBar; // ü�¹�

    //****************************************************
    //�̵�����
    //****************************************************
    private Movement2D movement2D;
    private SpriteRenderer _renderer;
    private Animator ob_Ani;
    private PlayerMoveAni MoveAni;
    private PlayerSkill skill;
    private float x;
    private float y;
    public float X => x;
    //*****************************************************

    //�ɷ�ġ
    /*
     * ���ݷ�
     * ��
     * ����
     * �̵��ӵ�
     */
    //*****************************************************
    private float basicDamage = 5f; // ���ݷ�
    public float BasicDamage => basicDamage;
    private float NormalSpeed = 3f;
    public float _NormalSpeed => NormalSpeed;
    public readonly float MaxHP = 200f; // ü��
    public float CurHP = 200f;

    public readonly float MaxAp = 100f; // ����
    public float CurAp = 100f;

    public readonly float MaxSt = 100f; // ���
    public float CurSt = 100f;
    
    private int level;
    public int Level => level;

    public bool[] isGageCharge;
    //*****************************************************
    // �뽬
    //*****************************************************
    public readonly float DashSpeed = 15f;
    public float DashCount = 0.1f;
    private readonly float DashTime = 0.1f;
    GameObject[] Shadows;
    TraceShadow[] shadowMethods;
    //*****************************************************

    private void Awake()
    {
        _renderer = transform.GetComponent<SpriteRenderer>();
        ob_Ani = transform.GetComponent<Animator>();
        movement2D = transform.GetComponent<Movement2D>();
        MoveAni = transform.GetComponent<PlayerMoveAni>();
        playerBar = transform.GetComponent<PlayerBar>();
        skill = GetComponent<PlayerSkill>();
        Shadows = GameObject.FindGameObjectsWithTag("Shadow");
        shadowMethods = new TraceShadow[Shadows.Length];
        isGageCharge = new bool[2];
        isGageCharge[0] = false;
        isGageCharge[1] = false;

        for (int i = 0; i < 4; i++)
        {
            if (Shadows[i] == null)
            {
                Debug.Log("�׸��ڰ� �Ҵ� X");
                break;
            }
            shadowMethods[i] = Shadows[i].GetComponent<TraceShadow>();
        }
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
        if (!skill.isDashSlice && !GameManager.instance.isMenuOn)
        {
            if(Input.GetMouseButtonDown(0) && CurSt >= 10)
            TryAttack();
            PlayerMove();
        }

        chargecontrol();
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

        if (!IsAttack())
        {
            if (x.Equals(1))
            {
                _renderer.flipX = false;
            }

            else if (x.Equals(-1))
            {
                _renderer.flipX = true;
            }
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

    private void TryAttack() // �⺻ ���� 
    {
        CurSt -= 10f;
        ob_Ani.SetTrigger("Attack");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyHitBox"))
        {
            EnemyControl enemyControl = col.GetComponentInParent<EnemyControl>();

            if ((CurHP - enemyControl._Damage < 0))
                CurHP = 0;

            else
                CurHP -= enemyControl._Damage;

            ob_Ani.SetTrigger("Hit");

            playerBar.CheckHPgage();

            if (isPlayerDead())
            {
                GameManager.instance.GameOver();
            }
        }
    }

    private bool isPlayerDead()
    {
        if (CurHP <= 0)
            return true;

        else
        return false;
    }

    private float dashCost = 30f;

    private void TryDash() // �뽬 ���
    {
        if (CurSt >= 30f)
            if (Input.GetKeyUp(KeyCode.Space) && DashCount <= 0)
        {
            movement2D.Move_Speed = DashSpeed;
            DashCount = DashTime;

            CurSt -= dashCost;
                playerBar.CheckSTgage();

            isGageCharge[(int)GageName.ST] = true;

            for(int i=0;i<4;i++)
            {
                shadowMethods[i].OnShadow();
            }
        }


        if (DashCount > 0)
        {
            DashCount -= Time.deltaTime;
            if (DashCount <= 0)
            {
                movement2D.Move_Speed = NormalSpeed;
                for (int i = 0; i < 4; i++)
                {
                    shadowMethods[i].OffShadow();
                }
            }
        }
    }

    private void chargecontrol()
    {
        if (isGageCharge[(int)GageName.AP])
        {
            Charging(GageName.AP);
        }

        if (isGageCharge[(int)GageName.ST])
        {
            Charging(GageName.ST);
        }
    }

    private void Charging(GageName name)
    {
        float StchargePoint = 10f;
        float ApchargePoint = 15f;
        switch (name)
        {
            case (GageName.AP):
                CurAp += ApchargePoint * Time.deltaTime;

                playerBar.CheckAPgage();

                if (CurAp >= MaxAp)
                {
                    CurAp = MaxAp;
                    isGageCharge[(int)name] = false;
                }
                break;

            case (GageName.ST):
                CurSt += StchargePoint * Time.deltaTime;

                playerBar.CheckSTgage();

                if (CurSt >= MaxSt)
                {
                    CurSt = MaxSt;
                    isGageCharge[(int)name] = false;
                }
                break;

            default:
                break;
        }
    }
}
