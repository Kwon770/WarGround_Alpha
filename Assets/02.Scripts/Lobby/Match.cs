using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour {

    public GameObject match;

    private void Update()
    {
        if (PhotonNetwork.inRoom)
        {
            //방에들어왔을때
            if (LobbyNetwork.instance.MatchManager != null)
            {
                //두명일때


            }
        }
        
    }

    public void RandomMatch()
    {
        match.SetActive(true);
        LobbyNetwork.instance.JoinRandom();
    }

    public void Cancel()
    {
        match.SetActive(false);
        LobbyNetwork.instance.LeaveRoom();
    }
}
