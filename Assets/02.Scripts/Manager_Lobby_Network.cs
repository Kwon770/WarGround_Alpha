using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Lobby_Network : MonoBehaviour
{
    private string[] roomList = new string[10];

    private Manager_Lobby_Canvas Canvas;
    private Manager_Lobby_Sys Sys;

    void Start()
    {

        Canvas = gameObject.GetComponent<Manager_Lobby_Canvas>();
        Sys = gameObject.GetComponent<Manager_Lobby_Sys>();
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
	
