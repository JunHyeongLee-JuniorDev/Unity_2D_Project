using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    /*
     1. �÷��̾��� ���� ���� �ε����� �÷��̾��� �� & �� �ӵ� 0����
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
