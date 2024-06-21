using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    public List<GameObject> enemyList;

    [SerializeField] private GameObject Player;

    private readonly int goblinCount = 20;
    private readonly int[] goblinRound = { 0, 3, 10, 15, 20, 20 };

    private readonly int eyeCount = 20;
    private readonly int[] eyeRound = { 0, 3, 10, 15, 20, 20 };

    private readonly int skeletonCount = 20;
    private readonly int[] skeletionRound = { 0, 3, 5, 15, 20, 20 };
    
    private readonly int mushroomCount = 20;
    private readonly int[] mushroomRound = { 0, 3, 0, 0, 10, 20 };
    private EnemyName enemyName;

    private int poolCount;
    public int[] poolByRound;
    private Vector3 poolPos = new Vector3(-80f, -0.82f, 0);

    private void Awake()
    {
        enemyList = new List<GameObject>();
        switch(Enemy.name)
        {
            case "Goblin":
                enemyName = EnemyName.GOBLIN;
                poolCount = goblinCount;
                poolByRound = goblinRound;
                break;

            case "Flying_eye":
                enemyName = EnemyName.FLYING_EYE;
                poolCount = eyeCount;
                poolByRound = eyeRound;
                break;

            case "Skeleton":
                enemyName = EnemyName.SKELETON;
                poolCount = skeletonCount;
                poolByRound = skeletionRound;
                break;

            case "Mushroom":
                enemyName = EnemyName.MUSHROOM;
                poolCount = mushroomCount;
                poolByRound = mushroomRound;
                break;
        }
    }

    private void Start()
    {
        EnemyControl enemyScript;
        GameObject oneEnemy;
        NavMeshAgent agent;

        for (int i = 0; i < poolByRound[GameManager.instance.Round]; i++)
        {
            oneEnemy = Instantiate(Enemy, poolPos, Quaternion.identity);
            enemyScript = oneEnemy.GetComponent<EnemyControl>();
            agent = oneEnemy.GetComponent<NavMeshAgent>();
            enemyScript._hp = enemyScript.MaxHealtharr[(int)enemyName];
            enemyScript.MaxHP = enemyScript.MaxHealtharr[(int)enemyName];
            enemyScript._Damage = enemyScript.Damagearr[(int)enemyName];
            agent.speed = enemyScript.Speedarr[(int)enemyName];
            oneEnemy.SetActive(false);
            enemyList.Add(oneEnemy);
        }
    }

    public void Spawning(int i)
    {
        NavMeshAgent agent;
        EnemyControl enemyControl;
        GameObject oneEnemy;


        if (i >= enemyList.Count)
        {
            oneEnemy = Instantiate(Enemy, poolPos, Quaternion.identity);
            enemyControl = oneEnemy.GetComponent<EnemyControl>();
            agent = oneEnemy.GetComponent<NavMeshAgent>();
            enemyControl._hp = enemyControl.MaxHealtharr[(int)enemyName];
            enemyControl.MaxHP = enemyControl.MaxHealtharr[(int)enemyName];
            enemyControl._Damage = enemyControl.Damagearr[(int)enemyName];
            agent.speed = enemyControl.Speedarr[(int)enemyName];
            oneEnemy.SetActive(false);
            enemyList.Add(oneEnemy);
        }

        enemyList[i].transform.position = gameObject.transform.position;
        enemyList[i].SetActive(true);
        agent = enemyList[i].GetComponent<NavMeshAgent>();
        enemyControl = enemyList[i].GetComponent<EnemyControl>();
        agent.enabled = true;
        enemyControl.Dead = false;
        Spawn_Enemy_HPbar(enemyControl);
    }

    public void EndRound()
    {
        for (int i = 0; i < poolByRound[GameManager.instance.Round]; i++)
        {
            enemyList[i].transform.position = poolPos;
            enemyList[i].SetActive(false);
        }
    }

    [SerializeField] private GameObject Enemy_HpBar;
    [SerializeField] private GameObject Canvas;

    private void Spawn_Enemy_HPbar(EnemyControl enemy)
    {
        GameObject SliderClone = Instantiate(Enemy_HpBar);
        
        SliderClone.transform.SetParent(Canvas.transform);
        
        SliderClone.transform.localScale = Vector3.one;
        
        
        SliderClone.GetComponent<EnemyHpPosition>().Setup(enemy.gameObject);
        SliderClone.GetComponent<EnemyHpBar>().SetBar(enemy);
    }
}
