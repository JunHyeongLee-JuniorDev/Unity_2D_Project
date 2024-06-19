using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : WeaponControl
{
    PlayerControl Player;
    EnemyControl enemy;

    private void Awake()
    {
        Player = GetComponentInParent<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            enemy = col.GetComponent<EnemyControl>();
            if (enemy != null && !enemy.isDead)
            {
                enemy.takeDamage(Player.BasicDamage);
            }
        }
    }
}
