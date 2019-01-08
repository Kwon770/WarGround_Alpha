using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REsystemManager : MonoBehaviour {

    [SerializeField] REunitInfo MarsInfo;
    [SerializeField] REunitBehaviour MarsBehave;
    [SerializeField] REunitBehaviour EnemeyBehave;
    [SerializeField] REunitInfo cameraMoveUnit;
    [SerializeField] REtileController tileController;
    [SerializeField] REactPointController actPointController;
    [SerializeField] REcameraManager camera;
    [SerializeField] REtileInfo tutorialTile;
    [SerializeField] REbuttonController buttonController;
    [SerializeField] REscriptMove scriptMove;
    [SerializeField] REscriptManager scriptManager;

    public int bitinium;
    public int commandPower;

    void Start () {

        tileController.GetChildTile();
        StartCoroutine(ScriptBoardStartAnim());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))    // move 버튼 누를 시로 바꾸기
        {
           
        }

        RayCast();
        CameraMove();
        ActPointController();
        SetScriptText();
    }

    public void MoveButtonUnitMove()
    {
        tileController.GetMovableTile(MarsInfo.unitTile, MarsInfo.UnitActPoint);
        ChangeTileColor();
        Debug.Log("실행");
    }

    void UnitMove(REtileInfo startPos, REtileInfo endPos, int cost)
    {
        StartCoroutine(MarsBehave.MovePlayer(startPos, endPos, cost));
    }

    void ResetSelectingTile()
    {
        tileController.ResetSelectingTile();
    }

    void RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.tag == "Tile")
                {
                    REtileInfo rayTile = hit.transform.GetComponent<REtileInfo>();

                    if (rayTile.Selecting == true)
                    {
                        UnitMove(MarsInfo.unitTile, rayTile, rayTile.tileCost);
                        MarsInfo.ConsumeActPoint(rayTile.tileCost);
                        //MarsInfo.unitTile.NullUnit();
                        //MarsInfo.unitTile = rayTile;
                        ResetSelectingTile();
                        ChangeTileColor();
                    }

                }

            }


        }
    }

    void CameraMove()
    {
        camera.CameraMove(cameraMoveUnit);
    }

    void ChangeTileColor()
    {
        for(int i = 0; i < tileController.tileList.Count; i++)
        {
            StartCoroutine(tileController.tileList[i].ChangeColor());
        }
    }

    void ActPointController()
    {
        actPointController.ActPointOn(MarsInfo.UnitActPoint);
    }

    //For tutorial proceed code
    IEnumerator ScriptBoardStartAnim()
    {
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(scriptMove.OnAnim());

        yield return null;
    }

    void HideScriptBoxForEvent()
    {
        StartCoroutine(scriptMove.HideAnim());
    }

    void SetScriptText()
    {
        scriptManager.SetScriptText();
    }

    void OnMoveButton()
    {
        buttonController.OnClickMoveButton();
    }

    void OnMovableTile()
    {
        ResetSelectingTile();
        tutorialTile.Selecting = true;
    }
}
