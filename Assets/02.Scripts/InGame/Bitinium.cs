using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bitinium : MonoBehaviour {

    [SerializeField] GameObject Bar;
    [SerializeField] Text text;

    int bit;
    int point;
    
    public void Setting(int Bit)
    {
        bit = Bit;
        text.text = 0 + " / " + Bit + " ( +" + 0 + ")";
    }
    public void SetUI(int bitinium, int point)
    {
        this.point = point;
        text.text = bitinium + " / " + bit + " ( +" + point + ")";
    }
    public void SetUI(int bitinium)
    {
        text.text = bitinium + " / " + bit + " ( +" + point + ")";
    }
}
