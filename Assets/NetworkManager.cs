using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour {

    public static NetworkManager network;

    [SerializeField] public string turnOwner;
    [SerializeField] public string[] UserList;
    Image EndTurnButton;
    

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
            if (SceneManager.GetActiveScene().name != "Lobby")
            {
                var EndButton = GameObject.Find("EndButton");
                EndTurnButton = EndButton.GetComponent<Image>();
                EndButton.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => EndTurn(PhotonNetwork.playerName)));
                break;
            }
            yield return null;
        }
        photonView.RPC("SetMaster", PhotonTargets.MasterClient, PhotonNetwork.playerName);
        yield return null;
    }

    //턴종료 버튼
    void EndTurn(string client)
    {
        if (turnOwner == client)
        {
            photonView.RPC("ChangeOwner", PhotonTargets.MasterClient, client);
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
        photonView.RPC("SetOwnerUI", PhotonTargets.All, turnOwner);
        if (turnOwner == PhotonNetwork.playerName) photonView.RPC("TurnSet", PhotonTargets.All);
    }
    [PunRPC]
    public void SetOwnerUI(string user)
    {
        if (user == PhotonNetwork.playerName)
        {
            //마이턴
            EndTurnButton.color = Color.blue;
        }
        else
        {
            //적의턴
            EndTurnButton.color = Color.red;
        }
    }
    //턴셋팅
    public void TurnSet()
    {
        Debug.Log("턴셋팅");

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
