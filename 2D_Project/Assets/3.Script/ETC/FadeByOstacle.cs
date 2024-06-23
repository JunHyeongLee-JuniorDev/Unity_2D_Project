using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeByOstacle : MonoBehaviour
{
    SpriteRenderer Sprite;
    private float aValue = 0.5f;
    Color color;
    Color temp;

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        color = Sprite.color;
        temp = Sprite.color;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            temp.a = aValue;
            Sprite.color = temp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Sprite.color = color;
        }
    }
}
