using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    /*
     ī�޶� ��Ʈ�ѷ�
        �ֿ���
        1. �÷��̾ ����ٴѴ�.
        2. �̴ϸʿ� �ִ� ���� Ŭ���� �� ���� ȭ���� ��������Ѵ�.
     */
    /*
     ��ҿ��� �÷��̾� ����
     */

    public void CameraView(Vector3 position)
    {
        transform.position = position;
    }
}
