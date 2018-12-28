using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leadership : MonoBehaviour {

    [SerializeField] GameObject Bar;

    public void Setting(int Leadership)
    {
        GameObject temp;
        for (int i = 1; i <= Leadership; i++)
        {
            temp = Instantiate(Bar);
            temp.transform.parent = transform;
//            temp.transform.localScale *= (Screen.width / 1600 * 0.8f);
        }
    }

    public void SetUI(int index)
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            transform.GetChild(i).GetComponent<OnOff>().OffObj();
        }
        for (int i = 0; i < GameData.data.LeaderShip; i++)
        {
            Debug.Log(i);
            transform.GetChild(i).GetComponent<OnOff>().OnObj();
        }
    }
}