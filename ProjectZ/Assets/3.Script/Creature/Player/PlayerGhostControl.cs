using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostControl : MonoBehaviour
{
    public GameObject ghost;
    private PlayerControl Player;

    private readonly Vector3 poolPosition = new Vector3(0, 200, 0);

    private void Awake()
    {
        Player = GetComponent<PlayerControl>();
    }

    public void TryDash()
    {
        GameObject currentGhost;

            currentGhost = Instantiate(ghost, transform.position, Quaternion.identity);
            currentGhost.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();

            if (Player.X.Equals(-1))
                ghostSprite.flipX = true;

            else
                ghostSprite.flipX = false;

        Destroy(currentGhost, 0.1f);
        
        
    }
}
