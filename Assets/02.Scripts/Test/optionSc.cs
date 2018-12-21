using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionSc : MonoBehaviour {

    [SerializeField] SoundManager soundManager;
    [SerializeField] Slider fxVolume;
    [SerializeField] Slider bgmVolume;
    [SerializeField] Slider masterVolume;

    [SerializeField] Dropdown screenSize;
    [SerializeField] int screenWidth;
    [SerializeField] int screenHeight;
    [SerializeField] Toggle isFullScreenToggle;
    [SerializeField] bool isFullScreen;
    [SerializeField] bool isChangeFullScreen; // 16 : 9 가 아닐경우 전체화면 지원하지 않습니다.
    [SerializeField] string isFullScreenString;


    private void Awake()
    {
        if((Screen.width / 16) * 9 == Screen.height)    // 16:9 비율이 아닐 경우에는 FULLSCREEN 활성화를 막습니다.
        {
            isChangeFullScreen = true;
            isFullScreenString = PlayerPrefs.GetString("isFullScreenSave", isFullScreen.ToString());
            isFullScreen = System.Convert.ToBoolean(isFullScreenString);
            Debug.Log(isFullScreen);
        }
        else
        {
            isChangeFullScreen = false;
            isFullScreen = false;
        }

        screenWidth = PlayerPrefs.GetInt("screenWidthSave", screenWidth);
        screenHeight = PlayerPrefs.GetInt("screenHeightSave", screenHeight);
        Debug.Log(screenWidth + " + " + screenHeight);

        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        setFirstUi();

        fxVolume.value = soundManager.fxVolume;  
    }

    private void Update()
    {
        sliderFxVolume();
        //sliderBgmVolume();
        //sliderMaserVolume();

        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void sliderFxVolume() // FX 볼륨 슬라이더, 사운드 매니저와 값 동기화
    {
        soundManager.fxVolume = fxVolume.value; 
    }

    /*public void sliderBgmVolume()   // BGM 볼륨 슬라이더, 사운드 매니저와 값 동기화, SoundManager에 해당 값을 만든 후 주석 해제해주세요
    {
        soundManager.bgmVolume = bgmVolume.value;
    }*/

    /*public void sliderMaserVolume() // 마스터 볼륨 슬라이더, 사운드 매니저와 값 동기화, SoundManager에 해당 값을 만든 후 주석 해제해주세요
    {
        soundManager.masterVolume = masterVolume.value;
    }*/

    public void setFirstUi()
    {
        if(screenWidth == 1920)
        {
            screenSize.value = 3;
        }
        else if (screenWidth == 1600)
        {
            screenSize.value = 2;
        }
        else if (screenWidth == 1280)
        {
            screenSize.value = 1;
        }
        else
        {
            screenSize.value = 0;
        }

        if(isFullScreen == true)
        {
            isFullScreenToggle.isOn = true;
        }
        else
        {
            isFullScreenToggle.isOn = false;
        }
    }   // 저장되있는 값을 불러와 그에 맞게 UI를 대응합니다.

    public void setDropdownScreenSize()
    {
        if (screenSize.value == 0)
        {
            screenWidth = 960;
            screenHeight = 540;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }
        if (screenSize.value == 1)
        {
            screenWidth = 1280;
            screenHeight = 720;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }
        if (screenSize.value == 2)
        {
            screenWidth = 1600;
            screenHeight = 900;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }
        if (screenSize.value == 3)
        {
            screenWidth = 1920;
            screenHeight = 1080;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }

    }

    public void setIsFullscreen()
    {
      if(isFullScreenToggle.isOn == false && isChangeFullScreen == true)
        {
            isFullScreen = false;
            PlayerPrefs.SetString("isFullScreenSave", isFullScreen.ToString());
            PlayerPrefs.Save();
        }
      else if (isFullScreenToggle.isOn == true && isChangeFullScreen == true)
        {
            isFullScreen = true;
            PlayerPrefs.SetString("isFullScreenSave", isFullScreen.ToString());
            PlayerPrefs.Save();
        }
      else
        {
            isFullScreen = false;
        }
    }

    void saveScreenSize()
    {
        PlayerPrefs.SetInt("screenWidthSave", screenWidth);
        PlayerPrefs.SetInt("screenHeightSave", screenHeight);
        PlayerPrefs.Save();
    }
}
