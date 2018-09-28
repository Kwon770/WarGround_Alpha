using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLobbyCanvas : MonoBehaviour {

    //System
    [SerializeField] private GameObject Blocking;

    [SerializeField] private GameObject scene_Play;
    [SerializeField] private GameObject scene_Char;
    [SerializeField] private GameObject scene_Lobby;

    [SerializeField] private Text Text_Home;
    [SerializeField] private Text Text_Play;
    [SerializeField] private Text Text_Char;

    [SerializeField] private Text Text_GameType;

    [SerializeField] private GameObject Fx_Home;
    [SerializeField] private GameObject Fx_Play;
    [SerializeField] private GameObject Fx_Char;

    [SerializeField] private GameObject Fx_BackGround_Match;
    [SerializeField] private GameObject Fx_Time2;

    private GameObject Fx_now3;
    private Text overText3;

    private GameObject Fx_last3;
    private Text LastText3;

    private int inLobby = 1;

    //Lobby Scene UI
    [SerializeField] private Text PlayedType;

    [SerializeField] private GameObject Button_Quick;
    [SerializeField] private GameObject Button_Credit;

    [SerializeField] private GameObject Panel_Option1;
    [SerializeField] private GameObject Panel_Option2;
    [SerializeField] private GameObject Button_Option1;
    [SerializeField] private GameObject Button_Option2;
    [SerializeField] private GameObject Button_Quit;

    private bool Option1 = false;
    private bool Option2 = false;

    private GameObject overButton4;

    private int QuickType;


    //Match Scene UI
    //Button - Text
    //BeforeMatch
    [SerializeField] private Text Button_Random;
    [SerializeField] private Text Button_Custom;
    [SerializeField] private Text Button_Tutorial;

    [SerializeField] private GameObject infor_Custom;
    [SerializeField] private GameObject infor_Random;
    [SerializeField] private GameObject infor_Tutorial;
    [SerializeField] private GameObject infor_Null;

    [SerializeField] private GameObject Fx_Random;
    [SerializeField] private GameObject Fx_Custom;
    [SerializeField] private GameObject Fx_Tutorial;

    [SerializeField] private GameObject BackGround_Before;

    private Text overButton;
    private GameObject overInfor;

    private GameObject Fx_now;

    //AfterRandom
    [SerializeField] private GameObject Button_Cancel;
    [SerializeField] private GameObject Fx_Time1;
    [SerializeField] private Text TimeS;
    [SerializeField] private Text TimeM;

    [SerializeField] private GameObject BackGround_AfterRandom;

    private bool canceled = false;
    private int matchingTimeS = 0;
    private int matchingTimeM = 0;

    //AfterCustom

    [SerializeField] private GameObject[] roomObject = new GameObject[7];
    RoomInfo[] list;
    private int number;
    private int page;
    private int numberNow;

    private int  [] password;

    [SerializeField] private GameObject BackGround_AfterCustom;

    [SerializeField] private GameObject panel_CreateRoom;
    [SerializeField] private GameObject panel_enterPrivate;

    [SerializeField] private GameObject Button_Created;

    [SerializeField] private InputField createdName;
    [SerializeField] private InputField createdPassword;
    [SerializeField] private InputField enterPassword;
    [SerializeField] private GameObject createdPasswordText;
    [SerializeField] private Dropdown createdMap;
    [SerializeField] private Toggle createdPrivate;

    [SerializeField] private Text roomInfo_Title;
    [SerializeField] private Text roomInfo_State;
    [SerializeField] private Text roomInfo_Map;

    [SerializeField] private Text roomInfo_Number;

    // 맵 사진 ?





    //Character Scene UI
    [SerializeField] private GameObject Button_team1;
    [SerializeField] private GameObject Button_team2;
    [SerializeField] private GameObject Button_Common;



    private ManagerLobbyNetwork network;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inLobby == 1)
            {
                if(Option1)
                {
                    Panel_Option1.SetActive(false);
                    Panel_Option2.SetActive(false);
                    Blocking.SetActive(false);
                    Option1 = false;
                }
                else if (Option2)
                {
                    Panel_Option1.SetActive(true);
                    Panel_Option2.SetActive(false);
                    Option2 = false;
                    Option1 = true;
                }
                else if (!Option1 && !Option2)
                {
                    Panel_Option1.SetActive(true);
                    Blocking.SetActive(true);
                    Option1 = true;
                }
            }
            else if (inLobby == 4)
            {
                BackGround_AfterCustom.SetActive(false);
                BackGround_Before.SetActive(true);

                inLobby = 2;
            }
            else if (inLobby == 10)
            {
                inLobby = 4;
                panel_CreateRoom.SetActive(false);
                Blocking.SetActive(false);
                a = false;
                createdPassword.text = "";
                createdPassword.gameObject.SetActive(false);
                createdPasswordText.SetActive(false);
            }
            else if(inLobby == 11)
            {
                inLobby = 4;
                panel_enterPrivate.SetActive(false);
            }
            else
            {
                //current Scene Fx On
                Fx_Home.SetActive(true);
                Text_Home.GetComponent<Text>().color = Color.white;

                //Last Scene Fx Off
                Fx_last3.SetActive(false);
                Fx_last3 = Fx_Home;
                LastText3.color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
                LastText3 = Text_Home;

                StartCoroutine(Cor1());
            }
        }
    }
    private void Start()
    {
        network = GetComponent<ManagerLobbyNetwork>();

        Fx_last3 = Fx_Home;
        LastText3 = Text_Home;
        if (PlayerPrefs.HasKey("PlayedType"))
        {
            QuickType = PlayerPrefs.GetInt("PlayedType");
            if (QuickType == 1) PlayedType.text = "Random Match";
            else if (QuickType == 2) PlayedType.text = "Custom Match";
            else if (QuickType == 3) PlayedType.text = "Tutorial";
        }
    }


    /// <summary>
    /// 
    /// </summary>
    //System(Common) Funtion
    public void ToLobby()
    {
        //current Scene Fx On
        Fx_now3.SetActive(true);
        overText3.GetComponent<Text>().color = Color.white;

        //Last Scene Fx Off
        Fx_last3.SetActive(false);
        Fx_last3 = Fx_Home;
        LastText3.color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        LastText3 = Text_Home;

        StartCoroutine(Cor1());
    }
    IEnumerator Cor1()
    {
        Camera.main.transform.DOMoveX(0, 0.7f).SetEase(Ease.OutQuart);
        inLobby = 1;
        yield return new WaitForSeconds(0.3f);
    }

    public void OverExit3()
    {
        if ((Fx_now3 == Fx_Home && inLobby != 1) || (Fx_now3 == Fx_Play && (inLobby != 2 || inLobby != 4)) || (Fx_now3 == Fx_Char && inLobby != 3))
        {
            Fx_now3.SetActive(false);
            overText3.color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        }
    }

    public void HomeOver()
    {
        Fx_now3 = Fx_Home;
        Fx_now3.SetActive(true);
        overText3 = Text_Home;
        overText3.color = Color.white;
    }
    public void PlayOver()
    {
        Fx_now3 = Fx_Play;
        Fx_now3.SetActive(true);
        overText3 = Text_Play;
        overText3.color = Color.white;
    }
    public void CharOver()
    {
        Fx_now3 = Fx_Char;
        Fx_now3.SetActive(true);
        overText3 = Text_Char;
        overText3.color = Color.white;
    }


    //public void Set






    /// <summary>
    /// 
    /// </summary>
    //Lobby Scene Function
    public void OverExit4()
    {
        overButton4.GetComponent<Outline>().enabled = false;
    }

    public void QuickOver()
    {
        overButton4 = Button_Quick;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void QuickPressed()
    {
        if (QuickType == 1)
        {

            ToMatch();

            Fx_BackGround_Match.SetActive(true);

            BackGround_AfterRandom.SetActive(true);
            BackGround_Before.SetActive(false);

            StartCoroutine(MatchingAnim());
            StartCoroutine(MatchingAnim2());

            network.JoinRandom();
        }
        else if (QuickType == 2)
        { 
            ToMatch();
            inLobby = 4;

            BackGround_AfterCustom.SetActive(true);
            BackGround_Before.SetActive(false);

            RoomListPrepare();
        }
        else if (QuickType == 3)
        {
            // 튜토리얼 짜지는거 보고 코딩
        }
    }

    public void CreditOver()
    {
        overButton4 = Button_Credit;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void CreditPressed()
    { 
    }

    public void Option1Over()
    {
        overButton4 = Button_Option1;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void Option1Pressed()
    {
        Panel_Option1.SetActive(true);
        Blocking.SetActive(true);
        Option1 = true;
    }

    public void Option2Over()
    {
        overButton4 = Button_Option2;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void Option2Pressed()
    {
        Panel_Option1.SetActive(false);
        Panel_Option2.SetActive(true);
        Option1 = false;
        Option2 = true;
    }

    public void QuitOver()
    {

        overButton4 = Button_Quit;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void QuitPressed()
    {
        Application.Quit();
    }




    //Match Scene Function
    public void ToMatch()
    {
        if(inLobby != 4) inLobby = 2;
        Camera.main.transform.DOMoveX(213, 0.7f).SetEase(Ease.OutQuart);

        //current Scene Fx On
        Fx_Play.SetActive(true);
        Text_Play.GetComponent<Text>().color = Color.white;

        //Last Scene Fx Off
        Fx_last3.SetActive(false);
        Fx_last3 = Fx_Play;
        LastText3.GetComponent<Text>().color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        LastText3 = Text_Play;
    }

    public void OverExit()
    {
        overInfor.SetActive(false);
        infor_Null.SetActive(true);
        overButton.color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        Fx_now.SetActive(false);
    }

    public void RandomOver()
    {
        infor_Null.SetActive(false);
        infor_Random.SetActive(true);
        overInfor = infor_Random;
        Button_Random.color = Color.white;
        overButton = Button_Random;
        Fx_Random.SetActive(true);
        Fx_now = Fx_Random;
    }
    public void RandomPressed()
    {
        OverExit();

        PlayerPrefs.SetInt("PlayedType", 1);
        Text_GameType.text = "Random Match";
        PlayedType.text = "Random";
        QuickType = 1;
        Fx_BackGround_Match.SetActive(true);

        BackGround_AfterRandom.SetActive(true);
        BackGround_Before.SetActive(false);

        StartCoroutine(MatchingAnim());
        StartCoroutine(MatchingAnim2());

        network.JoinRandom();
    }
    IEnumerator MatchingAnim()
    {
        while (true)
        {
            Fx_Time1.transform.DORotate(new Vector3(0, 180, 0), 0.7f).SetEase(Ease.OutExpo);
            Fx_Time2.transform.DORotate(new Vector3(0, 180, 0), 0.7f).SetEase(Ease.OutExpo);
            yield return new WaitForSeconds(0.7f);
            Fx_Time1.transform.DORotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutExpo);
            Fx_Time2.transform.DORotate(new Vector3(0, 0, 0), 1).SetEase(Ease.OutExpo);
            yield return new WaitForSeconds(1f);

            if (canceled) break; canceled = false;
        }
    }
    IEnumerator MatchingAnim2()
    {
        while(true)
        {
            if(matchingTimeS >= 60)
            {
                matchingTimeM++;
                matchingTimeS = 0;

                if(matchingTimeM < 10)
                {
                    TimeM.text = "0" + matchingTimeM;
                }
                else
                {
                    TimeM.text = "" + matchingTimeM;
                }
            }
            else
            {
                matchingTimeS++;

                if (matchingTimeS < 10)
                {
                    TimeS.text = "0" + matchingTimeS;
                }
                else
                {
                    TimeS.text = "" + matchingTimeS;
                }
            }

            yield return new WaitForSeconds(1);
            if (canceled)
            {
                canceled = false;
                matchingTimeS = 0;
                TimeS.text = "00";
                matchingTimeM = 0;
                TimeM.text = "00";

                break;
            }
        }
    }

    public void CancelOver()
    {
        overButton4 = Button_Cancel;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void CancelPressed()
    {
        OverExit4();
        network.LeaveRoom();

        canceled = true;
        Fx_BackGround_Match.SetActive(false);

        BackGround_AfterRandom.SetActive(false);
        BackGround_Before.SetActive(true);
    }

    public void CustomOver()
    {
        infor_Null.SetActive(false);
        infor_Custom.SetActive(true);
        overInfor = infor_Custom;
        Button_Custom.color = Color.white;
        overButton = Button_Custom;
        Fx_Custom.SetActive(true);
        Fx_now = Fx_Custom;
    }
    public void CustomPressed()
    {
        OverExit();

        PlayerPrefs.SetInt("PlayedType", 2);
        PlayedType.text = "Custom";
        QuickType = 2;
        inLobby = 4;

        BackGround_AfterCustom.SetActive(true);
        BackGround_Before.SetActive(false);

        RoomListPrepare();
    }

    public void TutorialOver()
    {
        infor_Null.SetActive(false);
        infor_Tutorial.SetActive(true);
        overInfor = infor_Tutorial;
        Button_Tutorial.color = Color.white;
        overButton = Button_Tutorial;
        Fx_Tutorial.SetActive(true);
        Fx_now = Fx_Tutorial;
    }
    public void TutorialPressed()
    {
        OverExit();

        PlayerPrefs.SetInt("PlayedType", 3);
        PlayedType.text = "Tutorial";
        QuickType = 3;
    }


    //AfterCustom
    
    public void CreateRoom()
    {
        inLobby = 10;

        panel_CreateRoom.SetActive(true);
        Blocking.SetActive(true);
        createdName.text = "";
    }

    public void CreatedOver()
    {
        overButton4 = Button_Created;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void Created()
    {
        inLobby = 4;

        if (createdPrivate.isOn) network.CreateRoom(createdName.text + '_' + UnityEngine.Random.Range(1001, 2000) + "_" + createdPassword.text + "_" + createdMap.value, createdMap.value);
        else network.CreateRoom(createdName.text + "_" + createdMap.value, createdMap.value);

        panel_CreateRoom.SetActive(false);
        Blocking.SetActive(false);

        //
        createdPassword.text = "";
        createdPassword.gameObject.SetActive(false);
        createdPasswordText.SetActive(false);
    }
    private bool a = false;
    public void PrivateOn()
    {
        if(!a)
        {
            a = true;
            createdPassword.gameObject.SetActive(true);
            createdPasswordText.SetActive(true);
        }
        else
        {
            a = false;
            createdPassword.text = "";
            createdPassword.gameObject.SetActive(false);
            createdPasswordText.SetActive(false);
        }
    }


    // name - string방이름_int 맵
    public void RoomListPrepare()
    {
        // page = 0 - 1페이지
        page = 0;
        list = network.GetRoomList();
        number = list.Length;
        roomInfo_Number.text = "(" + number + ")";
        //방 개수 한 페이지 이상일경우 7개만 로드
        if (number > 7)
        {
            numberNow = 7;
        }
        //한 페이지 이하면 갯수만큼 로드
        else numberNow = number;


        //모두 끄기
        for (int i = 0; i < 7; i++) roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
        for (int i = 0; i < 7; i++) roomObject[i].SetActive(false);

        if (numberNow != 0)
        {
            //차례로 키고 정보 기록
            for (int i = 0; i < numberNow; i++)
            {
                string[] infor = list[i].Name.Split('_');
                Debug.Log("0" + infor[0]);
                Debug.Log("1" +infor[1]);
                Debug.Log("2" + infor[2]);
                Debug.Log("3" + infor[3]);

                if (infor.Length == 2) //Custom
                {
                    roomObject[i].SetActive(true);

                    //제목
                    roomObject[i].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    //맵
                    switch(Convert.ToInt32(infor[1]))
                    {
                        case 0:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else if (infor.Length == 3) //Random
                {
                    roomObject[i].SetActive(true);
                    roomObject[i].transform.GetChild(4).gameObject.SetActive(true);
                    password[i] = UnityEngine.Random.Range(10000, 99999);

                    //제목
                    roomObject[i].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (Convert.ToInt32(infor[2]))
                    {
                        case 0:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else //Custom Private
                {
                    roomObject[i].SetActive(true);
                    roomObject[i].transform.GetChild(4).gameObject.SetActive(true);

                    password[i] = Convert.ToInt32(infor[2]);
                    //제목
                    roomObject[i].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (Convert.ToInt32(infor[3]))
                    {
                        case 0:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
            }
        }

        //list[i].Name; //name_mode
        //list[i].MaxPlayers;
        //list[i].PlayerCount;
        //list[i].IsOpen; // true면 waiting flase playing
    }

    public void Refresh()
    {
        for (int i = 0; i < 7; i++) roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
        //모두 끄기
        for (int i = 0; i < 7; i++) roomObject[i].SetActive(false);

        list = network.GetRoomList();
        number = list.Length;
        roomInfo_Number.text = "(" + number + ")";
        //페이지에 따른 갯수이상이면 최대 페이지 만큼 로드
        if (number > 7 * (page + 1)) numberNow = 7 * (page + 1);
        //그 페이지를 채우지 못하만큼 작아진 경우 모드 끄기
        else if (number <= 6 * page) numberNow = 0;
        //아니면 현재 페이지에 맞게 로드
        else numberNow = number;


        //차례로 키고 정보 기록
        if(numberNow != 0)
        {
            for (int i = 7 * page; i < numberNow; i++)
            {
                string[] infor = list[i].Name.Split('_');

                if (infor.Length == 2)
                {
                    roomObject[i % 7].SetActive(true);

                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    switch (Convert.ToInt32(infor[1]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else if (infor.Length == 3) //Random
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(4).gameObject.SetActive(true);
                    password[i % 7] = UnityEngine.Random.Range(10000, 99999);

                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (Convert.ToInt32(infor[2]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else //Custom Private
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(4).gameObject.SetActive(true);

                    password[i % 7] = Convert.ToInt32(infor[2]);
                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (Convert.ToInt32(infor[3]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
            }
        }
    }
    public void NextPage()
    {
        page++;

        for (int i = 0; i < 7; i++) roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
        //모두 끄기
        for (int i = 0; i < 7; i++) roomObject[i].SetActive(false);

        list = network.GetRoomList();
        number = list.Length;
        roomInfo_Number.text = "(" + number + ")";
        //페이지에 따른 갯수이상이면 최대 페이지 만큼 로드
        if (number > 7 * (page + 1)) numberNow = 7 * (page + 1);
        //그 페이지를 채우지 못하만큼 작아진 경우 모드 끄기
        else if (number <= 6 * page) numberNow = 0;
        //아니면 현재 페이지에 맞게 로드
        else numberNow = number;


        //차례로 키고 정보 기록
        if (numberNow != 0)
        {
            for (int i = 7 * page; i < numberNow; i++)
            {
                string[] infor = list[i].Name.Split('_');

                if (infor.Length == 2)
                {
                    roomObject[i % 7].SetActive(true);

                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    switch (Convert.ToInt32(infor[1]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else if (infor.Length == 3) //Random
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(4).gameObject.SetActive(true);
                    password[i % 7] = UnityEngine.Random.Range(10000, 99999);

                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (Convert.ToInt32(infor[2]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else //Custom Private
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(4).gameObject.SetActive(true);

                    password[i % 7] = Convert.ToInt32(infor[2]);
                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (Convert.ToInt32(infor[3]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
            }
        }
    }
    public void PrevPage()
    {
        page--;

        for (int i = 0; i < 7; i++) roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
        //모두 끄기
        for (int i = 0; i < 7; i++) roomObject[i].SetActive(false);

        list = network.GetRoomList();
        number = list.Length;
        roomInfo_Number.text = "(" + number + ")";
        //페이지에 따른 갯수이상이면 최대 페이지 만큼 로드
        if (number > 7 * (page + 1)) numberNow = 7 * (page + 1);
        //그 페이지를 채우지 못하만큼 작아진 경우 모드 끄기
        else if (number <= 6 * page) numberNow = 0;
        //아니면 현재 페이지에 맞게 로드
        else numberNow = number;


        //차례로 키고 정보 기록
        if (numberNow != 0)
        {
            for (int i = 7 * page; i < numberNow; i++)
            {
                string[] infor = list[i].Name.Split('_');

                if (infor.Length == 2)
                {
                    roomObject[i % 7].SetActive(true);

                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    switch (Convert.ToInt32(infor[1]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else if (infor.Length == 3) //Random
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(4).gameObject.SetActive(true);
                    password[i % 7] = UnityEngine.Random.Range(10000, 99999);

                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (Convert.ToInt32(infor[2]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
                else //Custom Private
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(4).gameObject.SetActive(true);

                    password[i % 7] = Convert.ToInt32(infor[2]);
                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (Convert.ToInt32(infor[3]))
                    {
                        case 0:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Desert";
                            break;
                        case 1:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Snowy Field";
                            break;
                        case 2:
                            roomObject[i % 7].transform.GetChild(3).GetComponent<Text>().text = "Plains";
                            break;
                    }
                    //상태
                    if (list[i].IsOpen == true) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Waiting";
                    else if (list[i].IsOpen == false) roomObject[i % 7].transform.GetChild(1).GetComponent<Text>().text = "Playing";
                    //인원수
                    roomObject[i % 7].transform.GetChild(2).GetComponent<Text>().text = list[i].PlayerCount + "/2";
                }
            }
        }
    }


    private int EnterNum;
    public void EnterPrivate()
    {
        if (password[EnterNum] == Convert.ToInt32(enterPassword.text))
        {
            panel_enterPrivate.SetActive(false);
            Blocking.SetActive(false);
            network.JoinCustom(roomObject[0].transform.GetChild(0).GetComponent<Text>().text);
        }
        else
        {
            StartCoroutine(WrongPasswordAnim());
        }
    }
    IEnumerator WrongPasswordAnim()
    {
        yield return new WaitForSeconds(1);
        panel_enterPrivate.SetActive(false);
        Blocking.SetActive(false);
    }
    // 3개 - infor[2] //맵 2개 - infor[1] 맵
    public void Room0Over()
    {
        overButton4 = roomObject[0];
        roomObject[0].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[0].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[0].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[0].transform.GetChild(1).GetComponent<Text>().text;
        //인원 ??
    }
    public void Room0Join()
    {
        if(roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 0;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[0].transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Room1Over()
    {
        overButton4 = roomObject[1];
        roomObject[1].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[1].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[1].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[1].transform.GetChild(1).GetComponent<Text>().text;
    }
    public void Room1Join()
    {
        if (roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 1;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[1].transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Room2Over()
    {
        overButton4 = roomObject[2];
        roomObject[2].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[2].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[2].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[2].transform.GetChild(1).GetComponent<Text>().text;
    }
    public void Room2Join()
    {
        if (roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 2;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[2].transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Room3Over()
    {
        overButton4 = roomObject[3];
        roomObject[3].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[3].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[3].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[3].transform.GetChild(1).GetComponent<Text>().text;
    }
    public void Room3Join()
    {
        if (roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 3;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[3].transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Room4Over()
    {
        overButton4 = roomObject[4];
        roomObject[4].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[4].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[4].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[4].transform.GetChild(1).GetComponent<Text>().text;
    }
    public void Room4Join()
    {
        if (roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 4;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[4].transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Room5Over()
    {
        overButton4 = roomObject[5];
        roomObject[5].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[5].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[5].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[5].transform.GetChild(1).GetComponent<Text>().text;
    }
    public void Room5Join()
    {
        if (roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 5;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[5].transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Room6Over()
    {
        overButton4 = roomObject[6];
        roomObject[6].GetComponent<Outline>().enabled = true;

        roomInfo_Title.text = roomObject[6].transform.GetChild(0).GetComponent<Text>().text;
        roomInfo_Map.text = roomObject[6].transform.GetChild(3).GetComponent<Text>().text;
        roomInfo_State.text = roomObject[6].transform.GetChild(1).GetComponent<Text>().text;
    }
    public void Room6Join()
    {
        if (roomObject[0].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 6;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(roomObject[6].transform.GetChild(0).GetComponent<Text>().text);
    }




    /// <summary>
    /// 
    /// </summary>
    //Character Scene Function
    public void ToChar()
    {
        inLobby = 3;
        Camera.main.transform.DOMoveX(420, 0.7f).SetEase(Ease.OutQuart);

        //current Scene Fx On
        Fx_now3.SetActive(true);
        overText3.GetComponent<Text>().color = Color.white;

        //Last Scene Fx Off
        Fx_last3.SetActive(false);
        Fx_last3 = Fx_Char;
        LastText3.GetComponent<Text>().color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        LastText3 = Text_Char;
    }

    public void OverExit2()
    {

    }

    public void Team1Over()
    {

    }

    public void Team2Over()
    {

    }

    public void CommonOver()
    {

    }
}
