using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponControl : WeaponControl
{
    EnemyControl enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyControl>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
