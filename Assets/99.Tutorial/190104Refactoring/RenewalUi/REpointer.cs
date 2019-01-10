using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class REpointer : MonoBehaviour {

    public List<Image> pointer;
    public int pointerIndex;

    public void OnOffPointer()
    {
        for(int i = 0; i < pointer.Count; i++)
        {
            if(i == pointerIndex)
            {
                pointer[pointerIndex].gameObject.SetActive(true);
            }
            else
            {
                pointer[i].gameObject.SetActive(false);
            }

        }
    }

}
