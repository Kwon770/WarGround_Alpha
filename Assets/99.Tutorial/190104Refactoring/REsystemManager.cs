using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class REsystemManager : MonoBehaviour {

    [SerializeField] REunitInfo MarsInfo;
    [SerializeField] REunitBehaviour MarsBehave;
    [SerializeField] REunitInfo EnemyInfo;
    [SerializeField] REunitBehaviour EnemyBehave;
    [SerializeField] REunitInfo cameraMoveUnit;
    [SerializeField] REtileController tileController;
    [SerializeField] REactPointController actPointController;
    [SerializeField] REcameraManager camera;
    [SerializeField] REtileInfo tutorialTile;
    [SerializeField] REbuttonController buttonController;
    [SerializeField] REscriptMove scriptMove;
    [SerializeField] REscriptManager scriptManager;
    [SerializeField] REpointer pointer;

    [SerializeField] REtileInfo spawnTile;
    [SerializeField] REunitInfo warrior;

    [SerializeField] REbitiniumAndCommand topText;

    [SerializeField] GameObject loadingCanvas;

    public int bitinium;
    public int commandPower;

    public bool end;
    bool attackEnemy;

    private void Awake()
    {
        SoundManager.soundmanager.planeBGM(true);
    }

    void Start () {

        tileController.GetChildTile();
        StartCoroutine(ScriptBoardStartAnim());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && scriptManager.isHide == false)  
        {
            PassScriptUseEnter();
        }

        topText.TextBitinium(bitinium);
        topText.TextCommand(commandPower);

        ScriptFunction();
        OnOffPointer();
        RayCast();
        CameraMove();
        ActPointController();
        SetScriptText();
        OccupyTileColor();

    }
    public void OccupyTileColor()
    {
        for(int i = 0; i < tileController.tileList.Count; i++)
        {
            if (tileController.tileList[i].Selecting == false && tileController.tileList[i].spawnSelecting == false)
            {
                tileController.tileList[i].OccupyColor();
            }

        }
    }

    public void MoveButtonUnitMove()
    {
        tileController.GetMovableTile(MarsInfo.unitTile, MarsInfo.UnitActPoint);
        ChangeTileColor();
    }

    void UnitMove(REtileInfo startPos, REtileInfo endPos, int cost)
    {
        StartCoroutine(MarsBehave.MovePlayer(startPos, endPos, cost));
    }

    void EnemyMove(REtileInfo startPos, REtileInfo endPos, int cost)
    {
        StartCoroutine(EnemyBehave.MovePlayer(startPos, endPos, cost));
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
                        //PassScript();
                        ResetSelectingTile();
                        ChangeTileColor();
                    }
                    else if (rayTile.spawnSelecting == true)
                    {
                        spawnTile.spawnSelecting = false;
                        spawnTile.StartCoroutine(spawnTile.ChangeColor());
                        warrior.gameObject.SetActive(true);
                        MarsInfo.UnitActPoint = 2;
                        PassScript();
                    }

                }
                else if (hit.transform.tag == "Enemy")
                {
                    if(hit.transform.GetComponent<REunitInfo>().isDamaed == true)
                    {
                        hit.transform.GetComponent<REunitInfo>().isDamaed = false;
                        MarsBehave.StartCoroutine(MarsBehave.Attack());
                        EnemyBehave.StartCoroutine(EnemyBehave.Damaged());
                        MarsInfo.UnitActPoint = 0;
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

    void ScriptFunction()
    {
        if (scriptManager.scriptNum == 1)
        {
            buttonController.OnClickMoveButton();
        }
        else if (scriptManager.scriptNum == 5)
        {
 
            buttonController.OnClickTurnButton();
        }
        else if (scriptManager.scriptNum == 6)
        {
            buttonController.isTurnClick = false;
            bitinium = 2;
        }

        else if (scriptManager.scriptNum == 13)
        {
            buttonController.OnClickTurnButton();
        }
        else if (scriptManager.scriptNum == 14)
        {
            bitinium = 4;
        }
        else if (scriptManager.scriptNum == 17)
        {
            buttonController.OnClickSpawnButton();
        }
        else if (scriptManager.scriptNum == 18)
        {
            commandPower = 1;
            bitinium = 1;
        }
        else if (scriptManager.scriptNum == 21)
        {
            buttonController.OnClickAttackButton();
        }
    }

    public void PassScriptUseEnter()
    {
        if(scriptManager.scriptNum != 1 && scriptManager.scriptNum != 2 && scriptManager.scriptNum != 5 && scriptManager.scriptNum != 13 && scriptManager.scriptNum != 17 && scriptManager.scriptNum != 21 && scriptManager.scriptNum != 24)
        {
            scriptManager.scriptNum++;
        }
        else if(scriptManager.scriptNum == 24 && end == false)
        {
            end = true;
            StartCoroutine(LoadScene());
        }

    }

    public void PassScript()
    {
        scriptManager.scriptNum++;
    }

    public void AfterTurnButtonClick(int caseNum)
    {
        if(caseNum == 0)
        {
            if (scriptManager.isHide == false)
            {
                StartCoroutine(scriptMove.HideAnim());
                tileController.GetMovableTile(EnemyInfo.unitTile, EnemyInfo.UnitActPoint);
                tileController.ResetSelectingTile();
                EnemyMove(EnemyInfo.unitTile, tileController.tileList[18], 2);
            }
          
        }
        else
        {
            if (scriptManager.isHide == false)
            {
                StartCoroutine(scriptMove.HideAnim());
                tileController.GetMovableTile(EnemyInfo.unitTile, EnemyInfo.UnitActPoint);
                tileController.ResetSelectingTile();
                EnemyMove(EnemyInfo.unitTile, tileController.tileList[17], 1);
            }

        }
    }

    public void AfterEnemyTurnScript()
    {
        scriptManager.scriptNum++;
        buttonController.OnClickTurnButton();
        buttonController.ClickTurn();
        tileController.Occupy();

        MarsInfo.UnitActPoint = 3;
        EnemyInfo.UnitActPoint = 2;

        if (scriptManager.isHide == true)
        {
            StartCoroutine(scriptMove.OnAnim());
        }
    }

    public void SpawnTileSelecting()
    {
        spawnTile.spawnSelecting = true;
        spawnTile.StartCoroutine(spawnTile.ChangeColor());
    }

    public void ClickAttackButton()
    {
        EnemyInfo.isDamaed = true;
        attackEnemy = true;
    }

    void OnOffPointer()
    {
        if (scriptManager.scriptNum == 1)
        {
            pointer.pointerIndex = 0;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum >= 3 && scriptManager.scriptNum <= 4)
        {
            pointer.pointerIndex = 1;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 5)
        {
            pointer.pointerIndex = 2;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 11)
        {
            pointer.pointerIndex = 3;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 12)
        {
            pointer.pointerIndex = 4;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 13)
        {
            pointer.pointerIndex = 2;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 17)
        {
            pointer.pointerIndex = 4;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 18)
        {
            pointer.pointerIndex = 5;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 21 && attackEnemy == false)
        {
            pointer.pointerIndex = 6;
            pointer.OnOffPointer();
        }
        else if (scriptManager.scriptNum == 21 && attackEnemy == true)
        {
            pointer.pointerIndex = 7;
            pointer.OnOffPointer();
        }
        else
        {
            pointer.pointerIndex = 9;
            pointer.OnOffPointer();
        }

    }

    public IEnumerator LoadScene()
    {
        loadingCanvas.gameObject.SetActive(true);
        float time = 0;
        AsyncOperation async = SceneManager.LoadSceneAsync("Lobby_Renewal");
        async.allowSceneActivation = false;

        while (time <= 3 && !async.isDone)
        {
            time += Time.deltaTime;
            yield return null;
        }
        async.allowSceneActivation = true;
        loadingCanvas.gameObject.SetActive(false);
        
    }
}
