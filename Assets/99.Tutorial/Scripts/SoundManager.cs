using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public float fxVolume;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip LobbyBGM;
    [SerializeField] AudioClip PlaneBGM;
    //Lobby FX
    [SerializeField] AudioClip ClickLobbyButton;
    [SerializeField] AudioClip ClickBackLobbyButton;
    [SerializeField] AudioClip MatchStart;
    //Plane FX
    [SerializeField] AudioClip ClickIngameButton;
    [SerializeField] AudioClip TouchIngameButton;
    [SerializeField] AudioClip ClickTurnButton;
    [SerializeField] AudioClip TouchBitiniumBar;
    [SerializeField] AudioClip EndGame;

    [SerializeField] AudioClip PlaneFootStep;

    [SerializeField] AudioClip SwingWeapon;
    [SerializeField] AudioClip ShortSword;
    [SerializeField] AudioClip SmallSword;
    [SerializeField] AudioClip BigSword;
    [SerializeField] AudioClip ArcherAttack;
    [SerializeField] AudioClip MagicAttack;
    [SerializeField] AudioClip ReloadGun;
    [SerializeField] AudioClip ShootGun;
    [SerializeField] AudioClip ShootLongRifle;
    [SerializeField] AudioClip Hammer;
    [SerializeField] AudioClip Spear;
    [SerializeField] AudioClip Heal;

    public static SoundManager soundmanager;

    private void Awake()
    {
        SoundManager.soundmanager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void lobbyBGM(bool playMusic)
    {
        
        if(playMusic == true)
        {
            soundFadeIn(audioSource);
            audioSource.clip = LobbyBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
       
    }

    public void planeBGM(bool playMusic)
    {

        if (playMusic == true)
        {
            audioSource.clip = PlaneBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

    }

    // 여기부턴 효과음

    public void clickLobbyButton()
    {
        audioSource.PlayOneShot(ClickLobbyButton, fxVolume);
    }
    public void clickBackLobbyButton()
    {
        audioSource.PlayOneShot(ClickBackLobbyButton, fxVolume);
    }
    public void matchStart()
    {
        audioSource.PlayOneShot(MatchStart, fxVolume);
    }
    public void clickIngameButton()
    {
        audioSource.PlayOneShot(ClickIngameButton, fxVolume);
    }
    public void touchIngameButton()
    {
        audioSource.PlayOneShot(TouchIngameButton, fxVolume);
    }
    public void clickTurnButton()
    {
        audioSource.PlayOneShot(ClickTurnButton, fxVolume);
    }
    public void touchBitiniumBar()
    {
        audioSource.PlayOneShot(TouchBitiniumBar, fxVolume);
    }
    public void endGame()
    {
        audioSource.PlayOneShot(EndGame, fxVolume);
    }
    public void planeFootStep()
    {
        audioSource.PlayOneShot(PlaneFootStep, fxVolume);
    }
    public void swingWeapon()
    {
        audioSource.PlayOneShot(SwingWeapon, fxVolume);
    }
    public void shortSword()
    {
        audioSource.PlayOneShot(ShortSword, fxVolume);
    }
    public void smallSword()
    {
        audioSource.PlayOneShot(SmallSword, fxVolume);
    }
    public void bigSword()
    {
        audioSource.PlayOneShot(BigSword, fxVolume);
    }
    public void archerAttack()
    {
        audioSource.PlayOneShot(ArcherAttack, fxVolume);
    }
    public void magicAttack()
    {
        audioSource.PlayOneShot(MagicAttack, fxVolume);
    }
    public void reloadGun()
    {
        audioSource.PlayOneShot(ReloadGun, fxVolume);
    }
    public void shootGun()
    {
        audioSource.PlayOneShot(ShootGun, fxVolume);
    }
    public void shootLongRifle()
    {
        audioSource.PlayOneShot(ShootLongRifle, fxVolume);
    }
    public void hammer()
    {
        audioSource.PlayOneShot(Hammer, fxVolume);
    }
    public void heal()
    {
        audioSource.PlayOneShot(Heal, fxVolume);
    }
    public void spear()
    {
        audioSource.PlayOneShot(Spear, fxVolume);
    }

    IEnumerator soundFadeIn(AudioSource sound)
    {
        float time = 0;
        sound.volume = 0f;
        while (time != 1)
        {
            sound.volume += time;
            time += Time.deltaTime;
            yield return null;
        }
    }

}
