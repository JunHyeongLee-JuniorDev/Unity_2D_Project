using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    EnemyControl enemy;
    Slider slider;

    public void SetBar(EnemyControl enemy)
    {
        this.enemy = enemy;
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = enemy.MaxHP;
        slider.value = enemy.MaxHP;
    }

    private void FixedUpdate()
    {
        slider.value = enemy._hp;
    }
}
