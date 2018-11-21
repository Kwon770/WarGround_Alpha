using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : Photon.MonoBehaviour {
    
    [SerializeField] string[] PlayerList;
    [SerializeField] string[] Elite;
    int check;
    bool sceneCheck;
    string MapType;
    
    void Awake ()
    {
        check = 0;

        ManagerLobbyNetwork.instance.MatchManager = this;
        DontDestroyOnLoad(this.gameObject);

        PlayerList = new string[PhotonNetwork.room.MaxPlayers];
        sceneCheck = true;
        
        MapType = "Plane";

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

    //id 갱신
    [PunRPC]
    public void GetUserID(string name)
    {
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
    public void Check()
    {
        check++;
    }

    private void Update()
    {
        if (sceneCheck)
        {
            sceneCheck = false;
            //씬 변경 시작
            if(PhotonNetwork.isMasterClient)PhotonNetwork.Instantiate("NetworkManager", Vector3.zero, Quaternion.identity, 0);
            photonView.RPC("SceneLoadStart", PhotonTargets.All);
        }
        if (check == PhotonNetwork.room.MaxPlayers) Destroy(this.gameObject);
    }
    
    //씬로드
    [PunRPC]
    public void SceneLoadStart()
    {
        StartCoroutine(SceneLoad());
    }

    IEnumerator SceneLoad()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(MapType);
        yield return async;

        //각자 지정한 엘리트 스폰
        GameObject unit = PhotonNetwork.Instantiate(Elite[ManagerLobbyNetwork.instance.EliteType - 1], MapSet.instance.SpawnPoint[PhotonNetwork.player.ID - 1].transform.position, MapSet.instance.SpawnRotation[PhotonNetwork.player.ID - 1], 0);
        unit.GetComponent<UnitInfo>().SetOwner(PhotonNetwork.playerName);
        unit.GetComponent<UnitInfo>().x = MapSet.instance.SpawnPoint[PhotonNetwork.player.ID - 1].GetComponent<TileInfo>().x;
        unit.GetComponent<UnitInfo>().y = MapSet.instance.SpawnPoint[PhotonNetwork.player.ID - 1].GetComponent<TileInfo>().y;

        photonView.RPC("Check", PhotonTargets.MasterClient);
    }
}
