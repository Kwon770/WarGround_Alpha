using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REscriptMove : MonoBehaviour {

    [SerializeField] Transform hidePos;
    [SerializeField] Transform onPos;

    [SerializeField] AnimationCurve curve;

    int turn = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(turn == 0)
            {
                StartCoroutine(HideAnim());
                turn = 1;
            }
            else
            {
                StartCoroutine(OnAnim());
                turn = 0;
            }
            
        }
    }

    IEnumerator HideAnim()
    {
        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            transform.position = Vector3.LerpUnclamped(onPos.position, hidePos.position, curve.Evaluate(time));
            yield return null;
        }
    }

    IEnumerator OnAnim()
    {
        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            transform.position = Vector3.LerpUnclamped(hidePos.position, onPos.position, curve.Evaluate(time));
            yield return null;
        }
    }
}
