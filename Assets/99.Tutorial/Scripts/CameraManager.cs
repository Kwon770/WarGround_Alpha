using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField] Camera cam;
    
    [SerializeField] TutorialManager tutoM;
    [SerializeField] public bool opening;
    [SerializeField] GameObject ship;
    [SerializeField] Vector3 shipAndCamDistance; // 카메라와 배 간 거리 저장

    [SerializeField] bool openingStart = false;
    
    private void Start()
    {
        
        opening = true;
        shipAndCamDistance = new Vector3(transform.position.x - ship.transform.position.x, transform.position.y - ship.transform.position.y, transform.position.z - ship.transform.position.z);
        transform.position = new Vector3(transform.position.x + ship.transform.position.x, transform.position.y, transform.position.z + ship.transform.position.z);

    }

    private void Update()
    {
        if (opening == true)
        {
            
            transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, ship.transform.position.z) + shipAndCamDistance;
        }
         else
        {
            if(openingStart == false)
            {
                cam.orthographic = true;
                cam.orthographicSize = 7f;
                

                transform.rotation = Quaternion.Euler(45f, 30f, 0f);
                cam.gameObject.transform.rotation = transform.rotation;
              

                openingStart = true;
            }
            
            transform.position = Vector3.Lerp(transform.position, tutoM.selectUnit.transform.position, 8.0f * Time.deltaTime);
            

        }

    }

    
}
