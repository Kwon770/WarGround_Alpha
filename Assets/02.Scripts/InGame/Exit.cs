using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ClickExit()
    {
        Debug.Log(Application.loadedLevel);
        if (Application.loadedLevel == 1)
        {
            Application.Quit();
        }
        else if(Application.loadedLevel == 3)
        {
            EndUI.UI._myScore = 0;
            EndUI.UI.SetRemainTurn(0);
        }
    }
	
}
