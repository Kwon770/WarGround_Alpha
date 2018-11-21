using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [SerializeField] LobbyNetwork network;
	// Use this for initialization
	void Awake () {
        network = GetComponent<LobbyNetwork>();
	}
	
    public void EnterRandomRoom()
    {
        network.JoinRandom();
    }

}
