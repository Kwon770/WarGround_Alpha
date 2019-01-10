using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class REscriptManager : MonoBehaviour {

    public List<string> scriptList;
    public Text scriptText;
    public int scriptNum;

    public bool isHide;

    public void SetScriptText()
    {
        scriptText.text = scriptList[scriptNum];
    }


}
