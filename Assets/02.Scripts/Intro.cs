using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour {

    public VideoPlayer vd;

    private void Start()
    {
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

    IEnumerator IntroCheck()
    {
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene("Lobby_Renewal");
    }

    void Skip()
    {
        SceneManager.LoadScene("Lobby_Renewal");
    }
}
