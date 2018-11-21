using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconControl : MonoBehaviour {

    [SerializeField] GameObject SelectPos;
    [SerializeField] GameObject Pos;

    [SerializeField] float Speed;
    [SerializeField] AnimationCurve curve;

    public void SelectMove()
    {
        Manager.instance.scene = (int)Manager.Menunum.Troop;

        Vector3 startPos, endPos;
        startPos = transform.position;
        endPos = SelectPos.transform.position;

        for(int i = 0; i < transform.GetSiblingIndex(); i++) transform.parent.GetChild(i).GetComponent<MenuControl>().Back();
        for (int i = transform.GetSiblingIndex() + 1; i < 6; i++) transform.parent.GetChild(i).GetComponent<MenuControl>().Back();

        StartCoroutine(Anim(startPos, endPos, 300, 500));
    }
    public void CancelBack()
    {
        Vector3 startPos, endPos;
        startPos = transform.position;
        endPos = Pos.transform.position;

        StartCoroutine(Anim(startPos, endPos, 500, 300));
    }
    public IEnumerator Anim(Vector3 startPos, Vector3 endPos, int from, int to)
    {
        Manager.instance.corutine = true;
        Manager.instance.Troops = gameObject;

        float time = 0;

        while (time <= 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(from, to, curve.Evaluate(time)), Mathf.Lerp(from, to, curve.Evaluate(time)));
            time += Time.deltaTime * Speed;
            yield return null;
        }

        Manager.instance.corutine = false;
    }
}
