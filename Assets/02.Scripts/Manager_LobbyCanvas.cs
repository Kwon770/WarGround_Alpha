using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_LobbyCanvas : MonoBehaviour {
    
    //System
    public GameObject scene_Play;
    public GameObject scene_Lobby;

    private GameObject onScene;

    private bool inLobby = true;

    //Lobby Scene UI
    public Text PlayedTpye;

    //Match Scene UI
    public GameObject infor_Custom;
    public GameObject infor_Random;
    public GameObject infor_Tutorial;
    public GameObject infor_Null;

    private GameObject overNow;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (inLobby)
            {

            }
            else
            {
                Camera.main.transform.DOMoveX(0, 0.5f).SetEase(Ease.OutQuart);
                onScene.SetActive(false);
                inLobby = true;
            }
        }
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey("PlayedType"))
        {
            PlayedTpye.text = PlayerPrefs.GetString("PlayedType");
        }
    }


    //System(Common) Funtion

    //Lobby Scene Function
    public void ToLobby()
    {
        Camera.main.transform.DOMoveX(0, 1).SetEase(Ease.OutQuart);
        onScene.SetActive(false);
        inLobby = true;
    }

    //Match Scene Function
    public void LobbyToMatch()
    {
        Camera.main.transform.DOMoveX(213, 1).SetEase(Ease.OutQuart);
        onScene = scene_Play;
        onScene.SetActive(true);
        inLobby = false;
    }

    public void OverExit()
    {
        overNow.SetActive(false);
        infor_Null.SetActive(true);
    }

    public void RandomOver()
    {
        infor_Null.SetActive(false);
        infor_Random.SetActive(true);
        overNow = infor_Random;
    }
    public void RandomPressed()
    {
        PlayerPrefs.SetString("PlayedType", "Random");
    }

    public void CustomOver()
    {
        infor_Null.SetActive(false);
        infor_Custom.SetActive(true);
        overNow = infor_Custom;
    }
    public void CustomPressed()
    {
        PlayerPrefs.SetString("PlayedType", "Custom Match");
    }

    public void TutoraiOver()
    {
        infor_Null.SetActive(false);
        infor_Tutorial.SetActive(true);
        overNow = infor_Tutorial;
    }
}
