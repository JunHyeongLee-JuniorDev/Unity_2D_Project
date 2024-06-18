using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    private List<GameObject> enemyList;

    private readonly int goblinCount = 50;
    private readonly int[] goblinRound = { 5, 10, 10, 15, 20, 30 };

    private readonly int eyeCount = 25;
    private readonly int[] eyeRound = { 5, 10, 10, 15, 15, 20 };

    private readonly int skeletonCount = 30;
    private readonly int[] skeletionRound = { 0, 0, 10, 15, 30, 30 };
    
    private readonly int mushroomCount = 20;
    private readonly int[] mushroomRound = { 0, 0, 0, 15, 20, 20 };

    private int poolCount;
    private int[] poolRound;
    private Vector3 poolPos = new Vector3(0, 200);

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
        WaitForSeconds wfs = new WaitForSeconds(1f);

        for(int i=0;i < poolRound[GameManager.instance.Round]; i++)
        {
            enemyList[i].SetActive(true);
            enemyList[i].transform.position = transform.position;
            yield return wfs;
        }
    }
    public void EndRound()
    {

    }
}
