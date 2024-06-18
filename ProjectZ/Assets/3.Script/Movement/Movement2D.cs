using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float Move_Speed = 0f;
    [SerializeField] private Vector2 moveDirection = Vector3.zero;

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * Move_Speed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}

