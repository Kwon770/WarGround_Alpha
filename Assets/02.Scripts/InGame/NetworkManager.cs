﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour {

    public static NetworkManager network;

    [SerializeField] public string turnOwner;
    [SerializeField] public string[] UserList;
    EndTurn endButton;
    int remainTurn;

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
        if (client != PhotonNetwork.playerName) GameData.EnemyName = client;
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
                remainTurn = GameData.data.EndTurnValue;
                EndUI.UI.SetRemainTurn(remainTurn);
                EndUI.UI.SetScore(0,0);

                endButton = GameObject.Find("EndButton").GetComponent<EndTurn>();
                GameObject.Find("MyTurn").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => EndTurnClick(PhotonNetwork.playerName)));
                if (PhotonNetwork.isMasterClient) endButton.MyTurn();
                GameObject.Find("_PlayerName").GetComponent<Text>().text=PhotonNetwork.playerName;
                break;
            }
            yield return null;
        }
        photonView.RPC("SetMaster", PhotonTargets.All, PhotonNetwork.playerName);
        yield return null;
    }

    //턴종료 버튼
    public void EndTurnClick(string client)
    {
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
        if (user == PhotonNetwork.playerName)
        {
            endButton.MyTurn();
            //마이턴
        }
        else
        {
            endButton.EnemyTurn();
            //적의턴
        }
    }

    //턴셋팅
    [PunRPC]
    public void TurnSet(string user)
    {
        StartCoroutine("TurnSetting", user);
    }

    IEnumerator TurnSetting(string user)
    {
        int myTerritory = 0;
        int enemyTerritory = 0;

        EndUI.UI.SetRemainTurn(--remainTurn);

        foreach (var unit in GameData.data.Units)
        {

            unit.Act = unit.MaxAct;
            if(unit.Kinds != "Warlock" && unit.Kinds != "Valkyrie") unit.AddATK = 0;

            if (Calculator.Calc.UnitInRange(PhotonNetwork.playerName, "Ragnarr", 1, GameData.data.FindTile(unit.x, unit.y)) && (unit.Kinds != "Ragnarr" && unit.Kinds != "Valkyrie")) unit.AddATK += 2;

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
            yield return null;
        }
        foreach(var tile in GameData.data.Tiles)
        {
            tile.cost = tile.idlecost;
            if (tile.occupyPoint > 1) tile.occupyPoint = 1;
            if (tile.occupyPoint < -1) tile.occupyPoint = -1;
            if (tile.occupyPoint == 1)
            {
                if (tile.Type == 1)
                {
                    foreach (var temp in Calculator.Calc.GetInrangeTile(tile, 2))
                    {
                        if (temp.idlecost == 100) continue;
                        temp.cost = 0;
                    }
                }
                else if (tile.Type == 2)
                {
                    myTerritory += 4;   
                }
                myTerritory++;
            }
            else if (tile.occupyPoint == -1) enemyTerritory++;
        }
        EndUI.UI.SetScore(myTerritory, enemyTerritory);
        GameData.data.SetBitinium(myTerritory / 4);

        yield return null;
        SetOwnerUI(user);
    }


    // 상대의 접속이 끊겼을 때 
    public void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        EndUI.UI._enemyScore = 0;
        EndUI.UI.SetRemainTurn(0);
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
