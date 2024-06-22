using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     1. 카메라 컨트롤러
     2. 게임 시작 종료
     */

    public static GameManager instance = null;
    [SerializeField]private GameObject player;
    private PlayerControl playerControl;
    private PlayerBar playerBar;

    private bool isGameStart = true;
    private int round = 1;
    public int[] maxEnemyAmount; // 라운드별 총 몬스터 개수
    private EnemySpawner[] Spawner;

    public int PlayerKills = 0;
    public int PLayerAllKills = 0;

    public GameObject Player => player;
    private bool isPlayerDead = false;
    public int Round
    {
        get
        {
            return round;
        }

        set
        {
            round = value;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        Spawner = new EnemySpawner[System.Enum.GetValues(typeof(EnemyName)).Length];
        Spawner[(int)EnemyName.GOBLIN] = GameObject.Find("GoblinSpwaner").GetComponent<EnemySpawner>();
        Spawner[(int)EnemyName.FLYING_EYE] = GameObject.Find("eyeSpawner").GetComponent<EnemySpawner>();
        Spawner[(int)EnemyName.MUSHROOM] = GameObject.Find("MushroomSpawner").GetComponent<EnemySpawner>();
        Spawner[(int)EnemyName.SKELETON] = GameObject.Find("SkelSpawner").GetComponent<EnemySpawner>();
        }

        else
        {
            Debug.Log("The instance already exists!");
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        maxEnemyAmount = new int[6];

        playerControl = player.transform.GetComponent<PlayerControl>();
        playerBar = player.transform.GetComponent<PlayerBar>();

            for (int i=1;i < maxEnemyAmount.Length; i++)
        {
            maxEnemyAmount[i] = 0;
            maxEnemyAmount[i] += Spawner[(int)EnemyName.GOBLIN].poolByRound[i];
            maxEnemyAmount[i] += Spawner[(int)EnemyName.FLYING_EYE].poolByRound[i];
            maxEnemyAmount[i] += Spawner[(int)EnemyName.SKELETON].poolByRound[i];
            maxEnemyAmount[i] += Spawner[(int)EnemyName.MUSHROOM].poolByRound[i];
        }
    }

    void Update()
    {
        if(isGameStart)
        {
            StartRound();
        }
    }
    
    private void StartRound()
    {
        StartCoroutine("StartRound_co");
        isGameStart = false;
    }

    private IEnumerator StartRound_co()
    {
        WaitForSeconds SpawnTime = new WaitForSeconds(2f);
        int MaxSpawnPool;

        MaxSpawnPool = Spawner[0].poolByRound[round];
        for (int i=0;i<Spawner.Length;i++)
        {
            if(MaxSpawnPool < Spawner[i].poolByRound[round])
            {
                MaxSpawnPool = Spawner[i].poolByRound[round];
            }
        }

        for(int i=0; i < MaxSpawnPool; i++)
        {
            if(i < Spawner[(int)EnemyName.GOBLIN].poolByRound[round])
                Spawner[(int)EnemyName.GOBLIN].Spawning(i);

            if (i < Spawner[(int)EnemyName.FLYING_EYE].poolByRound[round])
                Spawner[(int)EnemyName.FLYING_EYE].Spawning(i);

            if (i < Spawner[(int)EnemyName.MUSHROOM].poolByRound[round])
                Spawner[(int)EnemyName.MUSHROOM].Spawning(i);

            if (i < Spawner[(int)EnemyName.SKELETON].poolByRound[round])
                Spawner[(int)EnemyName.SKELETON].Spawning(i);

            yield return SpawnTime;
        }
    }

    public IEnumerator RoundCleared_co()
    {
        playerControl.CurHP = playerControl.MaxHP;
        playerBar.CheckHPgage();

        Spawner[(int)EnemyName.GOBLIN].EndRound();
        Spawner[(int)EnemyName.FLYING_EYE].EndRound();
        Spawner[(int)EnemyName.MUSHROOM].EndRound();
        Spawner[(int)EnemyName.SKELETON].EndRound();

        yield return new WaitForSeconds(10f);

        round += 1;
        StartRound();
    }

    public void AddScore()
    {
        PlayerKills += 1;
        PLayerAllKills += 1;
        if (PlayerKills >= maxEnemyAmount[Round])
        {
            PlayerKills = 0;
            StartCoroutine(RoundCleared_co());
        }
    }

    public void GameOver()
    {
        if(isPlayerDead)
        {

        }
    }
}
