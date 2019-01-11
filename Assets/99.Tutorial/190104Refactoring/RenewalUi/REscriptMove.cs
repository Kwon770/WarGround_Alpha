using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REscriptMove : MonoBehaviour {

    [SerializeField] Transform hidePos;
    [SerializeField] Transform onPos;

    [SerializeField] AnimationCurve curve;
    [SerializeField] REscriptManager scriptManager;

    int turn = 0;

    public IEnumerator HideAnim()
    {
        scriptManager.isHide = true;

        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            transform.position = Vector3.LerpUnclamped(onPos.position, hidePos.position, curve.Evaluate(time));
            yield return null;
        }
    }

    public IEnumerator OnAnim()
    {

        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            transform.position = Vector3.LerpUnclamped(hidePos.position, onPos.position, curve.Evaluate(time));
            yield return null;
        }

        scriptManager.isHide = false;
    }
}
