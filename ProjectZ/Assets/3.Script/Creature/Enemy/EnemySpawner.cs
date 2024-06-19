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
    private EnemyName enemyName;

    private int poolCount;
    private int[] poolRound;
    private Vector3 poolPos = new Vector3(-80f, -0.82f, 0);

    private void Awake()
    {
        EnemyControl enemyScript;

        enemyList = new List<GameObject>();
        switch(Enemy.name)
        {
            case "Goblin":
                enemyName = EnemyName.GOBLIN;
                poolCount = goblinCount;
                poolRound = goblinRound;
                break;

            case "Flying_eye":
                enemyName = EnemyName.FLYING_EYE;
                poolCount = eyeCount;
                poolRound = eyeRound;
                break;

            case "Skeleton":
                enemyName = EnemyName.SKELETON;
                poolCount = skeletonCount;
                poolRound = skeletionRound;
                break;

            case "Mushroom":
                enemyName = EnemyName.MUSHROOM;
                poolCount = mushroomCount;
                poolRound = mushroomRound;
                break;
        }
        GameObject oneEnemy;

        for(int i=0;i < poolCount;i++)
        {
            oneEnemy = Instantiate(Enemy, poolPos, Quaternion.identity);
            enemyScript = oneEnemy.GetComponent<EnemyControl>();
            enemyScript._hp = enemyScript.MaxHealtharr[(int)enemyName];
            enemyScript._Damage = enemyScript.Damagearr[(int)enemyName];
            enemyScript._Speed = enemyScript.Speedarr[(int)enemyName];
            oneEnemy.SetActive(false);
            enemyList.Add(oneEnemy);
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
        for (int i = 0; i < poolRound[GameManager.instance.Round]; i++)
        {
            enemyList[i].transform.position = poolPos;
            enemyList[i].SetActive(false);
        }
    }

    public bool IsCleared()
    {
        int aliveCounter= 0;
        EnemyControl CurEnemy;

        for (int i = 0; i < poolRound[GameManager.instance.Round]; i++)
        {
            CurEnemy = enemyList[i].GetComponent<EnemyControl>();
            if (CurEnemy.isDead)
                aliveCounter++;
        }

        if (aliveCounter.Equals(poolRound[GameManager.instance.Round]))
            return true;

        else
            return false;
    }
}
