using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bitinium : MonoBehaviour {

    [SerializeField] GameObject Bar;
    
    // Use this for initialization

    public void Setting(int Bit)
    {
        for(int i = 1; i <= Bit; i++) Instantiate(Bar).transform.parent = transform;
    }
    public void SetUI(int point)
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OffObj();
        }
        for (int i = 0; i < point; i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OnObj();
        }
    }
}
