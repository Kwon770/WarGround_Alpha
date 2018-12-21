using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager instance;
    public void Awake()
    {
        instance = this;
    }

    //로비씬 나가면 터트리기 가능?

    public GameObject Empty_Icons;
    public GameObject PanelPos;
    public GameObject PanelChar;
    public GameObject AlarmPos1;
    public GameObject AlarmPos2;
    public GameObject PanelAlarm;

    [SerializeField] Menu menu;
    [SerializeField] LoadTutorial tutorial;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float Speed;

    [HideInInspector] public bool Anim = false;

    [HideInInspector] public GameObject Countrys;
    [HideInInspector] public GameObject Troops;

    public enum Menunum
    {
        Home,
        Play,
        Character,
        Country,
        Troop,
        Description
    }

    [HideInInspector] public int scene = 0;
    [HideInInspector] public int index;
    [HideInInspector] public bool corutine = false;


    private void Start()
    {
        StartCoroutine(AlarmPanel());
    }

    void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape) && !corutine)
        {
            if (scene == (int)Menunum.Home)
            {

            }
            else if (scene == (int)Menunum.Play)
            {
                StartCoroutine(menu.MenuReturnAnim());
                scene = (int)Menunum.Home;
            }
            else if (scene == (int)Menunum.Character)
            {
                StartCoroutine(menu.CountryReturnAnim());
                scene = (int)Menunum.Home;
            }
            else if (scene == (int)Menunum.Troop)
            {
                ReturnTroop(index);
                scene = (int)Menunum.Character;
            }
            else if(scene == (int)Menunum.Description)
            {
                Debug.Log("ddddddddddddddddd");
                SoundManager.soundmanager.clickBackLobbyButton();
                StartCoroutine(ReturnPanel());
                scene = (int)Menunum.Troop;
            }
        }
	}
    

    public void Loading()
    {
        PanelAlarm.SetActive(false);
        tutorial.gameObject.SetActive(true);
        tutorial.Loading();
    }

    public void DescriptionBack()
    {
        StartCoroutine(ReturnPanel());
        scene = (int)Menunum.Troop;
    }

    IEnumerator ReturnPanel()
    {
        corutine = true;
        scene = (int)Menunum.Description;

        Vector3 startPos = transform.position;
        Vector3 endPos = PanelPos.transform.position;

        float time = 0;

        while (time <= 1)
        {
            PanelChar.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }

        ToTroop(index);

        corutine = false;
    }

    void ToTroop(int index)
    {
        for (int i = 0 + (3 * index); i < 3 + (3 * index); i++)
        {
            Empty_Icons.transform.GetChild(i).GetComponent<MenuControl>().Move();
        }
    }

    void ReturnTroop(int index)
    {
        for(int i = 0 + (3*index); i < 3 + (3*index); i++)
        {
            Empty_Icons.transform.GetChild(i).GetComponent<MenuControl>().Back();
        }
    }

    IEnumerator AlarmPanel()
    {
        Vector3 startPos, endPos;
        startPos = AlarmPos1.transform.position;
        endPos = AlarmPos2.transform.position;

        float time = 0;
        while (time <= 1)
        {
            PanelAlarm.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }

        yield return new WaitForSeconds(5);

        startPos = AlarmPos2.transform.position;
        endPos = AlarmPos1.transform.position;

        time = 0;
        while (time <= 1)
        {
            PanelAlarm.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }
    }
}
