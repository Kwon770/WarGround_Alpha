using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

    public Button button;

    [SerializeField] GameObject Pos;
    [SerializeField] GameObject home;

    [SerializeField] float layoutMoveSpeed;
    [SerializeField] AnimationCurve curve;

    //public Button Button
    //{
    //    get
    //    {
    //        return _button;
    //    }
    //}

    public void Move()
    {
        Vector3 startPos, endPos;
        startPos = transform.position;
        endPos = Pos.transform.position;

        StartCoroutine(Anim(startPos, endPos));
    }
    public void Back()
    {
        Vector3 startPos, endPos;
        startPos = transform.position;
        endPos = home.transform.position;

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
