using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadTutorial : MonoBehaviour {

    public Image[] anim;

    string[] hex = { "#FFAAAA", "#90D7FF", "#8FFF91", "#E38FFF", "#8FFFCE", "#FF8FA5", "DBAAFF", "E5FFAA", "FFAAC1" };
    
    bool ready = false;

    public void Loading()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(Anim());
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        float time = 0;
        AsyncOperation async = SceneManager.LoadSceneAsync("TutorialRemake1114");
        async.allowSceneActivation = false;

        while(time <= 3 && !async.isDone)
        {
            time += Time.deltaTime;
            yield return null;
        }
        async.allowSceneActivation = true;
        gameObject.SetActive(false);
    }

    public void Exiting()
    {
        StartCoroutine(Anim());
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        float time = 0;StartCoroutine(Anim());
        StartCoroutine(Load());
        AsyncOperation async = SceneManager.LoadSceneAsync("Lobby_Renewal");
        async.allowSceneActivation = false;

        while (time <= 3 && !async.isDone)
        {
            time += Time.deltaTime;
            yield return null;
        }
        async.allowSceneActivation = true;
        gameObject.SetActive(false);
    }

    IEnumerator Anim()
    {
        while(!ready)
        {
            string Hex = hex[Random.Range(0, 9)];

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
}
