using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    [SerializeField] public static float fxVolume;
    [SerializeField] public static float bgmVolume;
    [SerializeField] public static float masterVolume;

    [SerializeField] public AudioSource fxSource;
    [SerializeField] public AudioSource bgmSource;

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
        if (SoundManager.soundmanager != null)
        {
            Destroy(SoundManager.soundmanager.gameObject);
        }
        soundmanager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void lobbyBGM(bool playMusic)
    {
        if(playMusic == true)
        {
            Debug.Log(bgmVolume + " " + masterVolume);
            StartCoroutine(soundFadeIn(bgmSource));
            bgmSource.clip = LobbyBGM;
            //SoundManager.soundmanager.bgmSource.volume = bgmVolume.value * masterVolume.value *0.5f;
            bgmSource.volume = bgmVolume * masterVolume * 0.5f;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }

    public void planeBGM(bool playMusic)
    {
        if (playMusic == true)
        {
            bgmSource.clip = PlaneBGM;
            bgmSource.volume = bgmVolume * masterVolume * 0.5f;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }

    // 여기부턴 효과음

    public void clickLobbyButton()
    {
        fxSource.PlayOneShot(ClickLobbyButton, fxVolume * masterVolume);
    }
    public void clickBackLobbyButton()
    {
        fxSource.PlayOneShot(ClickBackLobbyButton, fxVolume * masterVolume);
    }
    public void matchStart()
    {
        fxSource.PlayOneShot(MatchStart, fxVolume * masterVolume);
    }
    public void clickIngameButton()
    {
        fxSource.PlayOneShot(ClickIngameButton, fxVolume * masterVolume);
    }
    public void touchIngameButton()
    {
        fxSource.PlayOneShot(TouchIngameButton, fxVolume * masterVolume);
    }
    public void clickTurnButton()
    {
        fxSource.PlayOneShot(ClickTurnButton, fxVolume * masterVolume);
    }
    public void touchBitiniumBar()
    {
        fxSource.PlayOneShot(TouchBitiniumBar, fxVolume * masterVolume);
    }
    public void endGame()
    {
        fxSource.PlayOneShot(EndGame, fxVolume * masterVolume);
    }
    public void planeFootStep()
    {
        fxSource.PlayOneShot(PlaneFootStep, fxVolume * masterVolume);
    }
    public void swingWeapon()
    {
        fxSource.PlayOneShot(SwingWeapon, fxVolume * masterVolume);
    }
    public void shortSword()
    {
        fxSource.PlayOneShot(ShortSword, fxVolume * masterVolume);
    }
    public void smallSword()
    {
        fxSource.PlayOneShot(SmallSword, fxVolume * masterVolume);
    }
    public void bigSword()
    {
        fxSource.PlayOneShot(BigSword, fxVolume * masterVolume);
    }
    public void archerAttack()
    {
        fxSource.PlayOneShot(ArcherAttack, fxVolume * masterVolume);
    }
    public void magicAttack()
    {
        fxSource.PlayOneShot(MagicAttack, fxVolume * masterVolume);
    }
    public void reloadGun()
    {
        fxSource.PlayOneShot(ReloadGun, fxVolume * masterVolume);
    }
    public void shootGun()
    {
        fxSource.PlayOneShot(ShootGun, fxVolume * masterVolume);
    }
    public void shootLongRifle()
    {
        fxSource.PlayOneShot(ShootLongRifle, fxVolume * masterVolume);
    }
    public void hammer()
    {
        fxSource.PlayOneShot(Hammer, fxVolume * masterVolume);
    }
    public void heal()
    {
        fxSource.PlayOneShot(Heal, fxVolume * masterVolume);
    }
    public void spear()
    {
        fxSource.PlayOneShot(Spear, fxVolume * masterVolume);
    }

    IEnumerator soundFadeIn(AudioSource sound)
    {
        Debug.Log("a");
        float time = 0;
        sound.volume = 0f;
        while (time <= 1f)
        {
            sound.volume = time * bgmVolume * masterVolume * 0.5f;
            time += Time.deltaTime;
            yield return null;
        }
    }

}