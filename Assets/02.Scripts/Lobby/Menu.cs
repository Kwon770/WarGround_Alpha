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
    public GameObject Random;
    public GameObject Custom;
    public GameObject Create;
    public GameObject Join;

    public GameObject RandomPos;
    public GameObject PartanPos;
    public GameObject CoraPos;

    [SerializeField] Manager manager;

    [SerializeField] AnimationCurve curve;
    [SerializeField][Tooltip("Fast Speed")]
    float Speed;
    [SerializeField][Tooltip("Slow Speed")]
    float Speed1;

    public GameObject Empty_Icons;


    public void Play()
    {
        Manager.instance.scene = (int)Manager.Menunum.Play;
        Empty_Icons.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            int num = i;
            EliteMenu[num].button.onClick.RemoveAllListeners();
            EliteMenu[num].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => LobbyNetwork.instance.SetElite(num)));
            EliteMenu[num].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => IconSelectAnim1()));
        }

        StartCoroutine(MenuAnim());
    }
    public void Char()
    {
        Manager.instance.scene = (int)Manager.Menunum.Character;
        Empty_Icons.SetActive(true);

        Partan.GetComponent<Button>().onClick.RemoveAllListeners();
        Partan.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => CountrySelect(0)));
        Cora.GetComponent<Button>().onClick.RemoveAllListeners();
        Cora.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => CountrySelect(1)));

        StartCoroutine(CountryAnim());
    }


    //도감 내부 Return 코루틴은 Manager에
    // 0 Partan 1 Cora
    void CountrySelect(int index)
    {
        Manager.instance.scene = (int)Manager.Menunum.Troop;
        Manager.instance.index = index;

        if (index == 0) Manager.instance.Countrys = Partan;
        else Manager.instance.Countrys = Cora;

        //모든 버튼 넣고
        for(int i = 0; i < 6; i++)
        {
            EliteMenu[i].Back();
        }

        //해당 버튼 노출
        for (int i = 0 + (3*index); i < 3 + (3*index); i++)
        {
            int num2 = i;
            EliteMenu[num2].button.onClick.RemoveAllListeners();

            //클릭시 그 나라의 아이콘만 Back 애니메이션
            for (int o = 0 + (3 * index); o < 3 + (3 * index); o++)
            {
                int num1 = o;
                EliteMenu[i].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => EliteMenu[num1].Back()));
            }

            //패널 조정
            EliteMenu[i].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => EliteIcon[num2].PanelMove1(num2)));
            EliteMenu[i].Move();
        }
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

        Vector3 startPos1 = PartanPos.transform.position;
        Vector3 startPos2 = CoraPos.transform.position;
        Vector3 endPos = transform.position;

        float time = 0;
        while (time <= 1)
        {
            Partan.transform.position = Vector3.Lerp(startPos1, endPos, curve.Evaluate(time));
            Cora.transform.position = Vector3.Lerp(startPos2, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }

        Manager.instance.corutine = false;
    }

    public IEnumerator CountryReturnAnim()
    {
        Manager.instance.corutine = true;

        Vector3 startPos = transform.position;
        Vector3 endPos1 = PartanPos.transform.position;
        Vector3 endPos2 = CoraPos.transform.position;

        float time = 0;
        while (time <= 1)
        {
            Partan.transform.position = Vector3.Lerp(startPos, endPos1, curve.Evaluate(time));
            Cora.transform.position = Vector3.Lerp(startPos, endPos2, curve.Evaluate(time));
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

        if(Manager.instance.Anim)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = RandomPos.transform.position;

            float time = 0;
            while (time <= 1)
            {
                Random.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
                //Custom.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(220, 0, curve.Evaluate(time)), 700);
                time += Time.deltaTime * Speed;
                yield return null;
            }

            Manager.instance.Anim = false;
        }

        foreach (var button in EliteMenu)
        {
            button.Back();
        }

        yield return new WaitForSeconds(0.3f);

        PlayButton.Move();
        CharacterButton.Move();

        Manager.instance.corutine = false;
    }

    public void IconSelectAnim1()
    {
        if(!Manager.instance.Anim)
        {
            Manager.instance.Anim = true;
            StartCoroutine(IconSelectAnim2());
        }
    }
    IEnumerator IconSelectAnim2()
    {
        Manager.instance.corutine = true;

        Random.SetActive(true);
        Random.transform.SetAsLastSibling();

        Vector3 startPos = RandomPos.transform.position;
        Vector3 endPos = transform.position;

        //Custom.SetActive(true);
        //Custom.transform.SetAsLastSibling();

        float time = 0;
        while (time <= 1)
        {
            Random.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            //Custom.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 220, curve.Evaluate(time)), 700);
            time += Time.deltaTime * Speed;
            yield return null;
        }

        Manager.instance.corutine = false;
    }

    /*코루틴 시작, 코루틴
    public void CustomSelect1()
    {
        StartCoroutine(CustomSelect2());
    }
    IEnumerator CustomSelect2()
    {
        Manager.instance.corutine = true;

        Custom.transform.SetAsLastSibling();

        StartCoroutine(CustomSelect3());

        float time = 0;
        while (time <= 1)
        {
            Custom.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(220, 600, curve.Evaluate(time)), 700);
            time += Time.deltaTime * Speed;
            yield return null;
        }

        Manager.instance.corutine = false;
    }
    IEnumerator CustomSelect3()
    {
        Manager.instance.corutine = true;

        Create.SetActive(true);
        Create.transform.SetAsLastSibling();
        Join.SetActive(true);
        Join.transform.SetAsLastSibling();

        float time = 0;
        while (time <= 1)
        {
            Create.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 220, curve.Evaluate(time)), 700);
            Create.transform.position = Vector3.Lerp(Create.transform.position, transform.GetChild(0).transform.position, curve.Evaluate(time));
            Join.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 220, curve.Evaluate(time)), 700);
            Join.transform.position = Vector3.Lerp(Join.transform.position, Cora.transform.position, curve.Evaluate(time));

            time += Time.deltaTime * Speed1;
            yield return null;
        }


        Manager.instance.corutine = false;
    }
    */
}
