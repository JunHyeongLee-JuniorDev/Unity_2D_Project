using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    /*
     1. 플레이어의 발이 벽에 부딪히면 플레이어의 발 & 몸 속도 0으로
     */

    private Movement2D player;
    private void Awake()
    {
        player = GetComponentInParent<Movement2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
