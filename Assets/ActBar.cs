using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActBar : MonoBehaviour {

    [SerializeField] GameObject Bar;

    [SerializeField] int IdleAct;
    // Use this for initialization

    public void Setting()
    {
        for (int i = 1; i <= IdleAct; i++) Instantiate(Bar).transform.parent = transform;
    }
    public void SetUI(int index)
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OffObj();
        }
        for (int i = 0; i < index; i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OnObj();
        }
    }
    public void ResetUI()
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OffObj();
        }
    }
}
