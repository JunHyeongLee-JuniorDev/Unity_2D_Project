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

    private void Awake()
    {
        if(instance = null)
        {
            instance = this;
        }

        else
        {
            Debug.Log("The instance already exists!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
