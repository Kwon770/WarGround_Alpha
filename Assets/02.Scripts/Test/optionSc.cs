using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionSc : MonoBehaviour {
    
    [SerializeField] Slider fxVolume;
    [SerializeField] Slider bgmVolume;
    [SerializeField] Slider masterVolume;
    
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
        }
        else
        {
            isChangeFullScreen = false;
            isFullScreen = false;
        }

        if (PlayerPrefs.HasKey("screenWidthSave"))
        {
            screenWidth = PlayerPrefs.GetInt("screenWidthSave", screenWidth);
            screenHeight = PlayerPrefs.GetInt("screenHeightSave", screenHeight);
        }
        

        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        setFirstUi();

        if (PlayerPrefs.HasKey("fxVolumeSave"))
        {
            SoundManager.fxVolume = PlayerPrefs.GetFloat("fxVolumeSave", fxVolume.value);
        }
        else SoundManager.fxVolume = 1;
        if (PlayerPrefs.HasKey("bgmVolumeSave"))
        {
            SoundManager.bgmVolume = PlayerPrefs.GetFloat("bgmVolumeSave", bgmVolume.value);
        }
        else SoundManager.bgmVolume = 1;
        if (PlayerPrefs.HasKey("masterVolumeSave"))
        {
            SoundManager.masterVolume = PlayerPrefs.GetFloat("masterVolumeSave", masterVolume.value);
        }
        else SoundManager.masterVolume = 1;


        fxVolume.value = SoundManager.fxVolume;
        bgmVolume.value = SoundManager.bgmVolume;
        masterVolume.value = SoundManager.masterVolume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void sliderFxVolume() // FX 볼륨 슬라이더, 사운드 매니저와 값 동기화
    {
        SoundManager.fxVolume = fxVolume.value;
        PlayerPrefs.SetFloat("fxVolumeSave", fxVolume.value);
        PlayerPrefs.Save();
    }

    public void sliderBgmVolume()   // BGM 볼륨 슬라이더, 사운드 매니저와 값 동기화, SoundManager에 해당 값을 만든 후 주석 해제해주세요
    {
        SoundManager.bgmVolume = bgmVolume.value;
        SoundManager.soundmanager.bgmSource.volume = SoundManager.bgmVolume * SoundManager.masterVolume * 0.5f;
        PlayerPrefs.SetFloat("bgmVolumeSave", bgmVolume.value);
        PlayerPrefs.Save();
//        Debug.Log(SoundManager.soundmanager.bgmSource.volume);
    }

    public void sliderMaserVolume() // 마스터 볼륨 슬라이더, 사운드 매니저와 값 동기화, SoundManager에 해당 값을 만든 후 주석 해제해주세요
    {
        SoundManager.masterVolume = masterVolume.value;
        PlayerPrefs.SetFloat("masterVolumeSave", masterVolume.value);
        PlayerPrefs.Save();
        sliderBgmVolume();
    }

    // 저장되있는 값을 불러와 그에 맞게 UI를 대응합니다.
    public void setFirstUi()
    {
        if(isFullScreen == true)
        {
            isFullScreenToggle.isOn = true;
        }
        else
        {
            isFullScreenToggle.isOn = false;
        }
    }

    public void setScreenSize(int index)
    {
        if (index == 0)
        {
            screenWidth = 960;
            screenHeight = 540;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }
        if (index == 1)
        {
            screenWidth = 1280;
            screenHeight = 720;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }
        if (index == 2)
        {
            screenWidth = 1600;
            screenHeight = 900;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            saveScreenSize();
        }
        if (index == 3)
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
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            PlayerPrefs.Save();
        }
        else if (isFullScreenToggle.isOn == true && isChangeFullScreen == true)
        {
            isFullScreen = true;
            PlayerPrefs.SetString("isFullScreenSave", isFullScreen.ToString());
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
            PlayerPrefs.Save();
        }
        else
        {
            isFullScreen = false;
            Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        }
    }

    void saveScreenSize()
    {
        PlayerPrefs.SetInt("screenWidthSave", screenWidth);
        PlayerPrefs.SetInt("screenHeightSave", screenHeight);
        PlayerPrefs.Save();
    }
}
