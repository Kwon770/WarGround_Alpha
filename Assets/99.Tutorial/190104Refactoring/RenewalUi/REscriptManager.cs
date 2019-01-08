using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class REscriptManager : MonoBehaviour {

    public List<string> scriptList;
    public Text scriptText;
    public int scriptNum;
    public bool isTextPass;

    public void SetScriptText()
    {
        scriptText.text = scriptList[scriptNum];
    }

    public void PassScript()    // 버튼 이벤트 없을 때 스크립트 넘기기
    {
        if(isTextPass == true)
        {
            scriptNum++;
        }
    }


    public void IsTextPassOn()  // 버튼을 누른 후 활성화시킬것
    {
        isTextPass = true;
        scriptNum++;
    }
}
