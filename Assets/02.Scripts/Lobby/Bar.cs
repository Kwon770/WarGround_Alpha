using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    [SerializeField] Image anim1, anim2;
    [SerializeField] GameObject home, play, character;
    RectTransform rect1, rect2;
    float pos;
    float width;

    private void Start()
    {
        rect1 = anim1.GetComponent<RectTransform>();
        rect2 = anim2.GetComponent<RectTransform>();
    }

    public void Selected(float pos, float width)
    {

    }

    public void Return()
    {
        anim1.transform.position = new Vector2(pos, anim1.transform.position.y);
        anim2.transform.position = new Vector2(pos, anim1.transform.position.y);
        rect1.sizeDelta = new Vector2(width, rect1.rect.height);
        rect2.sizeDelta = new Vector2(width, rect2.rect.height);
    }
}
