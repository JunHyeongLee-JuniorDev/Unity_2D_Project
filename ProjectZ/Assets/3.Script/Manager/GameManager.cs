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


    private int round = 1;
    private EnemySpawner[] Spawner;

    private int PlayerKills = 0;

    public bool _isRoundClear = true;
    public GameObject Player => player;
    public int Round => round;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Debug.Log("The instance already exists!");
            Destroy(gameObject);
        }
        Spawner = new EnemySpawner[System.Enum.GetValues(typeof(EnemyName)).Length];
    }
    
    private void Start()
    {
        Spawner[(int)EnemyName.GOBLIN] = GameObject.Find("GoblinSpwaner").GetComponent<EnemySpawner>();
        Spawner[(int)EnemyName.FLYING_EYE] = GameObject.Find("eyeSpawner").GetComponent<EnemySpawner>();
        Spawner[(int)EnemyName.MUSHROOM] = GameObject.Find("MushroomSpawner").GetComponent<EnemySpawner>();
        Spawner[(int)EnemyName.SKELETON] = GameObject.Find("SkelSpawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        StartRound();
        RoundCleared();
    }
    
    private void StartRound()
    {
        if(_isRoundClear)
        {
            StartCoroutine(Spawner[(int)EnemyName.GOBLIN].StartRound_co());
            StartCoroutine(Spawner[(int)EnemyName.FLYING_EYE].StartRound_co());
            StartCoroutine(Spawner[(int)EnemyName.MUSHROOM].StartRound_co());
            StartCoroutine(Spawner[(int)EnemyName.SKELETON].StartRound_co());
            _isRoundClear = false;
        }
    }

    private void RoundCleared()
    {
        if (Spawner[(int)EnemyName.GOBLIN].IsCleared() &&
           Spawner[(int)EnemyName.FLYING_EYE].IsCleared() &&
           Spawner[(int)EnemyName.MUSHROOM].IsCleared() &&
           Spawner[(int)EnemyName.SKELETON].IsCleared()
            )
        {
            Spawner[(int)EnemyName.GOBLIN].EndRound();
            Spawner[(int)EnemyName.FLYING_EYE].EndRound();
            Spawner[(int)EnemyName.MUSHROOM].EndRound();
            Spawner[(int)EnemyName.SKELETON].EndRound();
            _isRoundClear = true;
        }

        else
            _isRoundClear = false;
    }
}
