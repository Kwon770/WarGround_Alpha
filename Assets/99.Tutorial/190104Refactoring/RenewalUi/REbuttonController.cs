using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REbuttonController : MonoBehaviour {

    [SerializeField] REsystemManager systemManager;

    public enum ButtonOn { All, ATK, Move, Turn, Spawn }
    public ButtonOn buttonOn;


    public void ClickMove()
    {
        if(buttonOn == ButtonOn.Move)
        {
            systemManager.MoveButtonUnitMove();
            buttonOn = ButtonOn.All;
        }
    }

    public void OnClickMoveButton()
    {
        buttonOn = ButtonOn.Move;
    }

    
}
