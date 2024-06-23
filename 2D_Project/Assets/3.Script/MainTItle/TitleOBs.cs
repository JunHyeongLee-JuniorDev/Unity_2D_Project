using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOBs : MonoBehaviour
{
    Animator Ani;
    void Start()
    {
        Ani = GetComponent<Animator>();
        Ani.SetBool("Run", true);
    }
}
