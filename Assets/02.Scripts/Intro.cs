using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Steamworks;

public class Intro : MonoBehaviour {

    public VideoPlayer video;
    public AudioSource audio;
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
            if (coroutine != null) return;
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
        coroutine = null;
        yield break; ;
    }

    IEnumerator IntroCheck()
    {
        yield return new WaitForSeconds(12);
        Skip();
    }

    void Skip()
    {
        Destroy(video);
        Destroy(audio);
        Destroy(this);
        SceneManager.LoadScene("Lobby_Renewal");
    }
}
