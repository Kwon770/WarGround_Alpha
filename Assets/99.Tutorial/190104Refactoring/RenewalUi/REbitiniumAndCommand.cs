using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class REbitiniumAndCommand : MonoBehaviour {

    public Text bitinium;
    public Text command;

    public void TextBitinium(int bit)
    {
        bitinium.text = bit + " / 10 (+2)"; 
    }
    public void TextCommand(int cmd)
    {
        command.text = cmd + " / 6";
    }

}
