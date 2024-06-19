using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyName
{
    GOBLIN = 0,
    FLYING_EYE = 1,
    MUSHROOM = 2,
    SKELETON = 3
};

public class EnemyControl : MonoBehaviour
{
    /*
     에너미가 갖고 있어야할 기본적인 것들
    
    체력
    공격력
    이동속도
     */

    private NavMeshAgent agent;
    private Animator Ani;
    [SerializeField] private GameObject Player;
    private SpriteRenderer sprite;
    [SerializeField] private float Damage;

    public float _Damage
    {
        get
        {
            return Damage;
        }

        set
        {
            Damage = value;
        }
    }
    [SerializeField] private float HP;

    public float _hp
    {
        get
        {
            return HP;
        }

        set
        {
            HP = value;
        }
    }

    public void takeDamage(float value)
    {
            HP -= value;
    }

    [SerializeField] private float Speed;

    public float _Speed
    {
        get
        {
            return Speed;
        }

        set
        {
            Speed = value;
        }
    }

    public bool isDead = false;
    public readonly float[] MaxHealtharr = {20f, 10f, 30f, 25f};
    public readonly float[] Damagearr = {3f, 2f, 5f, 4f };
    public readonly float[] Speedarr = {2f, 3f, 1f, 1f };

    private void Start()
    {
        Player = GameManager.instance.Player;
        sprite = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (isDead)
                return;

            if (!isdie())
            {
                agent.SetDestination(Player.transform.position);
                EnemyMove();
                StopByHit();
            }
        }
    }

    private void StopByHit()
    {
        if (Ani.GetCurrentAnimatorStateInfo(0).IsTag("Hit"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        if (!Ani.GetCurrentAnimatorStateInfo(0).IsTag("Hit"))
        {
            agent.isStopped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerBasicAttack") && !isDead)
        {
            Ani.SetTrigger("Hit");
        }

    }

    private bool isdie()
    {
        if(HP <= 0)
        {
            Ani.SetBool("Run", false);
            Ani.SetTrigger("die");
            isDead = true;
            return true;
        }

        return false;
    }

    private void EnemyMove()
    {
        if(agent.velocity.Equals(Vector3.zero) && !gameObject.name.Equals("Flying_eye(Clone)"))
        {
            Ani.SetBool("Run", false);
        }

        else
        {
            Ani.SetBool("Run",true);
        }

        if (agent.velocity.x < 0)
        {
            sprite.flipX = true;
        }

        else if (agent.velocity.x > 0)
        {
            sprite.flipX = false;
        }

    }
}
