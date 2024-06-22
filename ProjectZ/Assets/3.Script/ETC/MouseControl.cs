using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    Camera mainCamera;
    public Image mousePointerImage;
    public Vector3 mousePosition;
    Vector3 clampedMousePosition;
    
    public Vector3 refMousePos;
    
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }
    
    void Update()
    {
        mousePosition = Input.mousePosition;
    
        clampedMousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        clampedMousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
        mousePosition = clampedMousePosition;
    
        mousePointerImage.rectTransform.position = mousePosition;
        refMousePos = mainCamera.ScreenToWorldPoint(mousePosition);
        refMousePos.z = 0;
    }
}
