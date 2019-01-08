using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REsymbol : MonoBehaviour {

    [SerializeField]
    Quaternion originRot;

    void KeepOriginRot()
    {
        transform.rotation = originRot;
    }
}
