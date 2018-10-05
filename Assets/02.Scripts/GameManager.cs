using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    enum ObjTpye
    {
        Unit,
        Tile
    }ObjTpye Type;
    enum Trigger
    {
        Attack,
        Move,
        None,
    }Trigger trigger;
    bool enemy;
    GameObject Obj;


    void Update()
    {
        Input_Computer();
    }
    //입력
    public void SetTrigger(string Type)
    {
        if (Type == "Attack")
        {
            trigger = Trigger.Attack;
        }
        else if (Type == "Move")
        {
            trigger = Trigger.Move;
        }
    }
    public void ResetTrigger()
    {
        trigger = Trigger.None;
        Obj = null;
        //모든 UI제거
    }
    void Input_Computer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                //유닛 클릭했을때
                if (hit.collider.tag == "Unit")
                {
                    //공격 할때
                    if (Obj != null && !enemy && trigger==Trigger.Attack)
                    {
                        //Obj->hit.transform.gameObject 공격
                        ResetTrigger();
                        return;
                    }
                    enemy = hit.transform.GetComponent<UnitInfo>().Owner == PhotonNetwork.playerName ? false : true;
                    Type = ObjTpye.Unit;
                    Obj = hit.transform.gameObject;

                    //적 클릭했을때
                    if (enemy)
                    {
                        //적 정보 UI보여주기
                        ResetTrigger();
                    }
                    //아군 클릭했을때
                    else
                    {
                        //아군 UI보여주기
                    }
                }
                //타일 클릭했을때
                else if (hit.collider.tag == "Tile")
                {
                    //이동 할때
                    if (Obj != null && !enemy && trigger == Trigger.Move)
                    {
                        //Obj가 hit.transform.gameObject 으로 이동
                        Move(Obj, hit.transform.gameObject);
                        ResetTrigger();
                    }
                    if (Obj == null)
                    {
                        //혹시 예정 된다면 타일 정보 출력
                    }
                }
            }
        }
    }

    //이동 및 방어 행동 연산
    void Move(GameObject Unit, GameObject Tile)
    {
        UnitInfo unit = Unit.GetComponent<UnitInfo>();
        TileInfo SP = GameData.data.FindTile(unit.x, unit.y);
        TileInfo EP = Tile.GetComponent<TileInfo>();

        List<TileInfo> path = Calculator.Calc.Move(SP, EP, unit.Act);

        if (path == null) return;

        unit.Move(path);
    }
}
