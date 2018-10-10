using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [SerializeField] ManagerLobbyNetwork network;
	// Use this for initialization
	void Awake () {
        network = GetComponent<ManagerLobbyNetwork>();
	}
	
    public void EnterRandomRoom()
    {
        network.JoinRandom();
    }

}
