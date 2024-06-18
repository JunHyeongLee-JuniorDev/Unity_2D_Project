using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostControl : MonoBehaviour
{
    public GameObject ghost;
    private PlayerMoveControl Player;
    private Movement2D movement2D;
    public bool makeGhost = false;

    private readonly int poolCount = 15;
    private readonly Vector3 poolPosition = new Vector3(0, 200, 0);
    private Queue<GameObject> ghost_queue;

    private void Awake()
    {
        GameObject currentGhost;
        Player = GetComponent<PlayerMoveControl>();
        ghost_queue = new Queue<GameObject>();
        movement2D = GetComponent<Movement2D>();

        for (int i=0;i < poolCount; i++)
        {
            currentGhost = Instantiate(ghost, poolPosition, transform.rotation);
            currentGhost.SetActive(false);
            ghost_queue.Enqueue(currentGhost);

        }
    }

    public IEnumerator TryDash_co()
    {
        GameObject currentGhost;
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

            currentGhost = ghost_queue.Dequeue();

            currentGhost.transform.position = transform.position;
            currentGhost.SetActive(true);
            currentGhost.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            SpriteRenderer ghostSprite = currentGhost.GetComponent<SpriteRenderer>();

            if (Player.X.Equals(-1))
                ghostSprite.flipX = true;

            else
                ghostSprite.flipX = false;

            yield return wfs;

            ghost_queue.Enqueue(currentGhost);
            currentGhost.SetActive(false);
            currentGhost.transform.position = poolPosition;
        
        
    }
}
