using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class NavigationButton : MonoBehaviour
{
    [SerializeField] Image _imageOnSelected;
    [SerializeField] Button _button;
    bool _selected;

    public Button Button
    {
        get
        {
            return _button;
        }
    }

    public bool Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            _imageOnSelected.enabled = value;
            _selected = value;
        }
    }

    public void OnPointerEnter()
    {
        _imageOnSelected.enabled = true;
    }

    public void OnPointerExit()
    {
        _imageOnSelected.enabled = _selected;
    }
}