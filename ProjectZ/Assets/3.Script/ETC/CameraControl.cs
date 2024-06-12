using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    /*
     카메라 컨트롤러
        주요기능
        1. 플레이어를 따라다닌다.
        2. 미니맵에 있는 곳을 클릭시 그 곳의 화면을 보여줘야한다.
     */
    /*
     평소에는 플레이어 고정
     */

    public void CameraView(Vector3 position)
    {
        transform.position = position;
    }
}
