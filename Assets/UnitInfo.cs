using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {

    public string Owner;

    [SerializeField] int maxhp;
    [SerializeField] int HP;
    [SerializeField] int SHD;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
