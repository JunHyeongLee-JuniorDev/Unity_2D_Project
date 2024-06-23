using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    /*
     * 카메라 밖에서 일정 좌표로 가면 다시 뒤로 보내기
     * 사이즈 x 만큼가면 뒤로 x*2만큼 보내기
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
