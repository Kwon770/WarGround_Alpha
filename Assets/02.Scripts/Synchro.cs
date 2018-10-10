using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synchro : MonoBehaviour {
    UnitInfo unitinfo;
    const float synchroSpeed = 5f;
    private void Awake()
    {
        unitinfo = GetComponent<UnitInfo>();
    }
    void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, unitinfo.currrentPos, Time.deltaTime * synchroSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, unitinfo.currentQuater, Time.deltaTime * synchroS;
    }
}
