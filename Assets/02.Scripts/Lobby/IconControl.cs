using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconControl : MonoBehaviour {

    [SerializeField] GameObject SelectPos;
    [SerializeField] GameObject Pos;

    [SerializeField] float layoutMoveSpeed;
    [SerializeField] AnimationCurve curve;

    public void SelectMove()
    {
        Vector3 startPos, endPos;
        startPos = transform.position;
        endPos = SelectPos.transform.position;

        StartCoroutine(Anim(startPos, endPos));
    }
    public void CancelBack()
    {
        Vector3 startPos, endPos;
        startPos = transform.position;
        endPos = Pos.transform.position;

        StartCoroutine(Anim(startPos, endPos));
    }
    public IEnumerator Anim(Vector3 startPos, Vector3 endPos)
    {
        float time = 0;

        while (time <= 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * layoutMoveSpeed;
            yield return null;
        }
    }
}
