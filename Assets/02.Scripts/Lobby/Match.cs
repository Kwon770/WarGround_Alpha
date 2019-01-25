using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour {

    [SerializeField] float Speed;
    [SerializeField] AnimationCurve curve;
    public GameObject Pos;

    public GameObject match;

    public Text myName;
    public Text myChar;
    public Image MyProfile;
    public Text enemyname;
    public Text enemyChar;
    public Image EnemyProfile;

    public GameObject Complete;
    public GameObject Error;


    public Image[] anim;

    string[] hex = { "#FFAAAA", "#90D7FF", "#8FFF91", "#E38FFF", "#8FFFCE", "#FF8FA5", "DBAAFF", "E5FFAA", "FFAAC1" };

    public void LoadEnemyInfo(string name, int kinds)
    {
        // 적 이름 작성
        enemyname.text = name;

        // 적 캐릭터 작성
        switch (LobbyNetwork.instance.EliteType)
        {
            case 0:
                enemyChar.text = "Partan - Pluto";
                break;
            case 1:
                enemyChar.text = "Partan - Brownbeard Pirates";
                break;
            case 2:
                enemyChar.text = "Cora - Royal Guard";
                break;
            case 3:
                enemyChar.text = "Partan - Merica";
                break;
            case 4:
                enemyChar.text = "Cora - Kamiken";
                break;
            case 5:
                enemyChar.text = "Cora - Savageborn";
                break;
        }

        // 매칭 소리 재생
        SoundManager.soundmanager.matchStart();

        // 매칭 완료 UI
        Complete.SetActive(true);

        // 매칭 에니메이션 종료
        StopCoroutine(Anim());

        // 타이머 애니메이션
        //Timer.SetActive(true);
        //StartCoroutine(TimerAnim());
    }

    // 시작 버튼 함수
    public void EnterMatch()
    {
        // Esc 키 무효화
        Manager.instance.corutine = true;

        LobbyNetwork.instance.JoinRandom();

        match.SetActive(true);

        myName.text = "Waiting . . .";
        myChar.text = "";

        // 오류 확인
        StartCoroutine(CheckConnection());
    }

    void StartMatch()
    {
        // 매칭 시작
        StartCoroutine(Anim());

        // 내 이름 작성
        myName.text = PhotonNetwork.playerName;
        
        // 내 캐릭터 작성
        switch (LobbyNetwork.instance.EliteType)
        {
            case 0:
                myChar.text = "Partan - Pluto";
                break;
            case 1:
                myChar.text = "Partan - Brownbeard Pirates";
                break;
            case 2:
                myChar.text = "Cora - Royal Guard";
                break;
            case 3:
                myChar.text = "Partan - Merica";
                break;
            case 4:
                myChar.text = "Cora - Kamiken";
                break;
            case 5:
                myChar.text = "Cora - Savageborn";
                break;
        }
    }

    //취소 버튼
    public void Cancel()
    {
        // Esc 키 무효화 취소
        Manager.instance.corutine = false;

        StopCoroutine(Anim());
        match.SetActive(false);
        LobbyNetwork.instance.LeaveRoom();
    }

    // 프로필 사진 로드
    public void LoadProfileImg(Sprite PlayerImg, Sprite EnemyImg)
    {
        MyProfile.sprite = PlayerImg;
        EnemyProfile.sprite = EnemyImg;
    }

    IEnumerator CheckConnection()
    {
        float time = 0f;
        while(time < 5f)
        {
            time += Time.deltaTime;
            if (PhotonNetwork.inRoom)
            {
                StartMatch();
                yield break;
            }
            yield return null;
        }

        // 오류 출력
        match.SetActive(false);
        StartCoroutine(ErrorMove());
    }

    IEnumerator ErrorMove()
    {
        Manager.instance.corutine = true;

        Vector3 startPos = Pos.transform.position;
        Vector3 endPos = Complete.transform.position;
        float time = 0;

        while (time <= 1)
        {
            Error.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        StopCoroutine(Anim());
        Manager.instance.corutine = false;

        startPos = Complete.transform.position;
        endPos = Pos.transform.position;
        time = 0;

        while (time <= 1)
        {
            Error.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }
    }

    IEnumerator Anim()
    {
        // 색 초기화
        for (int i = 0; i < 6; i++)
        {
            anim[i].color = Color.white;
        }

        while (true)
        {
            string Hex = hex[Random.Range(0, 9)];

            // 애니메이션
            for (int i = 0; i < 6; i++)
            {
                Color animColor = new Color();
                ColorUtility.TryParseHtmlString(Hex, out animColor);

                anim[i].color = animColor;

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
