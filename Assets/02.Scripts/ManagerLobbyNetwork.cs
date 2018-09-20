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
        ConnectNetwork();
    }


    //네트워크 연결 및 확인
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



    //랜덤룸 입장
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
            byte MaxPlayer = (byte)Random.RandomRange(2, 5);
            int Map = (byte)Random.RandomRange(1, 5); ;
            CreateRoom("WarGround_" + Random.RandomRange(1, 1000) + "_" + Map, MaxPlayer, Map);
        }
    }

    //커스텀룸 제작
    public void CreateRoom(string Name,byte MaxPlayer,int Map)
    {
        RoomOptions option = new RoomOptions
        {
            PublishUserId = true,
            MaxPlayers = MaxPlayer,
            IsOpen = true
        };
        PhotonNetwork.CreateRoom(Name, option, TypedLobby.Default);
    }

    //커스텀룸 입장
    public void JoinCustom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    //방 떠나기
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    //방 리스트 얻어오기
    public RoomInfo[] GetRoomList()
    {
        return PhotonNetwork.GetRoomList();
    }

    //유저가 들어왔을때
    private void OnPlayerConnected(NetworkPlayer player)
    {
        if (PhotonNetwork.room.MaxPlayers == PhotonNetwork.room.PlayerCount)
        {
            //매치매이킹 성공
        }
    }
    void Update()
    {
        Debug.Log(PhotonNetwork.connected + " " + PhotonNetwork.insideLobby + " " + PhotonNetwork.inRoom);
    }
}