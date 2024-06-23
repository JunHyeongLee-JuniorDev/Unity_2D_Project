using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    /*
     ��Ÿ� ��ü�� �����
     ���簴ü�� �ݶ��̴� ����� �� ������ �÷��̾� ������
    �θ� ���ʹ� ��ü�� attack �ִϸ��̼� �ߵ�
     */
    EnemyControl enemy;
    private readonly float defualtCoolTime = 2f;
    public float NextCoolTime = 0f;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyControl>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!enemy.isAttacking)
            {
                if (Time.time >= NextCoolTime)
                {
                    NextCoolTime = Time.time + defualtCoolTime;
                    StartCoroutine(enemy.TryAttack_co());
                }
            }
        }
    }
}
