using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REbuttonController : MonoBehaviour {

    [SerializeField] REsystemManager systemManager;
    [SerializeField] REuiTurnEnd turnEnd;
    [SerializeField] REscriptManager scriptManager;
    [SerializeField] Transform option;
     [SerializeField] Transform hidePos;
    [SerializeField] Transform onPos;

    [SerializeField] AnimationCurve curve;

    public enum ButtonOn { All, ATK, Move, Turn, Spawn }
    public ButtonOn buttonOn;
    public bool playerTurn;

    public bool isOptionOn;
    public bool isOptionAnim;
    public bool isTurnClick;

    public int turn;

    public bool isFirstSpawn;
    public bool isFirstAttack;

    public void ClickMove()
    {
        if(buttonOn == ButtonOn.Move)
        {
            buttonOn = ButtonOn.All;
            SoundManager.soundmanager.clickIngameButton();
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
            isTurnClick = true;
            SoundManager.soundmanager.clickTurnButton();
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
        if(isTurnClick == false)
        {
            buttonOn = ButtonOn.Turn;
        }

    }

    public void ClickSpawn()
    {
        if (buttonOn == ButtonOn.Spawn && isFirstSpawn == false)
        {
            isFirstSpawn = true;
            SoundManager.soundmanager.clickIngameButton();
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

    public void ClickOption()
    {
        if(isOptionOn == false && isOptionAnim == false)
        {
            isOptionOn = true;
            StartCoroutine(OnOption());
        }
        else if(isOptionOn == true && isOptionAnim == false)
        {
            isOptionOn = false;
            StartCoroutine(OffOption());
        }

    }

    public void ClickScript()
    {
        if(scriptManager.isHide == false)
        {
            systemManager.PassScriptUseEnter();
        }
    }

    IEnumerator OnOption()
    {
        isOptionAnim = true;
        SoundManager.soundmanager.clickLobbyButton();

        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            option.transform.position = Vector3.LerpUnclamped(hidePos.position, onPos.position, curve.Evaluate(time));
            yield return null;
        }

        isOptionAnim = false;
    }

    IEnumerator OffOption()
    {
        isOptionAnim = true;
        SoundManager.soundmanager.clickLobbyButton();

        float time = 0;

        while (time <= 1)
        {
            time += 2f * Time.deltaTime;

            option.transform.position = Vector3.LerpUnclamped(onPos.position, hidePos.position, curve.Evaluate(time));
            yield return null;
        }

        isOptionAnim = false;
    }
}
