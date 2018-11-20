using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {


    [SerializeField][Tooltip(" pluto, merica, kamiken, savage, brown, royal")]
    MenuControl[] EliteIcon;

    [SerializeField] MenuControl PlayButton;
    [SerializeField] MenuControl CharacterButton;

    [SerializeField] Manager manager;

    public GameObject Empty_Icons;


    public void Play()
    {
        manager.scene = (int)Manager.Menunum.Play;
        Empty_Icons.SetActive(true);

        //for(int i = 0; i < 6; i++)
        //{
        //    menu[i].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => Select((Menu)cachedIndex)));
        //}

        StartCoroutine(MenuAnim());
    }
    public void Char()
    {
        manager.scene = (int)Manager.Menunum.Character;
        Empty_Icons.SetActive(true);

        //for (int i = 0; i < 6; i++)
        //{
        //    menu[i].button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => Select((Menu)cachedIndex)));
        //}

        StartCoroutine(MenuAnim());
    }

    public IEnumerator MenuAnim()
    {

        PlayButton.Back();
        CharacterButton.Back();

        yield return new WaitForSeconds(0.3f);

        foreach (var button in EliteIcon)
        {
            button.Move();
        }
    }
    public IEnumerator MenuReturnAnim()
    {
        foreach (var button in EliteIcon)
        {
            button.Back();
        }

        yield return new WaitForSeconds(0.3f);

        PlayButton.Move();
        CharacterButton.Move();
    }
}
