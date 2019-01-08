using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REcameraManager : MonoBehaviour {

    [SerializeField] Camera cam;

	public void CameraMove(REunitInfo player)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x -(3.81f), transform.position.y, player.transform.position.z - 7.1f), 5 * Time.deltaTime);
    }
}
