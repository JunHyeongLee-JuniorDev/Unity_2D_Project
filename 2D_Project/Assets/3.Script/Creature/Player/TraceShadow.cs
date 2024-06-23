using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceShadow : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float offset;

    SpriteRenderer PlayerSprite;
    SpriteRenderer sprite;

    Animator Ani;
    Animator PlayerAni;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        PlayerSprite = target.GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
        PlayerAni = target.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, offset);
        Ani.Play(PlayerAni.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, PlayerAni.GetCurrentAnimatorStateInfo(0).normalizedTime);
        sprite.flipX = PlayerSprite.flipX;
    }

    public void OnShadow()
    {
        sprite.enabled = true;
    }

    public void OffShadow()
    {
        sprite.enabled = false;
    }
}
