using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {
    
    [SerializeField] Transform myTurn;
    [SerializeField] Transform enemyTurn;

    [SerializeField] AnimationCurve curve;

    Coroutine coroutine;


    [SerializeField] Transform upSide;
    [SerializeField] Transform downSide;

    public void MyTurn()
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(SetButton(myTurn, enemyTurn));
    }
    public void EnemyTurn()
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(SetButton(enemyTurn, myTurn));
    }
    IEnumerator SetButton(Transform setButton, Transform NonButton)
    {
        yield return null;
        float time = 0;

        while (time <= 1)
        {
            if (time > 0.3 && setButton.GetSiblingIndex() == 0) setButton.SetSiblingIndex(1);

            setButton.position = Vector3.LerpUnclamped(upSide.position, downSide.position, curve.Evaluate(time));
            NonButton.position = Vector3.LerpUnclamped(downSide.position, upSide.position, curve.Evaluate(time));
            time += Time.deltaTime;

            yield return null;
        }
        coroutine = null;
    }
}