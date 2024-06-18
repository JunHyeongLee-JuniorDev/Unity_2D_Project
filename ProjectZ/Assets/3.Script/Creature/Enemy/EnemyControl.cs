using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator Ani;
    [SerializeField] private GameObject Player;
    private Movement2D movement2D;

    private void Start()
    {
        Player = GameManager.instance.Player;
        movement2D = GetComponent<Movement2D>();
        Ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            agent.SetDestination(Player.transform.position);
            EnemyAni();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) // 플레이어 공격감지
    {
        if (col.CompareTag("PlayerAttack"))
        {

        }
    }

    private void EnemyAni()
    {
        if (agent.velocity == Vector3.zero)
        {
            Ani.SetTrigger("Idle");
        }

        else
        {
            Ani.SetTrigger("Run");
        }

    }
}
