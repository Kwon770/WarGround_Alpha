using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLobbySys : MonoBehaviour {

    //Overlay UI
    [SerializeField] private Text Text_User;

    //Random Player ID

    [SerializeField] private Text Text_PlayerID1;
    [SerializeField] private Text Text_OpID1;
    //Custom Player ID
    [SerializeField] private Text Text_PlayerID2;
    [SerializeField] private Text Text_OpID2;

    private LobbyNetwork network;
    private string userName;

    // Use this for initialization
    void Start ()
    {
        network = GetComponent<LobbyNetwork>();
        userName = "Guest" + Random.Range(0, 1001);
        Text_User.text = userName;
        network.SetUserID(userName);
    }



    public void CustomJoin()
    {
        Text_PlayerID2.text = PhotonNetwork.playerName;
        //if(적이 있을시)
        //Text_OpID2.text = 
    }

    public void RandomJoin()
    {
        Text_PlayerID1.text = PhotonNetwork.playerName;
        //if(적이 있을시)
        //Text_OpID1.text = 
    }
}
