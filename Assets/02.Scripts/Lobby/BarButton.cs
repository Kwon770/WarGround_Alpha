using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarButton : MonoBehaviour {

    Bar bar;
    RectTransform rect;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    void OnPointerEnter()
    {
        bar.Selected(transform.position.x, rect.rect.width);
    }

    void OnPointerExit()
    {

    }

    void OnClick()
    {

    }
}
