using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLobbyNetwork : MonoBehaviour
{
    private string[] roomList = new string[10];

    private ManagerLobbyCanvas Canvas;
    private ManagerLobbySys Sys;

    void Start()
    {

        Canvas = gameObject.GetComponent<ManagerLobbyCanvas>();
        Sys = gameObject.GetComponent<ManagerLobbySys>();
    }



    public bool ConnectNetwork()
    {
        return false;
    }

    public void JoinRandom()
    {

        Sys.MatchingDone();
    }

    public void JoinCustom(string roomName)
    {

    }

    public bool JoinSecret(string roomName, string pwd)
    {
        return false;
    }

    //public string[] GetRoomList()
    //{
    //    return roomList[];
    //}
}
	
