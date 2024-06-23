using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    /*
     1. ī�޶� ��Ʈ�ѷ�
     2. ���� ���� ����
     */

    public static GameManager instance = null;
    [SerializeField]private GameObject player;
    private PlayerControl playerControl;
    private PlayerBar playerBar;
    [SerializeField] private Text remainText;
    [SerializeField] private Text roundText;
    [SerializeField] private Text Kill;
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject Gameover;

    private bool isGameStart = true;
    private int round = 1;
    public int[] maxEnemyAmount; // ���庰 �� ���� ����
    private EnemySpawner[] Spawner;

    public int PlayerKills = 0;
    public int PLayerAllKills = 0;

    public GameObject Player => player;
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
        maxEnemyAmount = new int[4];

        playerControl = player.transform.GetComponent<PlayerControl>();
        playerBar = player.transform.GetComponent<PlayerBar>();
        Resume();
            for (int i=1;i < maxEnemyAmount.Length; i++)
        {
            maxEnemyAmount[i] = 0;
            maxEnemyAmount[i] += Spawner[(int)EnemyName.GOBLIN].poolByRound[i];
            maxEnemyAmount[i] += Spawner[(int)EnemyName.FLYING_EYE].poolByRound[i];
            maxEnemyAmount[i] += Spawner[(int)EnemyName.SKELETON].poolByRound[i];
            maxEnemyAmount[i] += Spawner[(int)EnemyName.MUSHROOM].poolByRound[i];
        }
    }

    public bool isMenuOn = false;

    void Update()
    {
        if(isGameStart)
        {
            StartRound();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOn)
            {
                PauseMethod();
            }
        
            else if(isMenuOn)
            {
                Resume();
            }
        }

        if(isRoundCleared)
        {
            CountDown();
        }
    }

    public void PauseMethod()
    {
        Time.timeScale = 0;
        Pause.SetActive(true);
        isMenuOn = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);
        isMenuOn = false;
    }
    private void StartRound()
    {
        roundText.text = "���� ���� :" + round;
        remainText.text = "���� ���� �� :" + (maxEnemyAmount[round] - PlayerKills) + '/' + maxEnemyAmount[round];
        StartCoroutine("StartRound_co");
        isGameStart = false;
    }

    private IEnumerator StartRound_co()
    {
        WaitForSeconds SpawnTime = new WaitForSeconds(1f);
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
    [SerializeField]private Text countText;
    [SerializeField]private GameObject countOb;
    private int maxRound = 3;

    private bool isRoundCleared = false;
    public IEnumerator RoundCleared_co()
    {
        isRoundCleared = true;
        playerControl.CurHP = playerControl.MaxHP;
        playerBar.CheckHPgage();
        endTime = Time.time + 10f;
        if (round+1 > maxRound)
            Victory();

        Spawner[(int)EnemyName.GOBLIN].EndRound();
        Spawner[(int)EnemyName.FLYING_EYE].EndRound();
        Spawner[(int)EnemyName.MUSHROOM].EndRound();
        Spawner[(int)EnemyName.SKELETON].EndRound();
        countOb.SetActive(true);

        yield return new WaitForSeconds(10f);
        countOb.SetActive(false);
        round += 1;

        isRoundCleared = false;
        StartRound();
    }
    float endTime;

    public void CountDown()
    {
        if(Time.time <= endTime)
        {
            countText.text = "�غ��ϼ���! ���� �ð� : " + (int)(endTime - Time.time);
        }
    }

    public void AddScore()
    {
        PlayerKills += 1;
        PLayerAllKills += 1;
        if (PlayerKills >= maxEnemyAmount[Round])
        {
            PlayerKills = 0;
            StartCoroutine(RoundCleared_co());

            if(round > 3)
            {

            }
        }

        remainText.text = "���� ���� �� :" + (maxEnemyAmount[round] - PlayerKills) + '/' + maxEnemyAmount[round];
        Kill.text = "Kill : " + PLayerAllKills;
    }

    [SerializeField]Text finalKill;
    [SerializeField]Text finalRound;
    [SerializeField] Text vicLose;

    public void GameOver()
    {
            PauseMethod();
            Gameover.SetActive(true);
            vicLose.text = "�й��Ͽ����ϴ�!";
            finalRound.text = "���� ���� : " + round;
            finalKill.text = "�� óġ : " + PLayerAllKills;
    }

    public void Victory()
    {
        PauseMethod();
        Gameover.SetActive(true);
        vicLose.text = "�¸��Ͽ����ϴ�!";
        finalRound.text = "���� ���� : " + round;
        finalKill.text = "�� óġ : " + PLayerAllKills;
    }
    
}
