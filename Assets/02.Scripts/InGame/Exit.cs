using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    public void ClickExit()
    {
        if (Application.loadedLevel == 1)
        {
            Application.Quit();
        }
        else
        {
            FindObjectOfType<EndUI>().StartCoroutine("Loading");
        }
    }
	
}
