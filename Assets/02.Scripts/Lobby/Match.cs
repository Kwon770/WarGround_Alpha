using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour {

    public GameObject match;

    public Text myName;
    public Text myChar;
    public Text enemyname;
    public Text enemyChar;

    public Image[] anim;

    string[] hex = { "#FFAAAA", "#90D7FF", "#8FFF91", "#E38FFF", "#8FFFCE", "#FF8FA5" };

    private void Update()
    {
        if (PhotonNetwork.inRoom)
        {
            //방에들어왔을때
            if (LobbyNetwork.instance.MatchManager != null)
            {
                //두명일때
                foreach (string player in LobbyNetwork.instance.MatchManager.PlayerList)
                {
                    if(player != null && player != myName + "")
                    {
                        enemyname.text = player;
                    }
                }
            }
        }
        
    }

    public void RandomMatch()
    {
        match.SetActive(true);
        LobbyNetwork.instance.JoinRandom();

        myName.text = PhotonNetwork.playerName;
        
        switch (LobbyNetwork.instance.EliteType)
        {
            case 0:
                myChar.text = "Partan - Pluto";
                break;
            case 1:
                myChar.text = "Partan - Merica";
                break;
            case 2:
                myChar.text = "Cora - Kamiken";
                break;
            case 3:
                myChar.text = "Cora - Savageborn";
                break;
            case 4:
                myChar.text = "Partan - Brownbeard Pirates";
                break;
            case 5:
                myChar.text = "Cora - Royal Guard";
                break;
        }
    }

    public void Cancel()
    {
        match.SetActive(false);
        LobbyNetwork.instance.LeaveRoom();
    }


    IEnumerator Anim()
    {
        string Hex = hex[Random.Range(0, 6)];

        for (int i = 0; i < 6; i++)
        {
            Debug.Log(Hex);

            Color animColor = new Color();
            ColorUtility.TryParseHtmlString(Hex, out animColor);

            anim[i].color = animColor;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
