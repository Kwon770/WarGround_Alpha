using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour
{
    [SerializeField] Transform Center;
    [SerializeField] float layoutMoveSpeed;
    [SerializeField] AnimationCurve curve;

    public void Move(int index)
    {
        StopAllCoroutines();
        StartCoroutine(MoveLerp(index));
    }

    IEnumerator MoveLerp(int index)
    {
        Transform target = transform.GetChild(index);
        Vector3 startPos,endPos;

        startPos = transform.position;
        endPos = transform.position + (Center.position - target.position);
        float time = 0;

        while (time<=1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * layoutMoveSpeed;
            yield return null;
        }
    }
}
