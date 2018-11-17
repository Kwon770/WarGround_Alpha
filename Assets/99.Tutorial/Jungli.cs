using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jungli : MonoBehaviour {

    GameObject[] t = new GameObject[100];

    GameObject temp;

    TileInfoTutorial tile;


    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            t[i] = transform.GetChild(i).gameObject;
            Debug.Log("a" + i);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount - i - 1; j++)
            {
                if (t[j].GetComponent<TileInfoTutorial>().X > t[j + 1].GetComponent<TileInfoTutorial>().X)
                {
                    temp = t[j];
                    t[j] = t[j + 1];
                    t[j + 1] = temp;
                }
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount - i - 1; j++)
            {
                if(t[j].GetComponent<TileInfoTutorial>().X != t[j + 1].GetComponent<TileInfoTutorial>().X)
                {
                    continue;
                }
                
                else if (t[j].GetComponent<TileInfoTutorial>().Y > t[j + 1].GetComponent<TileInfoTutorial>().Y)
                {
                    temp = t[j];
                    t[j] = t[j + 1];
                    t[j + 1] = temp;
                }
            }
        }

        
        for (int i = 0; i < transform.childCount; i++)
        {
            t[i].transform.name = "tile[" + i + "]";
            t[i].transform.SetSiblingIndex(i);
        }
        
    }
}
