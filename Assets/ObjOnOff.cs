using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOnOff : MonoBehaviour {

    [SerializeField] GameObject obj;
    public void TurnOn()
    {
        obj.SetActive(true);
    }
    public void TurnOff()
    {
        obj.SetActive(false);
    }
}
