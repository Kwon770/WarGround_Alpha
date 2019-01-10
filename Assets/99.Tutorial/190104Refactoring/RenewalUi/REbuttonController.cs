using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REbuttonController : MonoBehaviour {

    [SerializeField] REsystemManager systemManager;
    [SerializeField] REuiTurnEnd turnEnd;

    public enum ButtonOn { All, ATK, Move, Turn, Spawn }
    public ButtonOn buttonOn;
    public bool playerTurn;

    public int turn;

    public bool isFirstSpawn;
    public bool isFirstAttack;

    public void ClickMove()
    {
        if(buttonOn == ButtonOn.Move)
        {
            buttonOn = ButtonOn.All;
            systemManager.MoveButtonUnitMove();
            systemManager.PassScript();

        }
    }

    public void OnClickMoveButton()
    {
        buttonOn = ButtonOn.Move;
    }

    public void ClickAttack()
    {
        if (buttonOn == ButtonOn.ATK && isFirstAttack == false)
        {
            isFirstAttack = true;
            buttonOn = ButtonOn.All;
            systemManager.ClickAttackButton();
        }
    }

    public void OnClickAttackButton()
    {
        if(isFirstAttack == false)
        {
            buttonOn = ButtonOn.ATK;
        }

    }

    public void ClickTurn()
    {
        if (buttonOn == ButtonOn.Turn)
        {
            buttonOn = ButtonOn.All;
            turnEnd.StartCoroutine(turnEnd.TurnEndAnim());
           
            systemManager.AfterTurnButtonClick(turn);
            if(playerTurn == true)
            {
                playerTurn = false;
                turn++;
            }
            else
            {
                playerTurn = true;
            }

        }
    }

    public void OnClickTurnButton()
    {
        buttonOn = ButtonOn.Turn;
    }

    public void ClickSpawn()
    {
        if (buttonOn == ButtonOn.Spawn && isFirstSpawn == false)
        {
            isFirstSpawn = true;
            buttonOn = ButtonOn.All;
            systemManager.SpawnTileSelecting();
        }
    }

    public void OnClickSpawnButton()
    {
        if(isFirstSpawn == false)
        {

            buttonOn = ButtonOn.Spawn;

        }
    }
}
