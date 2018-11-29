using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sys : MonoBehaviour {

    public Text profileName;
    public Image profileImage;

	void Start () {

        StartCoroutine(WriteProfile());
    }
	
	IEnumerator WriteProfile()
    {
        while (profileName.text == "")
        {
            profileName.text = PhotonNetwork.playerName;
            yield return null;
        }
    }
}
