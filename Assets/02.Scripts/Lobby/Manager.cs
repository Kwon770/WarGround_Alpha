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

    [SerializeField] Menu menu;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float Speed;

    [HideInInspector] public GameObject Countrys;
    [HideInInspector] public GameObject Troops;

    public enum Menunum
    {
        Home,
        Play,
        Character,
        Country,
        Troop
    }
    [HideInInspector] public int scene = 0;
    [HideInInspector] public int index;
    [HideInInspector] public bool corutine = false;
       

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
            else if (scene == (int)Menunum.Country)
            {
                StartCoroutine(CountrysReturn());
                scene = (int)Menunum.Character;
            }
            else if (scene == (int)Menunum.Troop)
            {
                TroopsReturnFunc(index);
                scene = (int)Menunum.Country;
            }
        }
	}


    IEnumerator CountrysReturn()
    {
        corutine = true;

        menu.CountryIconBack();
        yield return new WaitForSeconds(0.1f);

        float time = 0;
        while (time <= 1)
        {
            Countrys.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(600, 215, curve.Evaluate(time)), 700);
            time += Time.deltaTime * Speed;
            yield return null;
        }

        corutine = false;
    }

    void TroopsReturnFunc(int index)
    {
        for(int i = 0 + (3*index); i < 3 + (3*index); i++)
        {
            if(i != Troops.transform.GetSiblingIndex())
            {
                Empty_Icons.transform.GetChild(i).GetComponent<MenuControl>().Move();
            }
        }
        Troops.GetComponent<IconControl>().CancelBack();
    }
}
