using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    private SpriteRenderer parent_sp; 
    private bool prevState = false; // ���� ���� ����
    private Vector3 Position; // ��Ʈ �ڽ� ��ġ
    Vector3 currentPos; // ��Ʈ �ڽ� ���� ��ġ

    void Start()
    {
        parent_sp = GetComponentInParent<SpriteRenderer>();
        Position = transform.localPosition;
        currentPos = Position;
        currentPos.x = Position.x * (-1f);
    }
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (!prevState.Equals(parent_sp.flipX) && parent_sp.flipX)
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0);
            transform.localPosition = currentPos;
        }

        else if (!prevState.Equals(parent_sp.flipX) && !parent_sp.flipX)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = Position;
        }
        prevState = parent_sp.flipX;
    }
}
