using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    /*
     사거리 객체를 만들고
     만든객체에 콜라이더 씌운뒤 그 범위에 플레이어 들어오면
    부모 에너미 객체의 attack 애니메이션 발동
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
