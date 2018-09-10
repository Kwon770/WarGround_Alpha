using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_LobbyCanvas : MonoBehaviour {

    public GameObject toPlayForAnim;
    public bool overOut = false;

    public void OverOut()
    {
        overOut = true;
        toPlayForAnim.transform.localScale = new Vector3(0, 0, 0);
        StopCoroutine(ToPlayAim());
    }

    public void ToPlayOver()
    {
        StartCoroutine(ToPlayAim());
    }

	IEnumerator ToPlayAim()
    {
        for(int i = 0; i < 100; i++)
        {
            toPlayForAnim.transform.localScale += new Vector3(0.01f, 0, 0);
            if(overOut)
            {
                overOut = false;
                break;
            }
            yield return new WaitForSeconds(0.005f);
        }
    }
}
