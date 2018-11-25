using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip lobbyBGM;
    [SerializeField] AudioClip inGameBGM;

    [SerializeField] AudioClip shootArrow;
    [SerializeField] AudioClip clickFirst;
    [SerializeField] AudioClip clickSecond;
    [SerializeField] AudioClip footStep;
    [SerializeField] AudioClip shootGunFirst;
    [SerializeField] AudioClip shootGunSecond;
    [SerializeField] AudioClip reloadGun;
    [SerializeField] AudioClip matchClick;
    [SerializeField] AudioClip matchCatch;
    [SerializeField] AudioClip useBitinium;
    [SerializeField] AudioClip drawSwordFirst;
    [SerializeField] AudioClip drawSwordSecond;
    [SerializeField] AudioClip slashSwordFirst;
    [SerializeField] AudioClip slashSwordSecond;

    public void PlayLobbyBGM(bool playMusic)
    {
        
        if(playMusic == true)
        {
            audioSource.clip = lobbyBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
       
    }

    public void PlayInGameBGM(bool playMusic)
    {

        if (playMusic == true)
        {
            audioSource.clip = inGameBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

    }

    public void ShootArrow()
    {
        audioSource.PlayOneShot(shootArrow);
    }

    public void ClickFirst()
    {
        audioSource.PlayOneShot(clickFirst);
    }

    public void ClickSecond()
    {
        audioSource.PlayOneShot(clickSecond);
    }

    public void FootStep()
    {
        audioSource.PlayOneShot(footStep);
    }

    public void ShootGunFirst()
    {
        audioSource.PlayOneShot(shootGunFirst);
    }

    public void ShootGunSecond()
    {
        audioSource.PlayOneShot(shootGunSecond);
    }

    public void ReloadGun()
    {
        audioSource.PlayOneShot(reloadGun);
    }

    public void MatchClick()
    {
        audioSource.PlayOneShot(matchClick);
    }

    public void MatchCatch()
    {
        audioSource.PlayOneShot(matchCatch);
    }

    public void UseBitinium()
    {
        audioSource.PlayOneShot(useBitinium);
    }

    public void DrawSwordFirst()
    {
        audioSource.PlayOneShot(drawSwordFirst);
    }

    public void DrawSwordSecond()
    {
        audioSource.PlayOneShot(drawSwordSecond);
    }

    public void SlashSwordFirst()
    {
        audioSource.PlayOneShot(slashSwordFirst);
    }

    public void SlashSwordSecond()
    {
        audioSource.PlayOneShot(slashSwordSecond);
    }

}
