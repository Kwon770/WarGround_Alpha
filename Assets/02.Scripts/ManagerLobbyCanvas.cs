using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLobbyCanvas : MonoBehaviour {

    //System
    [SerializeField] private GameObject Blocking;

    [SerializeField] private GameObject Fx_Home;
    [SerializeField] private GameObject Fx_Play;
    [SerializeField] private GameObject Fx_Char;

    [SerializeField] private GameObject Fx_Time2;

    [SerializeField] private GameObject UI;


    private GameObject Fx_now3;

    private GameObject Fx_last3;

    private int inLobby = 1;

    //Lobby Scene UI
    [SerializeField] private Text PlayedType;

    [SerializeField] private GameObject Button_Quick;
    [SerializeField] private GameObject Button_Quick_Play;

    [SerializeField] private GameObject Panel_Option;
    [SerializeField] private GameObject Button_Option;
    [SerializeField] private GameObject Button_Quit;

    [SerializeField] private Text Resolution;

    private bool Option = false;
    private int resoultion_w = 1920;
    private int resoultion_h = 1080;
    private bool mode = true;

    //outline button
    private GameObject overButton4;
    //icon
    private GameObject overButton5;

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
    [SerializeField] private Text TimeS1;
    [SerializeField] private Text TimeM1;

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

    string [] password = new string[10];

    [SerializeField] private GameObject BackGround_AfterCustom;

    [SerializeField] private GameObject panel_CreateRoom;
    [SerializeField] private GameObject panel_enterPrivate;

    [SerializeField] private GameObject Button_Created;
    [SerializeField] private GameObject Button_CreatedRoom;
    [SerializeField] private GameObject Button_NextPage;
    [SerializeField] private GameObject Button_PrevPage;
    [SerializeField] private GameObject Button_Refresh;

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
                if(Option)
                {
                    Panel_Option.SetActive(false);
                    Option = false;
                }
                else
                {
                    Panel_Option.SetActive(true);
                    Option = true;
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

                //Last Scene Fx Off
                Fx_last3.SetActive(false);
                Fx_last3 = Fx_Home;

                StartCoroutine(Cor1());
            }
        }
    }
    private void Start()
    {
        network = GetComponent<ManagerLobbyNetwork>();

        Fx_last3 = Fx_Home;
        if (PlayerPrefs.HasKey("PlayedType"))
        {
            QuickType = PlayerPrefs.GetInt("PlayedType");
            if (QuickType == 1) PlayedType.text = "Random Match";
            else if (QuickType == 2) PlayedType.text = "Custom Match";
            else if (QuickType == 3) PlayedType.text = "Tutorial";
        }

        if (PlayerPrefs.GetInt("mode") == 0) mode = false;
        else mode = true;
        Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), mode);
        Resolution.text = PlayerPrefs.GetInt("width") + "*" + PlayerPrefs.GetInt("height");
    }


    /// <summary>
    /// 
    /// </summary>
    //System(Common) Funtion
    public void ToLobby()
    {
        //current Scene Fx On
        Fx_now3.SetActive(true);

        //Last Scene Fx Off
        Fx_last3.SetActive(false);
        Fx_last3 = Fx_Home;

        StartCoroutine(Cor1());
    }
    IEnumerator Cor1()
    {
        Camera.main.transform.DOMoveX(0, 0.7f).SetEase(Ease.OutQuart);
        UI.transform.DOMoveX(960, 0.7f).SetEase(Ease.OutQuart);
        inLobby = 1;
        yield return new WaitForSeconds(0.3f);
    }

    public void OverExit3()
    {
        if ((Fx_now3 == Fx_Home && inLobby != 1) || (Fx_now3 == Fx_Play && (inLobby != 2 && inLobby != 4)) || (Fx_now3 == Fx_Char && inLobby != 3))
        {
            Fx_now3.SetActive(false);
        }
    }

    public void HomeOver()
    {
        Fx_now3 = Fx_Home;
        Fx_now3.SetActive(true);
    }
    public void PlayOver()
    {
        Fx_now3 = Fx_Play;
        Fx_now3.SetActive(true);
    }
    public void CharOver()
    {
        Fx_now3 = Fx_Char;
        Fx_now3.SetActive(true);
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

            Button_Quick.SetActive(false);
            Button_Quick_Play.SetActive(true);

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


    public void OverExit5()
    {
        overButton5.GetComponent<Image>().color = Color.white;
    }

    public void Option1Over()
    {
        overButton5 = Button_Option;
        overButton5.GetComponent<Image>().color = Color.black;
    }
    public void Option1Pressed()
    {
        Panel_Option.SetActive(true);
        Option = true;
    }
    public void OptionQuit()
    {
        Panel_Option.SetActive(false);
        Option = false;
    }

    public void Option_HD()
    {
        if (PlayerPrefs.GetInt("mode") == 0) mode = false;
        else mode = true;
        Screen.SetResolution(1280, 720, mode);
        resoultion_w = 1280;
        resoultion_h = 720;
        PlayerPrefs.SetInt("width", 1280);
        PlayerPrefs.SetInt("height", 720);

        Resolution.text = PlayerPrefs.GetInt("width") + "*" + PlayerPrefs.GetInt("height");
    }
    public void Option_FHD()
    {
        if (PlayerPrefs.GetInt("mode") == 0) mode = false;
        else mode = true;
        Screen.SetResolution(1920, 1080, mode);
        resoultion_w = 1920;
        resoultion_h = 1080;
        PlayerPrefs.SetInt("width", 1920);
        PlayerPrefs.SetInt("height", 1080);

        Resolution.text = PlayerPrefs.GetInt("width") + "*" + PlayerPrefs.GetInt("height");
    }
    public void Option_QHD()
    {
        if (PlayerPrefs.GetInt("mode") == 0) mode = false;
        else mode = true;
        Screen.SetResolution(2560, 1440, mode);
        resoultion_w = 2560;
        resoultion_h = 1440;
        PlayerPrefs.SetInt("width", 2560);
        PlayerPrefs.SetInt("height", 1440);

        Resolution.text = PlayerPrefs.GetInt("width") + "*" + PlayerPrefs.GetInt("height");
    }
    public void Option_Window()
    {
        Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), false);
        mode = false;
        PlayerPrefs.SetInt("mode", 0);
    }
    public void Option_Full()
    {
        Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), true);
        mode = true;
        PlayerPrefs.SetInt("mode", 1);
    }
    

    public void QuitOver()
    {
        overButton5 = Button_Quit;
        overButton5.GetComponent<Image>().color = Color.black;
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
        UI.transform.DOMoveX(-1117, 0.7f).SetEase(Ease.OutQuart);

        //current Scene Fx On
        Fx_Play.SetActive(true);

        //Last Scene Fx Off
        Fx_last3.SetActive(false);
        Fx_last3 = Fx_Play;
    }

    public void OverExit()
    {
        overInfor.SetActive(false);
        infor_Null.SetActive(true);
        Fx_now.SetActive(false);
    }

    public void RandomOver()
    {
        infor_Null.SetActive(false);
        infor_Random.SetActive(true);
        overInfor = infor_Random;
        overButton = Button_Random;
        Fx_Random.SetActive(true);
        Fx_now = Fx_Random;
    }
    public void RandomPressed()
    {
        OverExit();

        PlayerPrefs.SetInt("PlayedType", 1);
        PlayedType.text = "Random";
        QuickType = 1;

        Button_Quick.SetActive(false);
        Button_Quick_Play.SetActive(true);

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
                    TimeM1.text = "0" + matchingTimeM;
                }
                else
                {
                    TimeM.text = "" + matchingTimeM;
                    TimeM1.text = "" + matchingTimeM;
                }
            }
            else
            {
                matchingTimeS++;

                if (matchingTimeS < 10)
                {
                    TimeS.text = "0" + matchingTimeS;
                    TimeS1.text = "0" + matchingTimeS;
                }
                else
                {
                    TimeS.text = "" + matchingTimeS;
                    TimeS1.text = "" + matchingTimeS;
                }
            }

            yield return new WaitForSeconds(1);
            if (canceled)
            {
                canceled = false;
                matchingTimeS = 0;
                TimeS.text = "00";
                TimeS1.text = "00";
                matchingTimeM = 0;
                TimeM.text = "00";
                TimeM1.text = "00";

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

        Button_Quick.SetActive(true);
        Button_Quick_Play.SetActive(false);

        BackGround_AfterRandom.SetActive(false);
        BackGround_Before.SetActive(true);
    }

    public void CustomOver()
    {
        infor_Null.SetActive(false);
        infor_Custom.SetActive(true);
        overInfor = infor_Custom;
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
    
    public void CreatedRoomOver()
    {
        overButton5 = Button_CreatedRoom;
        overButton5.GetComponent<Image>().color = Color.black;
    }
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
        for (int i = 0; i < 7; i++)
        {
            roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
            roomObject[i].transform.GetChild(5).gameObject.SetActive(false);
            roomObject[i].SetActive(false);
        }

        if (numberNow != 0)
        {
            //차례로 키고 정보 기록
            for (int i = 0; i < numberNow; i++)
            {
                string[] infor = list[i].Name.Split('_');

                if (infor.Length == 2) //Custom
                {
                    roomObject[i].SetActive(true);
                    roomObject[i].transform.GetChild(5).gameObject.SetActive(true);

                    //제목
                    roomObject[i].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    //맵
                    switch(int.Parse(infor[1]))
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
                    password[i] = UnityEngine.Random.Range(10000, 99999) + "";

                    //제목
                    roomObject[i].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (int.Parse(infor[2]))
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

                    password[i] = infor[2];
                    //제목
                    roomObject[i].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (int.Parse(infor[3]))
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


    public void RefreshOver()
    {
        overButton5 = Button_Refresh;
        overButton5.GetComponent<Image>().color = Color.black;
    }
    public void Refresh()
    {
        //모두 끄기
        for (int i = 0; i < 7; i++)
        {
            roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
            roomObject[i].transform.GetChild(5).gameObject.SetActive(false);
            roomObject[i].SetActive(false);
        }

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
                //

                if (infor.Length == 2)
                {
                    roomObject[i % 7].SetActive(true);
                    roomObject[i % 7].transform.GetChild(5).gameObject.SetActive(true);

                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    switch (int.Parse(infor[1]))
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
                    password[i % 7] = UnityEngine.Random.Range(10000, 99999) + "";

                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (int.Parse(infor[2]))
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

                    password[i % 7] = infor[2];
                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (int.Parse(infor[3]))
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

    public void NextPageOver()
    {
        overButton5 = Button_NextPage;
        overButton5.GetComponent<Image>().color = Color.black;
    }
    public void NextPage()
    {
        page++;

        //모두 끄기
        for (int i = 0; i < 7; i++)
        {
            roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
            roomObject[i].transform.GetChild(5).gameObject.SetActive(false);
            roomObject[i].SetActive(false);
        }

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
                    roomObject[i % 7].transform.GetChild(5).gameObject.SetActive(true);

                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    switch (int.Parse(infor[1]))
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
                    password[i % 7] = UnityEngine.Random.Range(10000, 99999) + "";

                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (int.Parse(infor[2]))
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

                    password[i % 7] = infor[2];
                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (int.Parse(infor[3]))
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

    public void PrevPageOver()
    {
        overButton5 = Button_PrevPage;
        overButton5.GetComponent<Image>().color = Color.black;
    }
    public void PrevPage()
    {
        page--;

        //모두 끄기
        for (int i = 0; i < 7; i++)
        {
            roomObject[i].transform.GetChild(4).gameObject.SetActive(false);
            roomObject[i].transform.GetChild(5).gameObject.SetActive(false);
            roomObject[i].SetActive(false);
        }

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
                    roomObject[i % 7].transform.GetChild(5).gameObject.SetActive(true);

                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];
                    switch (int.Parse(infor[1]))
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
                    password[i % 7] = UnityEngine.Random.Range(10000, 99999) + "";

                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0] + infor[1];
                    //맵
                    switch (int.Parse(infor[2]))
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

                    password[i % 7] = infor[2];
                    //제목
                    roomObject[i % 7].transform.GetChild(0).GetComponent<Text>().text = infor[0];

                    //맵
                    switch (int.Parse(infor[3]))
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
        if (password[EnterNum] == enterPassword.text)
        {
            panel_enterPrivate.SetActive(false);
            Blocking.SetActive(false);
            network.JoinCustom(list[EnterNum].Name);
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
        else network.JoinCustom(list[0].Name);
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
        if (roomObject[1].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 1;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(list[1].Name);
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
        if (roomObject[2].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 2;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(list[2].Name);
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
        if (roomObject[3].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 3;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(list[3].Name);
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
        if (roomObject[4].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 4;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(list[4].Name);
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
        if (roomObject[5].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 5;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(list[5].Name);
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
        if (roomObject[6].transform.GetChild(4).gameObject.activeSelf)
        {
            EnterNum = 6;
            Blocking.SetActive(true);
            panel_enterPrivate.SetActive(true);
            inLobby = 11;
        }
        else network.JoinCustom(list[6].Name);
    }




    /// <summary>
    /// 
    /// </summary>
    //Character Scene Function
    public void ToChar()
    {
        inLobby = 3;
        Camera.main.transform.DOMoveX(420, 0.7f).SetEase(Ease.OutQuart);
        UI.transform.DOMoveX(-3120, 0.7f).SetEase(Ease.OutQuart);

        //current Scene Fx On
        Fx_now3.SetActive(true);

        //Last Scene Fx Off
        Fx_last3.SetActive(false);
        Fx_last3 = Fx_Char;
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
