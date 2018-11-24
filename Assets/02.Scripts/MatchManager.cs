using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchManager : Photon.MonoBehaviour {
    
    [SerializeField] public string[] PlayerList;
    [SerializeField] string[] Elite;
    int check;
    bool sceneCheck;
    string MapType;
    
    void Awake ()
    {
        check = 0;

        LobbyNetwork.instance.MatchManager = this;
        DontDestroyOnLoad(this.gameObject);

        PlayerList = new string[PhotonNetwork.room.MaxPlayers];
        sceneCheck = true;
        
        MapType = "Plane";
        photonView.RPC("GetUserID", PhotonTargets.All, PhotonNetwork.playerName);
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

    //씬 로드 체크
    [PunRPC]
    public void Check()
    {
        check++;
        if (check == PhotonNetwork.room.MaxPlayers) Destroy(this.gameObject);
    }

    private void Update()
    {
        if (sceneCheck)
        {
            Debug.Log("씬 이동");
            sceneCheck = false;
            //씬 변경 시작
            if(PhotonNetwork.isMasterClient)PhotonNetwork.Instantiate("NetworkManager", Vector3.zero, Quaternion.identity, 0);

            StartCoroutine(SceneLoad());
        }
        if (check == PhotonNetwork.room.MaxPlayers)
        {
            Destroy(this.gameObject);
        }
    }
    
    //씬로드
    IEnumerator SceneLoad()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(MapType);
        yield return async;
        
        Debug.Log(LobbyNetwork.instance.EliteType);
        //각자 지정한 엘리트 스폰
        GameObject unit = PhotonNetwork.Instantiate(Elite[LobbyNetwork.instance.EliteType], MapSet.instance.SpawnPoint[PhotonNetwork.player.ID - 1].transform.position, MapSet.instance.SpawnRotation[PhotonNetwork.player.ID - 1], 0);
        unit.GetComponent<UnitInfo>().SetOwner(PhotonNetwork.playerName);
        unit.GetComponent<UnitInfo>().x = MapSet.instance.SpawnPoint[PhotonNetwork.player.ID - 1].GetComponent<TileInfo>().x;
        unit.GetComponent<UnitInfo>().y = MapSet.instance.SpawnPoint[PhotonNetwork.player.ID - 1].GetComponent<TileInfo>().y;

        photonView.RPC("Check", PhotonTargets.MasterClient);
    }

    //동기화
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //좌표 동기화
        }
        else
        {
            //좌표 동기화
        }
    }
}
