using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLobbyNetwork : MonoBehaviour
{

    [SerializeField]string GameVersion;
    
    private ManagerLobbyCanvas Canvas;
    private ManagerLobbySys Sys;

    void Start()
    {

        Canvas = gameObject.GetComponent<ManagerLobbyCanvas>();
        Sys = gameObject.GetComponent<ManagerLobbySys>();
    }



    public bool ConnectNetwork()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(GameVersion);
        }
        if (!PhotonNetwork.insideLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        if(PhotonNetwork.insideLobby) return true;
        return false;
    }

    public void JoinRandom()
    {
        bool check = false;
        RoomInfo[] list = GetRoomList();
        foreach(RoomInfo room in list)
        {
            if(room.Name.Substring(0, 9).Equals("WarGround"))
            {
                check = true;
                PhotonNetwork.JoinRoom(room.Name);
            }
        }
        if (!check)
        {
            check = true;
            RoomOptions option=new RoomOptions();
            option.MaxPlayers = 2;
            option.IsOpen = true;
            PhotonNetwork.CreateRoom("WarGround" + Random.RandomRange(0, 1000), option,TypedLobby.Default);
        }
        if(check) Sys.MatchingDone();
    }

    public void JoinCustom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public RoomInfo[] GetRoomList()
    {
        return PhotonNetwork.GetRoomList();
    }
}
	
//스튜던트 객체 안에 이름 학번 학점넣고
//여러개를 넣어서 또다른 클래스 만들어서 종류별로 정렬 이름 입력시 학생정보구하기