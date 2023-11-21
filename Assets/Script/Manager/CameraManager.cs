using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float clickPoint = 0;
    private bool isMoveCamera = false;

    [Header("# Camera Var")]
    [SerializeField] private float dragSpeed = 0.0f;
    // # Mathf.Clamp Var
    [SerializeField] private float minLimit = 0.0f;
    [SerializeField] private float maxLimit = 0.0f;

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        // 특정 키를 눌렀을 떄 카메라 이동이 가능함
        if (Input.GetKeyDown(KeyCode.Space)) isMoveCamera = true;
        else if (Input.GetKeyUp(KeyCode.Space)) isMoveCamera = false;

        // 마우스 위치기억
        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition.x;

        if (isMoveCamera == true)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 postion = Camera.main.ScreenToViewportPoint(-new Vector2(Input.mousePosition.x - clickPoint, 0));
                Vector2 move = postion * (Time.deltaTime * dragSpeed);

                Camera.main.transform.Translate(move);

                // 카메라 범위 제한 
                Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, minLimit, maxLimit),
                Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
        }
    }
}