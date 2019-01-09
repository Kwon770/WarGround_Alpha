using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leadership : MonoBehaviour {

    [SerializeField] GameObject Bar;
    [SerializeField] Text text;

    int leadership;

    public void Setting(int Leadership)
    {
        leadership = Leadership;
        text.text = 0 + " / " + Leadership;
    }

    public void SetUI(int index)
    {
        text.text = index + " / " + leadership;
    }
}