using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    public enum Menu
    {
        Home,
        Play,
        Character,
    }

    [SerializeField]
    Panels panels;
    [SerializeField]
    NavigationButton _home;
    [SerializeField]
    NavigationButton _play;
    [SerializeField]
    NavigationButton _character;

    NavigationButton[] _buttons;

    private void Awake()
    {
        _buttons = new NavigationButton[] { _home, _play, _character };

        // 클릭 이벤트 등록
        for (int i = 0; i < _buttons.Length; i++)
        {
            int cachedIndex = i;
            _buttons[i].Button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => Select((Menu)cachedIndex)));
        }
    }

    private void Start()
    {
        Select(Menu.Home);
    }

    public void Select(Menu menu)
    {
        for (int i = 0; i < _buttons.Length; i++)
            _buttons[i].Selected = (menu == (Menu)i);

        // TODO : 실제 패널 이동
        panels.Move((int)menu);
    }

    public void PopUpSettings()
    {
        // TODO : 설정창 팝업
    }

    public void ExitGame()
    {
        // TODO : 확인 팝업
        Application.Quit();
    }
}
