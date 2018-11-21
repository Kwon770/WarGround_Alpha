using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float layoutMoveSpeed;

    public GameObject Empty_Icons;


    public void Play()
    {
        manager.scene = (int)Manager.Menunum.Play;
        Empty_Icons.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
            if (EliteMenu[i] == null)
            {
                Debug.Log("me");
            }
            if(EliteMenu[i].button == null)
            {
                Debug.Log("bu");
            }
            EliteMenu[i].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>LobbyNetwork.instance.SetElite(i)));
        }

        StartCoroutine(MenuAnim());
    }
    public void Char()
    {
        manager.scene = (int)Manager.Menunum.Character;
        Empty_Icons.SetActive(true);

        for (int i = 0; i < 6; i++)
        {
             EliteMenu[i].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => EliteIcon[i].SelectMove()));
        }

        StartCoroutine(CountryAnim());
    }





    IEnumerator CountryAnim()
    {
        Partan.SetActive(true);
        Cora.SetActive(true);
        yield return null;

        float time = 0;

        while (time <= 0.5f)
        {
            Partan.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 215, curve.Evaluate(time)), 0);
            Cora.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(0, 215, curve.Evaluate(time)), 0);
            time += Time.deltaTime * layoutMoveSpeed;
            yield return null;
        }
    }

    public IEnumerator CountryReturnAnim()
    {
        float time = 0;

        while (time <= 0.5f)
        {
            Partan.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(215, 0, curve.Evaluate(time)), 0);
            Cora.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(215, 0, curve.Evaluate(time)), 0);
            time += Time.deltaTime * layoutMoveSpeed;
            yield return null;
        }

        Partan.SetActive(false);
        Cora.SetActive(false);
    }

    public IEnumerator MenuAnim()
    {

        PlayButton.Back();
        CharacterButton.Back();

        yield return new WaitForSeconds(0.3f);

        foreach (var button in EliteMenu)
        {
            button.Move();
        }
    }
    public IEnumerator MenuReturnAnim()
    {
        foreach (var button in EliteMenu)
        {
            button.Back();
        }

        yield return new WaitForSeconds(0.3f);

        PlayButton.Move();
        CharacterButton.Move();
    }
}
