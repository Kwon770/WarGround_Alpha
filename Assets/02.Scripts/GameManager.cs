using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager manager;

    [SerializeField] public Transform SpawnButton;
    [SerializeField] public GameObject AttackButton;
    [SerializeField] public GameObject MoveButton;

    enum ObjTpye
    {
        Unit,
        Tile
    }ObjTpye Type;
    enum Trigger
    {
        None,
        Attack,
        Move,
        Spawn
    }
    Trigger trigger;
    bool enemy;
    GameObject Obj;

    private void Awake()
    {
        manager = this;
    }

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
        else if (Type == "Spawn")
        {
            trigger = Trigger.Spawn;
        }
    }
    public void ResetTrigger()
    {
        trigger = Trigger.None;
        Obj = null;

        //모든 UI제거
        SpawnButton.gameObject.SetActive(false);
        AttackButton.SetActive(false);
        MoveButton.SetActive(false);
    }
    void Input_Computer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Click(hit);
            }
        }
    }

    //처리
    void Click(RaycastHit hit)
    {
        //유닛 클릭했을때
        if (hit.collider.tag == "Unit")
        {
            Debug.Log("유닛클릭 : " + hit.collider.gameObject + " " + trigger);
            //공격 할때
            if (Obj != null && !enemy && trigger == Trigger.Attack)
            {
                //Obj->hit.transform.gameObject 공격
                Attack(Obj, hit.transform.gameObject);
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
                Obj.GetComponent<UnitInfo>().UI();
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
                return;
            }
            else if(Obj != null && !enemy && trigger == Trigger.Spawn)
            {
                Spawn.spawn.UnitSpawn(hit.transform.GetComponent<TileInfo>());
                ResetTrigger();
                return;
            }
            if (Obj == null)
            {
                //혹시 예정 된다면 타일 정보 출력
            }
        }
    }

    //이동 및 공격, 방어 행동 연산
    void Move(GameObject Unit, GameObject Tile)
    {
        Debug.Log(Unit + " " + Tile);
        UnitInfo unit = Unit.GetComponent<UnitInfo>();
        TileInfo SP = GameData.data.FindTile(unit.x, unit.y);
        TileInfo EP = Tile.GetComponent<TileInfo>();

        List<TileInfo> path = Calculator.Calc.Move(SP, EP, unit.Act);

        Debug.Log(path);
        if (path == null) return;
        if (unit.move != null) StopCoroutine(unit.move);
        unit.move = StartCoroutine(unit.Move(path));
    }
    void Attack(GameObject Attacker,GameObject Defender)
    {
        UnitInfo attacker = Attacker.GetComponent<UnitInfo>();
        UnitInfo defender = Defender.GetComponent<UnitInfo>();

        int range = Calculator.Calc.Range(GameData.data.FindTile(attacker.x, attacker.y), GameData.data.FindTile(defender.x, defender.y), attacker.range);
        if (range <= attacker.range && range !=-1)
        {
            //닌자일 경우
            if (defender.Kinds == "Ninja" && range > 1) defender.anim.photonView.RPC("EVASION", PhotonTargets.All);

            else defender.photonView.RPC("GetDemage", PhotonTargets.All, attacker.ATK + attacker.AddATK);
            attacker.photonView.RPC("Attack", PhotonTargets.All,defender.x,defender.y);
        }
    }
}
