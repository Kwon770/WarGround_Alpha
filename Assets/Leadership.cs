﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leadership : MonoBehaviour {

    [SerializeField] GameObject Bar;

    [SerializeField] int IdleLeadership;
    // Use this for initialization

    public void Setting(int Leadership)
    {
        for (int i = 1; i <= Leadership; i++) Instantiate(Bar).transform.parent = transform;
    }
    public void SetUI(int index)
    {
        for(int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OffObj();
        }
        for (int i = 0; i < GameData.data.Maxbitinium - index; i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OnObj();
        }
    }
}
