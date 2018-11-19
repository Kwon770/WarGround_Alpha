using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

    public GameObject home;
    [SerializeField] float layoutMoveSpeed;
    [SerializeField] AnimationCurve curve;


    public IEnumerator MenuAnim()
    {
        Vector3 startPos, endPos;

        startPos = transform.position;
        endPos = home.transform.position;
        float time = 0;

        while (time <= 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * layoutMoveSpeed;
            yield return null;
        }
    }
}
