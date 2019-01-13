using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : Photon.MonoBehaviour {

    public static EndUI UI;

    //ingameUI
    [SerializeField] Text remainTurn;
    [SerializeField] Text myScore;
    [SerializeField] Text enemyScore;

    //EndUIValue
    [SerializeField] Text endMyScore;
    [SerializeField] Text endEnemyScore;
    [SerializeField] Text gameResult;
    [SerializeField] Transform EndCanvas;
    [SerializeField] AnimationCurve curve;


    //값
    [HideInInspector] public int _myScore;
    [HideInInspector] public int _enemyScore;
    private void Awake()
    {
        UI = this;
        _myScore = 0;
        _enemyScore = 0;
    }

    public void SetRemainTurn(int turn)
    {
        if (turn <= 0)
        {
            StartCoroutine("EndGame");

            PhotonNetwork.LeaveRoom();
            if (MatchManager.manager != null) Destroy(MatchManager.manager.gameObject);
            if (NetworkManager.network != null) Destroy(NetworkManager.network.gameObject);

            return;
        }
        remainTurn.text = turn.ToString();
    }
    public void SetScore(int my, int enemy)
    {
        _myScore = my;
        _enemyScore = enemy;

        myScore.text = my.ToString();
        enemyScore.text = enemy.ToString();
    }

    IEnumerator EndGame()
    {
        SoundManager.soundmanager.endGame();

        endMyScore.text = _myScore.ToString();
        endEnemyScore.text = _enemyScore.ToString();
        if (_myScore < _enemyScore) gameResult.text = "You Lose!";
        else if(_myScore > _enemyScore) gameResult.text = "You Win!";
        else gameResult.text = "Draw!";

        float time = 0;
        Vector3 startPos = EndCanvas.position;
        Vector3 endPos = EndCanvas.parent.position;


        while (time <= 1f)
        {
            EndCanvas.position = Vector3.LerpUnclamped(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    public void LoadScene()
    {
        foreach (UnitInfo unit in GameData.data.Units)
        {
            if (unit == null) continue;
            if(unit.photonView.isMine) Destroy(unit.gameObject);
        }
        StartCoroutine("Loading");
    }
    public IEnumerator Loading()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        float time = 0f;

        Vector3 startPos = EndCanvas.transform.position;
        Vector3 endPos = new Vector3(Screen.width/2, -Screen.height/2, 0);

        yield return null;

        while (time <= 1f || async.progress < 0.9f)
        {
            Debug.Log((time <= 1f) + " " + !async.isDone + " " + async.progress);
            EndCanvas.position = Vector3.LerpUnclamped(startPos, endPos, time);
            time += Time.deltaTime;
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
