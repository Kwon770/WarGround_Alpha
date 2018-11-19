using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    


    [SerializeField] int myTurn = 0;      //0 : 아무것도 못함

    [SerializeField] UnitInfoTutorial unit;
    [SerializeField] int startLocX = 0;    //현재 선택한 유닛이 있는 타일의 좌표, 시작점
    [SerializeField] int startLocY = 0;

    [SerializeField] int goToLocX = 0;    //가야할 타일의 X좌표 저장해두는 공간, 끝 점
    [SerializeField] int goToLocY = 0;

    [SerializeField] int listFirst;   //리스트에서 2칸 이상 갈때, 다시 스타트블럭을 잡아줌
    [SerializeField] int listSize;    //리스트의 총 사이즈를 담아둠
    [SerializeField] int listSwitch;    //리스트의 시작점을 잡아줌

    [SerializeField] GameObject[] tileSave = new GameObject[34];
    [SerializeField] TileInfoTutorial[] tileSaveInfo = new TileInfoTutorial[34];
    [SerializeField] UnitInfoTutorial selectUnit; // 지금 현재 고른 유닛 표시


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
        if(Input.GetKeyDown(KeyCode.Space) && selectUnit.movingEnd == true)
        {
            GetGotoTile(selectUnit.startPoint, selectUnit.actPoint);
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

                    Debug.Log("a");

                    if (rayTile.selectTile == true)
                    {
                        AfterBfsMove(rayTile, rayTile.stage);
                        AllTileBreak();
                    }
                    
                    
                }
                if (hit.transform.tag == "Unit")
                {
                    selectUnit = hit.transform.GetComponent<UnitInfoTutorial>();
                }
            }
            
        } //stage

    }


    void GetGotoTile(TileInfoTutorial startTile, int actPoint)
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
            Debug.Log("tttt" + tempStage);
        }


        startTile.firstTile = false;
    }



    void AfterBfsMove(TileInfoTutorial endTile, int tileStage)
    {
        
        TileInfoTutorial pathSave = endTile;
        List<TileInfoTutorial> tileList = new List<TileInfoTutorial>();

        if(tileStage == 0)
        {
            tileList.Add(pathSave);
            Debug.Log("aaa");
        }
        else
        {
            tileList.Add(pathSave);
            for (int i = 0; i < tileStage; i++)
            {
                tileList.Add(pathSave.path);
                pathSave = pathSave.path;
            }
        }
        /*else if(tileStage == 1)
        {
            tileList.Add(pathSave);
            tileList.Add(pathSave.path);

        }*/

        StartCoroutine(UnitMove(tileList, tileStage, selectUnit));
        
    }

    IEnumerator UnitMove(List<TileInfoTutorial> moveList, int tileStage, UnitInfoTutorial moveUnit)   //유닛이 이동하는데 딜레이 있게 이동한다
    {
        if (tileStage == 0)
        {
            moveUnit.endPoint = moveList[0];
            moveUnit.movingEnd = false;
            yield return new WaitForSeconds(1.0f);
            moveUnit.movingEnd = true;
        }
        else
        {
            for (int i = tileStage; i >= 0; i--)
            {
                moveUnit.movingEnd = false;
                
                moveUnit.endPoint = moveList[i];
                yield return new WaitForSeconds(1.0f);
            }
            moveUnit.movingEnd = true;
        }
        
        
    }

    void AllTileBreak()   //명령 후 모든 타일 초기화
    {
        for (int i = 0; i < 34; i++)
        {
            tileSaveInfo[i].selectTile = false;
        }
    }
}