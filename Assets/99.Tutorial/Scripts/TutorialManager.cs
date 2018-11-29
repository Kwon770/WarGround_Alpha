using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    

    [SerializeField] public UnitInfoTutorial unit;
    [SerializeField] int startLocX = 0;    //현재 선택한 유닛이 있는 타일의 좌표, 시작점
    [SerializeField] int startLocY = 0;

    [SerializeField] int goToLocX = 0;    //가야할 타일의 X좌표 저장해두는 공간, 끝 점
    [SerializeField] int goToLocY = 0;

    [SerializeField] int listFirst;   //리스트에서 2칸 이상 갈때, 다시 스타트블럭을 잡아줌
    [SerializeField] int listSize;    //리스트의 총 사이즈를 담아둠
    [SerializeField] int listSwitch;    //리스트의 시작점을 잡아줌

    [SerializeField] GameObject[] tileSave = new GameObject[34];
    [SerializeField] public TileInfoTutorial[] tileSaveInfo = new TileInfoTutorial[34];
    [SerializeField] public UnitInfoTutorial selectUnit; // 지금 현재 고른 유닛 표시

    [SerializeField] public bool canClick = true;


    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 이제부터 본 게임
    [SerializeField] ScriptManager scriptManager;
    [SerializeField] public UnitInfoTutorial enemy; // 지금 현재 고른 유닛 표시
    [SerializeField] GameObject selectEffect;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject warrior;
    [SerializeField] bool attackSwitch;
    /*[SerializeField] Text actPointText;
    [SerializeField] Text actPointParent;*/ //테스트 하고 지우기 11/29

    [SerializeField] int myTurn = 0;      //0 :내턴   1: 니턴
     // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ UI 전용


    [SerializeField] int commandPower = 0;
    [SerializeField] int bitinium = 0;
    [SerializeField] int bitiniumRegen = 0;

    [SerializeField] public int ATK;
    [SerializeField] public int HP;
    [SerializeField] public int SHD;
    [SerializeField] public int actPoint;
    [SerializeField] public string unitName;

    [SerializeField] Text commandPowerText;
  
    [SerializeField] Text bitiniumRegenText;

    [SerializeField] Text ATKtext;
    [SerializeField] Text HPtext;
    [SerializeField] Text SHDtext;
    [SerializeField] Text Nametext;

    [SerializeField] GameObject commandUi;
    [SerializeField] GameObject actPointUi;
    [SerializeField] GameObject bitiniumUi;

    enum selectButton { Atk, Move };

    private void Start()
    {

        for (int i = 0; i < 34; i++)
        {
            tileSaveInfo[i] = tileSave[i].GetComponent<TileInfoTutorial>();
        }

    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space) && selectUnit.movingEnd == true) 이제는 쓸모없는 친구
        {
            GetGotoTile(selectUnit.startPoint, selectUnit.actPoint);
        }*/

        
        ATK = selectUnit.ATK;
        HP = selectUnit.HP;
        SHD = selectUnit.SHD;
        actPoint = selectUnit.actPoint;
        unitName = selectUnit.unitName;

        ATKtext.text = ATK.ToString();
        HPtext.text = HP.ToString();
        SHDtext.text = SHD.ToString();
        bitiniumRegenText.text = bitiniumRegen.ToString();
        Nametext.text = unitName;

        selectEffect.transform.position = selectUnit.transform.position;

        for(int i = 0; i < 3 ; i++)
        {
            if (i < actPoint)
            {
                actPointUi.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                actPointUi.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }

        for (int i = 0; i < 10; i++)
        {
            if (i < bitinium )
            {
                bitiniumUi.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                bitiniumUi.transform.GetChild(i).gameObject.SetActive(false);
            }

        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (hit.transform.tag == "Tile")
                {
                   

                    TileInfoTutorial rayTile = hit.transform.GetComponent<TileInfoTutorial>();
                    

                    if (rayTile.selectTile == true)
                    {
                        if(scriptManager.textNumber == 15)
                        {
                            AllTileBreak();

                            warrior.SetActive(true);
                            unit.actPoint--;
                            scriptManager.canSkip = true;
                            scriptManager.textNumber++;
                            scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
                            scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;
                        }
                        else if(scriptManager.textNumber == 17)
                        {
                            AllTileBreak();
                            
                            enemy.StartCoroutine(enemy.Damaged());
                            enemy.StartCoroutine(unit.Attack());
                            Debug.Log("17발동 타일 눌러서");
                        }
                        else
                        {
                            AfterBfsMove(rayTile, rayTile.stage);
                            AllTileBreak();
                        }
                        
                    }
                    
                    
                }
                if (hit.transform.tag == "Unit" )
                {
                    if (scriptManager.textNumber == 1 || scriptManager.textNumber == 15)
                    {
                        return;
                        
                    }
                    else if (scriptManager.textNumber == 17)
                    {
                        AllTileBreak();
                        
                        enemy.StartCoroutine(enemy.Damaged());
                        enemy.StartCoroutine(unit.Attack());
                        Debug.Log("17발동 유닛 눌러서");
                    }
                    else
                    {
                        selectUnit = hit.transform.GetComponent<UnitInfoTutorial>();
                    }

                    
                    
                }
            }
            
        } //stage

    }


    public void GetGotoTile(TileInfoTutorial startTile, int actPoint)
    {


        if (actPoint == 0)
        {
            return;
        }

        startTile.firstTile = true;

        listFirst = 0;
        listSize = 1;
        listSwitch = 0;



        List<TileInfoTutorial> tileList = new List<TileInfoTutorial>();
        tileList.Add(startTile);

        int tempStage = 0; //이것은 경로 추적에 중요하다

        for (int i = 0; i < actPoint; i++) //행동력만큼 반복하며
        {
            
           

            for (int j = listSwitch; j < listSize; j++) //리스트에 들어있는 타일을 기준으로 찾으며
            {
                startLocX = tileList[j].X;
                startLocY = tileList[j].Y;
               


                for (int k = 0; k < 34; k++) //그것을 나의 타일들과 비교하여 맞는 조건의 타일을 구한다
                {
                   
                    if (tileList[j].X % 2 == 0)
                    {
                        if (tileSaveInfo[k].X == startLocX && tileSaveInfo[k].Y == startLocY - 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            
                            
                        }
                        if (tileSaveInfo[k].X == startLocX && tileSaveInfo[k].Y == startLocY + 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                           
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            

                        }
                        if (tileSaveInfo[k].X == startLocX+1 && tileSaveInfo[k].Y == startLocY + 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            

                        }
                        if (tileSaveInfo[k].X == startLocX + 1 && tileSaveInfo[k].Y == startLocY && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            

                        }
                        if (tileSaveInfo[k].X == startLocX - 1 && tileSaveInfo[k].Y == startLocY + 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            

                        }
                        if (tileSaveInfo[k].X == startLocX - 1 && tileSaveInfo[k].Y == startLocY && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            

                        }
                    }
                    else
                    {
                        if (tileSaveInfo[k].X == startLocX && tileSaveInfo[k].Y == startLocY - 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;

                            
                        }
                        if (tileSaveInfo[k].X == startLocX && tileSaveInfo[k].Y == startLocY + 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            
                        }
                        if (tileSaveInfo[k].X == startLocX + 1 && tileSaveInfo[k].Y == startLocY - 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            
                        }
                        if (tileSaveInfo[k].X == startLocX + 1 && tileSaveInfo[k].Y == startLocY && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            
                        }
                        if (tileSaveInfo[k].X == startLocX - 1 && tileSaveInfo[k].Y == startLocY - 1 && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            
                        }
                        if (tileSaveInfo[k].X == startLocX - 1 && tileSaveInfo[k].Y == startLocY && tileSaveInfo[k].selectTile == false && tileSaveInfo[k].firstTile == false)
                        {
                            tileList.Add(tileSaveInfo[k]);
                            tileSaveInfo[k].selectTile = true;
                            tileSaveInfo[k].path = tileList[j];
                            tileSaveInfo[k].stage = tempStage;
                            listFirst++;
                            
                        }
                    }


                } // 6개 구해주는 for문

                
            }
            listSize = tileList.Count;
            listSwitch = listSize - listFirst;
            listFirst = 0;


            tempStage++;
            
        }


        startTile.firstTile = false;


        if (scriptManager.textNumber == 6)
        {
            AfterBfsMove(tileSaveInfo[18], tileSaveInfo[18].stage);
            AllTileBreak();
        }
        else if(scriptManager.textNumber == 14)
        {
            AfterBfsMove(tileSaveInfo[17], tileSaveInfo[17].stage);
            AllTileBreak();
        }
        else
        {
            for (int i = 0; i < 34; i++)
            {
                if (i != 16)
                {
                    tileSaveInfo[i].selectTile = false;
                }
            }
        }
        
    } //범위를 잡아줌



    void AfterBfsMove(TileInfoTutorial endTile, int tileStage)
    {
        
        TileInfoTutorial pathSave = endTile;
        List<TileInfoTutorial> tileList = new List<TileInfoTutorial>();

        if(tileStage == 0)
        {
            tileList.Add(pathSave);
            selectUnit.actPoint--;
            Debug.Log("aaa");
        }
        else
        {
            tileList.Add(pathSave);
            selectUnit.actPoint--;
            for (int i = 0; i < tileStage; i++)
            {
                tileList.Add(pathSave.path);

                selectUnit.actPoint--;
                pathSave = pathSave.path;
            }
        }
        

        StartCoroutine(selectUnit.UnitMove(tileList, tileStage, selectUnit));
        
    } //실제 최소 경로 찾아 이동시켜줌

    void AllTileBreak()   //명령 후 모든 타일 초기화
    {
        for (int i = 0; i < 34; i++)
        {
            tileSaveInfo[i].selectTile = false;
        }
    }


    public void ClickMoveButton()
    {
        if (canClick != true || scriptManager.textNumber != 0 || selectUnit == enemy)
        {
            Debug.Log("클릭 안됨 이동");
            return;
        }

        if (selectUnit.movingEnd != true)
        {
            return;
        }

        

        GetGotoTile(selectUnit.startPoint, selectUnit.actPoint);

        //여기도 임시 땜빵 일단 쓰고 있으셈
        scriptManager.canSkip = true;
        scriptManager.textNumber++;
        scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
        scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;
    }

    public void ClickTurnButton()
    {
        if (canClick != true)
        {
            Debug.Log("클릭 안됨");
            return;
        }

        if (selectUnit.movingEnd != true)
        {
            return;
        }
        Debug.Log("클릭됨");

        if (scriptManager.textNumber == 5)
        {
            scriptManager.canSkip = true;
            scriptManager.textNumber++;
            scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
            scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;
            selectUnit = enemy;
            GetGotoTile(selectUnit.startPoint,selectUnit.actPoint);

            unit.actPoint = 3;
            enemy.actPoint = 2;
        }
        else if (scriptManager.textNumber == 13)
        {
            scriptManager.canSkip = true;
            scriptManager.textNumber++;
            scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
            scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;

           


            selectUnit = enemy;
            GetGotoTile(selectUnit.startPoint, selectUnit.actPoint);

            unit.actPoint = 0;
        }
        else
        {
            return;
        }


    }

    public void ClickWarriorButton()
    {
        if (canClick != true || selectUnit == enemy)
        {
            Debug.Log("클릭 안됨");
            return;
        }

        if (selectUnit.movingEnd != true)
        {
            return;
        }
        Debug.Log("클릭됨");

        if (scriptManager.textNumber == 15)
        {

            
            tileSaveInfo[24].selectTile = true;
        }
        
        else
        {
            return;
        }


    }

    public void ClickAttackButton()
    {
        if (canClick != true || selectUnit == enemy)
        {
            
            return;
        }

        if (selectUnit.movingEnd != true)
        {
            return;
        }
        

        if (scriptManager.textNumber == 17 && selectUnit == unit && attackSwitch == false)
        {

            attackSwitch = true;
            tileSaveInfo[17].selectTile = true;
        }

        else
        {
            return;
        }


    }

}