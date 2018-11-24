using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour {

    [SerializeField] Transform On;
    [SerializeField] Transform Off;

    public void OnObj()
    {
        On.SetSiblingIndex(1);
    }
    public void OffObj()
    {
        Off.SetSiblingIndex(1);
    }
}
