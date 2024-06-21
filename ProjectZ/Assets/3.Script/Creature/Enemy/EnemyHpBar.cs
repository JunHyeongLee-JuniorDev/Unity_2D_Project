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
    }

    private void Update()
    {
        slider.value = enemy._hp / enemy.MaxHP;
    }

    public void checkHp()
    {
        Debug.Log(slider.value);
        slider.value = enemy._hp / enemy.MaxHP;
    }
}
