using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAni : MonoBehaviour
{
    private Animator ob_Ani;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        ob_Ani = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void MoveMotion(float x, float y)
    {
        ob_Ani.SetBool("Run", false);
        ob_Ani.SetBool("Idle", false);

        if (!x.Equals(0f) || !y.Equals(0f))
        {
                ob_Ani.SetBool("Run", true);
        }

        else
        {
            ob_Ani.SetBool("Idle", true);
        }
    }
}
