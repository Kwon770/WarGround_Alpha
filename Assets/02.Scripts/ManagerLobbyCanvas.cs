using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLobbyCanvas : MonoBehaviour {

    //System
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

    private GameObject overButton4;

    private int QuickType;
    //Match Scene UI
    //Button - Text
    //BeforeMatch
    [SerializeField] private GameObject Button_Back1;
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

    [SerializeField] private GameObject[] roomObject = new GameObject[6];
    RoomInfo[] list;

    [SerializeField] private GameObject BackGround_AfterCustom;





    //Character Scene UI
    [SerializeField] private GameObject Button_Back2;
    [SerializeField] private GameObject Button_team1;
    [SerializeField] private GameObject Button_team2;
    [SerializeField] private GameObject Button_Common;



    private ManagerLobbyNetwork Network;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inLobby == 1)
            {
                //옵션
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
        Network = GetComponent<ManagerLobbyNetwork>();

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
        if ((Fx_now3 == Fx_Home && inLobby != 1) || (Fx_now3 == Fx_Play && inLobby != 2) || (Fx_now3 == Fx_Char && inLobby != 3))
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
        OverExit4();

        overButton4 = Button_Quick;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void QuickPressed()
    {
        if (QuickType == 1)
        {
            ToMatch();
            RandomPressed();
        }
        else if (QuickType == 2)
        {
            ToMatch();
            CustomPressed();
        }
        else if (QuickType == 3)
        {
            // 튜토리얼 짜지는거 보고 코딩
        }
    }


    public void CreditOver()
    {
        OverExit4();

        overButton4 = Button_Credit;
        overButton4.GetComponent<Outline>().enabled = true;
    }






    //Match Scene Function
    public void ToMatch()
    {
        inLobby = 2;
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
        OverExit();

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
        Fx_BackGround_Match.SetActive(true);

        BackGround_AfterRandom.SetActive(true);
        BackGround_Before.SetActive(false);

        StartCoroutine(MatchingAnim());
        StartCoroutine(MatchingAnim2());

        Network.JoinRandom();
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

        canceled = true;
        Fx_BackGround_Match.SetActive(false);

        BackGround_AfterRandom.SetActive(false);
        BackGround_Before.SetActive(true);
    }

    public void CustomOver()
    {
        OverExit();

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

        RoomListPrepare();
    }

    public void TutorialOver()
    {
        OverExit();

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
    }
    
    //AfterCustom

    //public void RoomListPrepare()
    //{
    //    list = Network.Getroomlist();
    //    list[0].Name; //name_mode
    //    list[0].MaxPlayers;
    //    list[0].PlayerCount;
    //    list[0].IsOpen; // true면 waiting flase playing
    //}






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

    public void BackOver2()
    {
        Fx_now3 = Fx_Home;
        overText3 = Text_Home;

        overButton4 = Button_Back2;
        overButton4.GetComponent<Outline>().enabled = true;
    }
}
