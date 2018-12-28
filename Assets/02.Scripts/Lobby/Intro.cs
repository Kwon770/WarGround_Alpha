using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour {

    public VideoPlayer vd;
    public GameObject Lobby;

    bool IsIntro = true;

    private void Start()
    {
        StartCoroutine(IntroCheck());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && IsIntro)
        {
            StartCoroutine(Skip());
            StopCoroutine(IntroCheck());
        }
    }

    IEnumerator IntroCheck()
    {
        yield return new WaitForSeconds(12);

        vd.Stop();
        IsIntro = false;
        Lobby.SetActive(true);

        SoundManager.soundmanager.lobbyBGM(true);

        yield return new WaitForSeconds(1);

        StartCoroutine(Manager.instance.AlarmPanel());
        yield return new WaitForSeconds(8);
        gameObject.SetActive(false);
    }

    IEnumerator Skip()
    {
        vd.Stop();
        IsIntro = false;
        Lobby.SetActive(true);

        SoundManager.soundmanager.lobbyBGM(true);

        yield return new WaitForSeconds(1);

        StartCoroutine(Manager.instance.AlarmPanel());
        yield return new WaitForSeconds(8);
        gameObject.SetActive(false);
    }
}
