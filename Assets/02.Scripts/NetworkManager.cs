using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour {

    public static NetworkManager network;

    [SerializeField] public string turnOwner;
    [SerializeField] public string[] UserList;
    GameObject endButton;

    //초기화
    void Awake ()
    {
        network = this;
        DontDestroyOnLoad(this.gameObject);
        if (PhotonNetwork.isMasterClient) turnOwner = PhotonNetwork.playerName;
        UserList = null;
        StartCoroutine("Setting");
	}
    [PunRPC]
    public void SetMaster(string client)
    {
        Debug.Log(client);
        if (UserList == null)
        {
            UserList = new string[PhotonNetwork.room.PlayerCount];
        }
        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (UserList[i] == client) break;
            if (UserList[i] == null)
            {
                UserList[i] = client;
                break;
            }
        }
    }
    IEnumerator Setting()
    {
        while (true)
        {
            if (SceneManager.GetActiveScene().name != "Lobby_Renewal")
            {
                endButton = GameObject.Find("EndButton");
                endButton.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => EndTurn(PhotonNetwork.playerName)));
                break;
            }
            yield return null;
        }
        photonView.RPC("SetMaster", PhotonTargets.MasterClient, PhotonNetwork.playerName);
        yield return null;
    }

    //턴종료 버튼
    public void EndTurn(string client)
    {
        Debug.Log(client);
        if (turnOwner == client)
        {
            SetOwnerUI("Not_Mine");
            photonView.RPC("ChangeOwner", PhotonTargets.MasterClient, turnOwner);
        }
    }
    [PunRPC]
    public void ChangeOwner(string user)
    {
        int index = 0;
        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (UserList[i] == user)
            {
                index = (i+1)% PhotonNetwork.room.PlayerCount;
            }
        }
        turnOwner = UserList[index];
        
        if (turnOwner == PhotonNetwork.playerName) photonView.RPC("TurnSet", PhotonTargets.All, turnOwner);
        else photonView.RPC("SetOwnerUI", PhotonTargets.All, turnOwner);
    }
    [PunRPC]
    public void SetOwnerUI(string user)
    {
        Debug.Log("턴오너 : " + user + ", 내이름 : " + PhotonNetwork.playerName + "----" + endButton.transform.GetSiblingIndex());
        if (user == PhotonNetwork.playerName && endButton.transform.GetSiblingIndex() == 0)
        {
            endButton.transform.SetSiblingIndex(1);
            //마이턴
        }
        else
        {
            endButton.transform.SetSiblingIndex(0);
            //적의턴
        }
    }
    //턴셋팅
    [PunRPC]
    public void TurnSet(string user)
    {
        Debug.Log("턴셋팅");
        StartCoroutine("TurnSetting", user);
    }
    IEnumerator TurnSetting(string user)
    {
        int myTerritory=0;
        foreach (var unit in GameData.data.Units)
        {
            Debug.Log(unit);

            //내 유닛일경우
            if (unit.Owner == PhotonNetwork.playerName)
            {
                TileInfo SP = GameData.data.FindTile(unit.x, unit.y);
                List<TileInfo> tiles = Calculator.Calc.GetInrangeTile(SP, 1);

                foreach (var tile in tiles)
                {
                    tile.GetOcccupy();
                    if(unit.Kinds == "Harrier") tile.GetOcccupy();
                }
            }

            //내 유닛이 아닌경유
            else
            {
                TileInfo SP = GameData.data.FindTile(unit.x, unit.y);
                List<TileInfo> tiles = Calculator.Calc.GetInrangeTile(SP, 1);

                foreach (var tile in tiles)
                {
                    tile.LoseOcccupy();
                    if (unit.Kinds == "Harrier") tile.LoseOcccupy();
                }
            }
            unit.Act = unit.MaxAct;

            yield return null;
        }
        foreach(var tile in GameData.data.Tiles)
        {
            tile.cost = tile.idlecost;
            if (tile.occupyPoint > 1) tile.occupyPoint = 1;
            if (tile.occupyPoint < -1) tile.occupyPoint = -1;
            if (tile.occupyPoint == 1) myTerritory++;

        }

        yield return null;

        Debug.Log("gggggggg");
        SetOwnerUI(user);
    }

    private void Update()
    {
        Debug.Log("MyName : " + PhotonNetwork.playerName + ",  turnOwner : " + turnOwner);
    }

    //동기화
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //좌표 동기화
            stream.SendNext(turnOwner);
        }
        else
        {
            //좌표 동기화
            turnOwner = (string)stream.ReceiveNext();
        }
    }
}
