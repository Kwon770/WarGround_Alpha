using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCamera : MonoBehaviour {

    [SerializeField] float CameraSize;    //줌 인, 줌 아웃
    [SerializeField] float CameraSizeMin;
    [SerializeField] float CameraSizeMax;
    [SerializeField] float ZoomSpeed;
    [SerializeField] float ScreenMouseSpeed;
    [SerializeField] float ScreenDragSpeed;
    Vector3 prevPos = Vector3.zero;

    public Camera MainCam;

    public bool CameraStop = false;
	
	void Update () {

        MainCam.orthographicSize = CameraSize;

       

        if (Input.GetKeyDown(KeyCode.Space)) // 카메라 잠금, 잠금해제 (스페이스바)
        {
            CameraStopSwitch();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // 줌 아웃
        {
            CameraSize = CameraSize + ZoomSpeed * Time.deltaTime;
            CameraZoomLock();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)     // 줌 인
        {
            CameraSize = CameraSize - ZoomSpeed * Time.deltaTime;
            CameraZoomLock();
        }

        if (Input.GetMouseButtonDown(1))  prevPos = Input.mousePosition;
        if (Input.GetMouseButton(1))    // 오른쪽 마우스 드래그로 이동
        {
            Vector3 dir = (Input.mousePosition - prevPos);
            
            transform.position -= transform.right * dir.x * ScreenDragSpeed * Time.deltaTime * CameraSize;
            transform.position -= transform.forward * dir.y * ScreenDragSpeed * Time.deltaTime * CameraSize;
            prevPos = Input.mousePosition;
        }

        else if (Input.mousePosition.x <= 0)
        {
            transform.position -= transform.right * Time.deltaTime * ScreenMouseSpeed * CameraSize;// new Vector3(transform.position.x - ScreenMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (Input.mousePosition.x >= Screen.width)
        {
            transform.position += transform.right * Time.deltaTime * ScreenMouseSpeed * CameraSize;// new Vector3(transform.position.x + ScreenMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (Input.mousePosition.y <= 0)
        {
            transform.position -= transform.forward * Time.deltaTime * ScreenMouseSpeed * CameraSize;//new Vector3(transform.position.x, transform.position.y, transform.position.z - ScreenMoveSpeed * Time.deltaTime);
        }
        else if (Input.mousePosition.y >= Screen.height)
        {
            transform.position += transform.forward * Time.deltaTime * ScreenMouseSpeed * CameraSize;// new Vector3(transform.position.x, transform.position.y, transform.position.z + ScreenMoveSpeed * Time.deltaTime);
        }



    }






    void CameraStopSwitch()
    {
        if (CameraStop == false)
        {
            CameraStop = true;
        }
        else
        {
            CameraStop = false;
        }

    }
    void CameraZoomLock()
    {
        if(CameraSize >= CameraSizeMax)
        {
            CameraSize = CameraSizeMax;
        }
        if (CameraSize <= CameraSizeMin)
        {
            CameraSize = CameraSizeMin;
        }
    }
}


  