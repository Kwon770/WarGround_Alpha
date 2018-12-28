using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActBar : MonoBehaviour {

    [SerializeField] GameObject Bar;

    [SerializeField] int IdleAct;
    // Use this for initialization

    public void Setting()
    {
        GameObject temp;
        for (int i = 1; i <= IdleAct; i++)
        {
            temp = Instantiate(Bar);
            temp.transform.parent = transform;
            //temp.transform.localScale *= (Screen.width / 1600);
        }
    }

    public void ResetUI()
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OffObj();
        }
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
}
