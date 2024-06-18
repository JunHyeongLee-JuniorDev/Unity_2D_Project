using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    private List<GameObject> enemyList;

    private readonly int goblinCount = 20;
    private readonly int[] goblinRound = { 0, 5, 10, 15, 20, 20 };

    private readonly int eyeCount = 20;
    private readonly int[] eyeRound = { 0, 5, 10, 15, 15, 20 };

    private readonly int skeletonCount = 20;
    private readonly int[] skeletionRound = { 0, 5, 10, 15, 20, 20 };
    
    private readonly int mushroomCount = 20;
    private readonly int[] mushroomRound = { 0, 5, 5, 15, 20, 20 };

    private int poolCount;
    private int[] poolRound;
    private Vector3 poolPos = new Vector3(-80f, -0.82f, 0);

    private void Awake()
    {
        enemyList = new List<GameObject>();
        switch(Enemy.name)
        {
            case "Goblin":
                poolCount = goblinCount;
                poolRound = goblinRound;
                break;

            case "Flying_eye":
                poolCount = eyeCount;
                poolRound = eyeRound;
                break;

            case "Skeleton":
                poolCount = skeletonCount;
                poolRound = skeletionRound;
                break;

            case "Mushroom":
                poolCount = mushroomCount;
                poolRound = mushroomRound;
                break;
        }
        GameObject oneEnemy;

        for(int i=0;i < poolCount;i++)
        {
            oneEnemy = Instantiate(Enemy, poolPos, Quaternion.identity);
            oneEnemy.SetActive(false);
            enemyList.Add(oneEnemy);
        }
    }

    void Update()
    {
        if (GameManager.instance._isRoundClear)
        {
            StartCoroutine("StartRound_co");
            GameManager.instance._isRoundClear = false;
        }
    }

    public IEnumerator StartRound_co()
    {
        WaitForSeconds SpawnTime = new WaitForSeconds(2f);

        for(int i=0;i < poolRound[GameManager.instance.Round]; i++)
        {
            enemyList[i].transform.position = gameObject.transform.position;
            enemyList[i].SetActive(true);
            yield return SpawnTime;
        }
    }
    public void EndRound()
    {

    }
}
