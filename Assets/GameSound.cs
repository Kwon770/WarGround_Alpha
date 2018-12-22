using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSound : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
{
    enum Kinds
    {
        Normal,
        TurnButton,
        BitiniumBar,
        CommandBar,
        ActBar,
        Icon
    }

    [SerializeField] Kinds ButtonKind;

    // Use this for initialization
    public void click()
    {
        SoundManager.soundmanager.clickIngameButton();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Ddd");
        if (ButtonKind == Kinds.Normal)
        {
            SoundManager.soundmanager.touchIngameButton();
        }
        if (ButtonKind == Kinds.TurnButton)
        {
            SoundManager.soundmanager.clickIngameButton();
        }
        if (ButtonKind == Kinds.Icon)
        {
            SoundManager.soundmanager.touchIngameButton();
        }
        if (ButtonKind == Kinds.CommandBar)
        {
            SoundManager.soundmanager.touchIngameButton();
        }
        if (ButtonKind == Kinds.BitiniumBar)
        {
            SoundManager.soundmanager.touchBitiniumBar();
        }
        if (ButtonKind == Kinds.ActBar)
        {
            SoundManager.soundmanager.touchIngameButton();
        }
    }
}
