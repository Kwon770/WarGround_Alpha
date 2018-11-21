using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    [SerializeField] Menu menu;

    public enum Menunum
    {
        Home,
        Play,
        Character,
    }
    [HideInInspector]public int scene = 0;


    void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (scene == (int)Menunum.Home)
            {

            }
            else if (scene == (int)Menunum.Play || scene == (int)Menunum.Character)
            {
                StartCoroutine(menu.MenuReturnAnim());
                scene = (int)Menunum.Home;
            }
        }
	}
}
