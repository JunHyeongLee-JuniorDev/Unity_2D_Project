using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     1. ī�޶� ��Ʈ�ѷ�
     2. ���� ���� ����
     3. ���� �޴���
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
    public bool _isRoundClear = true; // �����
    public bool isRoundClear => _isRoundClear;
    public int Round => round;

    void Update()
    {
            
    }
}
