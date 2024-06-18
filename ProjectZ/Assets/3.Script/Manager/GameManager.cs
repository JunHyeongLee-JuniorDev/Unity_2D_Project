using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     1. 카메라 컨트롤러
     2. 게임 시작 종료
     3. 사운드 메니저
     */

    public static GameManager instance = null;
    [SerializeField]private GameObject player;
    public GameObject Player => player;

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
    }

    private int round = 1;
    public bool _isRoundClear = true; // 디버깅
    public bool isRoundClear => _isRoundClear;
    public int Round => round;

    void Update()
    {
            
    }
}
