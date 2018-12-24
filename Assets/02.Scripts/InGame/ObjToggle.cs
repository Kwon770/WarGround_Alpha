using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjToggle : MonoBehaviour {

    public bool toggle;

    [SerializeField] MenuControl menu;

    public void Awake()
    {
        toggle = false;
    }
    public void SetToggle()
    {
        if (toggle)
        {
            toggle = false;
            menu.Back();
            return;
        }
        toggle = true;
        menu.Move();
    }
}
