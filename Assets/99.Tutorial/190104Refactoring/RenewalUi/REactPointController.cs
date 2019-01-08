using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REactPointController : MonoBehaviour {

    public void ActPointOn(int actPoint)
    {
        for (int i = 0; i < 3; i++)
        {
            if(i < actPoint)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
         
        }
    }
}
