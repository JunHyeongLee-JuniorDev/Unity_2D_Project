using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DForBack : MonoBehaviour
{
    
    [SerializeField]public float Move_Speed = 0f;
    private Vector3 moveDirection = Vector3.left;

    void Update()
    {
        transform.position += moveDirection * Move_Speed * Time.deltaTime;
    }
}
