using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : Photon.MonoBehaviour {
    
    [SerializeField] int[] EliteSelect;
    [SerializeField] string[] PlayerList;
    [SerializeField] string[] Elite;
    bool sceneCheck;
    string MapType;
    
    void Start ()
    {
        ManagerLobbyNetwork.instance.MatchManager = this;
        DontDestroyOnLoad(this.gameObject);

        PlayerList = new string[PhotonNetwork.room.MaxPlayers];
        EliteSelect = new int[PhotonNetwork.room.MaxPlayers];
        sceneCheck = true;

        if (PhotonNetwork.room.MaxPlayers == 2)
        {
            MapType = "Plane";
        }
        else if (PhotonNetwork.room.MaxPlayers == 3)
        {
            MapType = "Desert";
        }
        else if (PhotonNetwork.room.MaxPlayers == 4)
        {
            MapType = "Snow";
        }

        photonView.RPC("GetUserID", PhotonTargets.MasterClient, PhotonNetwork.playerName);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        if (stream.isWriting)
        {
//            stream.SendNext(PlayerCount);
        }
        else
        {
//            PlayerCount = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void GetUserID(string name)
    {
        Debug.Log(name);
        for (int i = 0; i < PhotonNetwork.room.MaxPlayers; i++)
        {
            if (PlayerList[i] == null)
            {
                PlayerList[i] = name;
                break;
            }
        }
    }

    [PunRPC]
    public void SetElite(string name, int eliteType)
    {
        Debug.Log(name + " " + eliteType);
        for (int i = 0; i < PhotonNetwork.room.MaxPlayers; i++)
        {
            if (PlayerList[i] == name)
            {
                EliteSelect[i] = eliteType;
                break;
            }
        }
    }
    private void Update()
    {
        bool check = true;
        for(int i = 0; i < PhotonNetwork.room.MaxPlayers; i++)
        {
            if (EliteSelect[i] == 0) check = false;
        }
        if (check && sceneCheck)
        {
            sceneCheck = false;
            //씬 변경 시작
            photonView.RPC("SceneLoadStart", PhotonTargets.All);
        }
    }
    [PunRPC]
    public void SceneLoadStart()
    {
        StartCoroutine(SceneLoad());
    }
    IEnumerator SceneLoad()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(MapType);
        yield return async;
        if (PhotonNetwork.isMasterClient)
        {
            GameSet();
        }
    }
    void GameSet()
    {
        Debug.Log("AAAAAAAAAAAAAAAAA");
        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            Debug.Log(Elite[EliteSelect[i]]);
            PhotonNetwork.Instantiate(Elite[EliteSelect[i]], MapSet.instance.SpawnPoint[i].transform.position, MapSet.instance.SpawnRotation[i], 0);
        }
        //엘리트 유닛 생성
    }
}
