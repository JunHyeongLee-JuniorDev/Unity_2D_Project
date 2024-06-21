using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    /*
     * ī�޶� �ۿ��� ���� ��ǥ�� ���� �ٽ� �ڷ� ������
     * ������ x ��ŭ���� �ڷ� x*2��ŭ ������
     */
    SpriteRenderer spriteRenderer;
    float Width;
    Vector3 defultPos;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Width = spriteRenderer.bounds.size.x;
        defultPos = transform.position;
    }

    private void Update()
    {
        if(transform.position.x < defultPos.x - Width)
        {
            transform.position = defultPos;
        }
    }
}
