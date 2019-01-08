using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REuiTurnEnd : MonoBehaviour {

    [SerializeField] Transform frontPos;
    [SerializeField] Transform backPos;

    [SerializeField] AnimationCurve curve;

    [SerializeField] Transform animUpPos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(TurnEndAnim());
        }
    }

    IEnumerator TurnEndAnim()
    {
        Transform backTurn = transform.GetChild(0).transform;
        Transform frontTurn = transform.GetChild(1).transform;

        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            backTurn.transform.position = Vector3.LerpUnclamped(backPos.position, animUpPos.position, curve.Evaluate(time));
            yield return null;
        }

        time = 0;

        while (time <= 1)
        {
            time += 1.5f * Time.deltaTime;

            backTurn.SetAsLastSibling();

            backTurn.transform.position = Vector3.LerpUnclamped(animUpPos.position, frontPos.position, curve.Evaluate(time));
            frontTurn.transform.position = Vector3.LerpUnclamped(frontPos.position, backPos.position, curve.Evaluate(time));

            yield return null;
        }
 
    }
}
