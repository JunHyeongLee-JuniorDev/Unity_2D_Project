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
    PlayerControl PlayerControl;
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
    [SerializeField] public float MaxHP;

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
    public readonly float[] MaxHealtharr = {15f, 10f, 30f, 25f};
    public readonly float[] Damagearr = {5f, 3f, 15f, 8f };
    public readonly float[] Speedarr = {1.5f, 3f, 1f, 1f };

    private void Start()
    {
        Player = GameManager.instance.Player;
        sprite = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        PlayerControl = Player.GetComponentInParent<PlayerControl>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void FixedUpdate()
    {
        if (!Dead && !isAttacking && !isStopByHit)
        {
            agent.SetDestination(Player.transform.position);
            EnemyMoveAni();
        }
        else
        {
            agent.velocity = Vector3.zero;
        }
    }

    public bool isStopByHit = false;
    public IEnumerator StopByHit_co()
    {
        if (Dead)
            yield break;

        isStopByHit = true;
        agent.velocity = Vector3.zero;
        Ani.SetTrigger("Hit");

        yield return new WaitForSeconds(Ani.GetCurrentAnimatorClipInfo(0).Length);

        isStopByHit = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerBasicAttack") && !isdie())
        {
            if (!Dead)
            {
                _hp = HP - PlayerControl.BasicDamage;
                StartCoroutine(StopByHit_co());
            }
            if (isdie())
            {
                GameManager.instance.AddScore();
            }
        }
    }

    public bool Dead = false;

    public bool isdie()
    {
            if (HP <= 0)
            {
            if (Dead)
            {
                return true;
            }
                Ani.SetBool("Run", false);
                Ani.SetTrigger("die");
                agent.enabled = false;
                Dead = true;
                return true;
            }

            return false;
    }

    private void EnemyMoveAni()
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

    public bool isAttacking = false;

    public IEnumerator TryAttack_co()
    {
        isAttacking = true;
        Ani.SetTrigger("Attack");
        yield return new WaitForSeconds(Ani.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;

        agent.velocity = Vector3.zero;
    }
}
