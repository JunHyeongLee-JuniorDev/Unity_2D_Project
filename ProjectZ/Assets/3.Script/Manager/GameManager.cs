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
