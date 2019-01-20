using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Steamworks;

public class Intro : MonoBehaviour {

    public VideoPlayer vd;

    Coroutine coroutine;

    void Start()
    {
        coroutine = StartCoroutine("setting");
        StartCoroutine(IntroCheck());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(IntroCheck());
            Skip();
        }
    }

    IEnumerator setting()
    {
        while (!SteamManager.Initialized)
        {
            Debug.Log(SteamManager.Initialized);
            yield return null;
        }
        PhotonNetwork.playerName = SteamFriends.GetPersonaName();
        yield return null;
    }

    IEnumerator IntroCheck()
    {
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene("Lobby_Renewal");
    }

    void Skip()
    {
        if (coroutine != null) return;
        SceneManager.LoadScene("Lobby_Renewal");
    }
}
