using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {


    [SerializeField][Tooltip(" pluto, merica, kamiken, savage, brown, royal")]
    MenuControl[] EliteMenu;
    [SerializeField][Tooltip(" pluto, merica, kamiken, savage, brown, royal")]
    IconControl[] EliteIcon;

    [SerializeField] MenuControl PlayButton;
    [SerializeField] MenuControl CharacterButton;
    public GameObject Partan;
    public GameObject Cora;

    [SerializeField] Manager manager;

    [SerializeField] AnimationCurve curve;
    [SerializeField] float Speed;

    public GameObject Empty_Icons;


    public void Play()
    {
        Manager.instance.scene = (int)Manager.Menunum.Play;
        Empty_Icons.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            int num = i;
            EliteMenu[num].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>LobbyNetwork.instance.SetElite(num)));
        }

        StartCoroutine(MenuAnim());
    }
    public void Char()
    {
        Manager.instance.scene = (int)Manager.Menunum.Character;
        Empty_Icons.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            int num = i;
            EliteMenu[num].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => EliteIcon[num].SelectMove()));
        }

        Partan.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => CountrySelect(0)));
        Cora.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => CountrySelect(1)));

        StartCoroutine(CountryAnim());
    }


    //도감 내부 Return 코루틴은 Manager에
    // 0 Partan 1 Cora
    void CountrySelect(int index)
    {
        Manager.instance.scene = (int)Manager.Menunum.Country;
        Manager.instance.index = index;
        if (index == 0) Manager.instance.Countrys = Partan;
        else Manager.instance.Countrys = Cora;

        for (int i = 0 + (3*index); i < 3 + (3*index); i++)
        {
            EliteMenu[i].Move();
        }

        StartCoroutine(CountrySelectAnim(index));
    }
    // Manager에서 접근해서 애니메이션 진행
    public void CountryIconBack()
    {
        foreach (var button in EliteMenu)
        {
            button.Back();
        }
    }


    IEnumerator CountryAnim()
    {
        Manager.instance.corutine = true;

        PlayButton.Back();
        CharacterButton.Back();

        Partan.SetActive(true);
        Cora.SetActive(true);

        float time = 0;
        while (time <= 1)
        {
            Partan.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 215, curve.Evaluate(time)), 700);
            Cora.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 215, curve.Evaluate(time)), 700);
            time += Time.deltaTime * Speed;
            yield return null;
        }

        Manager.instance.corutine = false;
    }

    IEnumerator CountrySelectAnim(int index)
    {
        Manager.instance.corutine = true;

        float time = 0;
        if(index == 0)
        {
            while (time <= 1)
            {
                Partan.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(215, 600, curve.Evaluate(time)), 700);
                time += Time.deltaTime * Speed;
                yield return null;
            }
        }
        else
        {
            while (time <= 1)
            {
                Cora.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(215, 600, curve.Evaluate(time)), 700);
                time += Time.deltaTime * Speed;
                yield return null;
            }
        }

        Manager.instance.corutine = false;
    }

    public IEnumerator CountryReturnAnim()
    {
        Manager.instance.corutine = true;

        float time = 0;
        while (time <= 1)
        {
            Partan.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(215, 0, curve.Evaluate(time)), 700);
            Cora.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(215, 0, curve.Evaluate(time)), 700);
            time += Time.deltaTime * Speed;
            yield return null;
        }

        PlayButton.Move();
        CharacterButton.Move();

        Partan.SetActive(false);
        Cora.SetActive(false);

        Manager.instance.corutine = false;
    }

    public IEnumerator MenuAnim()
    {
        Manager.instance.corutine = true;

        PlayButton.Back();
        CharacterButton.Back();

        yield return new WaitForSeconds(0.3f);

        foreach (var button in EliteMenu)
        {
            button.Move();
        }

        Manager.instance.corutine = false;
    }
    public IEnumerator MenuReturnAnim()
    {
        Manager.instance.corutine = true;

        foreach (var button in EliteMenu)
        {
            button.Back();
        }

        yield return new WaitForSeconds(0.3f);

        PlayButton.Move();
        CharacterButton.Move();

        Manager.instance.corutine = false;
    }
}
