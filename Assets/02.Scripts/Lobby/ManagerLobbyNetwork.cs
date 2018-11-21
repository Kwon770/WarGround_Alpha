using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLobbyNetwork : Photon.MonoBehaviour
{
    public static ManagerLobbyNetwork instance;

    [SerializeField] public int EliteType;
    [SerializeField] string GameVersion;
    [SerializeField] string UserName;
    private ManagerLobbyCanvas Canvas;
    private ManagerLobbySys Sys;

    public MatchManager MatchManager;

    void Start()
    {
        UserName = "Guest" + Random.Range(1, 1000);
        PhotonNetwork.playerName = UserName;//이름 지정 다음에 지워야함
        instance = this;//싱글톤
        ConnectNetwork();//인터넷연결
        MatchManager = null;
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
        if (PhotonNetwork.insideLobby)
        {
            PhotonNetwork.playerName = UserName;
            return true;
        }
        return false;
    }

    public void SetUserID(string name)
    {
        UserName = name;
        PhotonNetwork.playerName = UserName;
    }

    //랜덤룸 입장
    public void JoinRandom()
    {
        //네트워크 미연결시 리턴
        if (!ConnectNetwork()) return;

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
            int Map = 1;//임시로 기본맵인 1
            CreateRoom("WarGround_" + Random.Range(1, 1000), Map);
        }
    }

    //커스텀룸 제작
    public void CreateRoom(string Name, int Map)
    {
        //네트워크 미연결시 리턴
        if (!ConnectNetwork()) return;

        RoomOptions option = new RoomOptions
        {
            PublishUserId = true,
            MaxPlayers = 2,
            IsOpen = true
        };
        PhotonNetwork.CreateRoom(Name, option, TypedLobby.Default);
    }

    //커스텀룸 입장
    public void JoinCustom(string roomName)
    {
        //네트워크 미연결시 리턴
        if (!ConnectNetwork()) return;

        PhotonNetwork.JoinRoom(roomName);
    }

    //방 떠나기
    public void LeaveRoom()
    {
        ResetInfo();
        PhotonNetwork.LeaveRoom();
    }

    //방 리스트 얻어오기
    public RoomInfo[] GetRoomList()
    {
        return PhotonNetwork.GetRoomList();
    }

    public void SetName(string name)
    {
        UserName = name;
    }

    private void Update()
    {
        if(PhotonNetwork.inRoom)
        {
            Debug.Log(PhotonNetwork.room.Name + " " + PhotonNetwork.room.PlayerCount + " " + PhotonNetwork.playerName);
            if (PhotonNetwork.room.MaxPlayers == PhotonNetwork.room.PlayerCount)
            {
                if (PhotonNetwork.isMasterClient && MatchManager == null)
                {
                    MatchManager = PhotonNetwork.Instantiate("MatchManager", Vector3.zero, Quaternion.identity, 0).GetComponent<MatchManager>();
                }
                //매치매이킹 성공
            }
            else if (MatchManager != null)
            {
                PhotonView.Destroy(MatchManager.gameObject);
            }
        }
    }

    // 1. savage, merica, kami, pluto, brown, royal
    public void SetElite(int type)
    {
        EliteType = type;
    }
    public void ResetInfo()
    {
        EliteType = 0;
    }

}