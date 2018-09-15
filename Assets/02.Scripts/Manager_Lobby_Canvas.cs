using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_LobbyCanvas : MonoBehaviour {
    
    //System
    public GameObject scene_Play;
    public GameObject scene_Char;
    public GameObject scene_Lobby;

    public GameObject Text_Home;
    public GameObject Text_Play;
    public GameObject Text_Char;

    public GameObject Fx_Home;
    public GameObject Fx_Play;
    public GameObject Fx_Char;

    private GameObject Fx_now3;
    private GameObject overText3;

    private GameObject Fx_last3;
    private GameObject LastText3;

    private int inLobby = 1;

    //Lobby Scene UI
    public Text PlayedType;
    public GameObject Button_Quick;
    public GameObject Button_ToPlay2;
    public GameObject Button_Credit;

    private GameObject overButton4;

    //Match Scene UI
    //Button - Text
    public GameObject Button_Back1;
    public GameObject Button_Random;
    public GameObject Button_Custom;
    public GameObject Button_Tutorial;

    public GameObject infor_Custom;
    public GameObject infor_Random;
    public GameObject infor_Tutorial;
    public GameObject infor_Null;

    public GameObject Fx_Random;
    public GameObject Fx_Custom;
    public GameObject Fx_Tutorial;

    public GameObject BackGroun1;

    private GameObject overButton;
    private GameObject overInfor;
    private GameObject Fx_now;

    //Character Scene UI
    public GameObject Button_Back2;
    public GameObject Button_team1;
    public GameObject Button_team2;
    public GameObject Button_Common;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (inLobby == 1)
            {

            }
            else
            {
                StartCoroutine(Cor1());
            }
        }
    }
    private void Start()
    {
        Fx_last3 = Fx_Home;
        LastText3 = Text_Home;
        if (PlayerPrefs.HasKey("PlayedType"))
        {
            PlayedType.text = PlayerPrefs.GetString("PlayedType");
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
        LastText3.GetComponent<Text>().color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
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
            overText3.GetComponent<Text>().color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        }
    }

    public void HomeOver()
    {
        Fx_now3 = Fx_Home;
        Fx_now3.SetActive(true);
        overText3 = Text_Home;
        overText3.GetComponent<Text>().color = Color.white;
    }
    public void PlayOver()
    {
        Fx_now3 = Fx_Play;
        Fx_now3.SetActive(true);
        overText3 = Text_Play;
        overText3.GetComponent<Text>().color = Color.white;
    }
    public void CharOver()
    {
        Fx_now3 = Fx_Char;
        Fx_now3.SetActive(true);
        overText3 = Text_Char;
        overText3.GetComponent<Text>().color = Color.white;
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
        overButton4 = Button_Quick;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void ToPlay2Over()
    {
        overButton4 = Button_ToPlay2;
        overButton4.GetComponent<Outline>().enabled = true;
    }
    public void CreditOver()
    {
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
        overButton.GetComponent<Text>().color = new Color(0.4823529f, 0.4823529f, 0.4823529f);
        Fx_now.SetActive(false);
    }

    public void RandomOver()
    {
        infor_Null.SetActive(false);
        infor_Random.SetActive(true);
        overInfor = infor_Random;
        Button_Random.GetComponent<Text>().color = Color.white;
        overButton = Button_Random;
        Fx_Random.SetActive(true);
        Fx_now = Fx_Random;
    }
    public void RandomPressed()
    {
        PlayerPrefs.SetString("PlayedType", "Random");
    }

    public void CustomOver()
    {
        infor_Null.SetActive(false);
        infor_Custom.SetActive(true);
        overInfor = infor_Custom;
        Button_Custom.GetComponent<Text>().color = Color.white;
        overButton = Button_Custom;
        Fx_Custom.SetActive(true);
        Fx_now = Fx_Custom;
    }
    public void CustomPressed()
    {
        PlayerPrefs.SetString("PlayedType", "Custom Match");
    }

    public void TutoraiOver()
    {
        infor_Null.SetActive(false);
        infor_Tutorial.SetActive(true);
        overInfor = infor_Tutorial;
        Button_Tutorial.GetComponent<Text>().color = Color.white;
        overButton = Button_Tutorial;
        Fx_Tutorial.SetActive(true);
        Fx_now = Fx_Tutorial;
    }
    
    public void BackOver1()
    {
        Fx_now3 = Fx_Home;
        overText3 = Text_Home;

        overButton4 = Button_Back1;
        overButton4.GetComponent<Outline>().enabled = true;

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

    public void BackOver2()
    {
        Fx_now3 = Fx_Home;
        overText3 = Text_Home;

        overButton4 = Button_Back2;
        overButton4.GetComponent<Outline>().enabled = true;
    }
}
