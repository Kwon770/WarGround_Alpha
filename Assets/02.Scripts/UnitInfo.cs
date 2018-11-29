using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : Photon.MonoBehaviour {

    public Anim anim;

    public Vector3 currrentPos;
    public Quaternion currentQuater;

    public bool moveTrigger;
    public string Owner;
    public int x;
    public int y;
    
    public Coroutine move;

    [SerializeField] bool dieTrigger;

    [SerializeField] public string Kinds;

    [SerializeField] bool CanSpawn;

    [SerializeField] float delay;

    [SerializeField] public int ATK;
    [SerializeField] public int AddATK;

    [SerializeField] public int MaxHP;
    [SerializeField] public int HP;

    [SerializeField] public int SHD;

    [SerializeField] public int MaxAct;
    [SerializeField] public int Act;

    [SerializeField] public int range;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float rotSpeed;
    // Use this for initialization

    //초기화
    void Awake()
    {
        Debug.Log("셋팅하기" + " " + photonView.isMine);
        StartCoroutine("Setting");
    }
    IEnumerator Setting()
    {
        dieTrigger = false;
        anim = GetComponent<Anim>();
        DontDestroyOnLoad(gameObject);
        moveTrigger = false;

        if (!photonView.isMine)
        {
            gameObject.AddComponent<Synchro>();
        }
        while (true)
        {
            if (GameData.data != null)
            {
                GameData.data.AddUnit(this);
                break;
            }
            yield return null;
        }
    }
    public void SetOwner(string name)
    {
        Owner = name;
    }

    public void UI()
    {
        GameManager.manager.AttackButton.SetActive(true);
        GameManager.manager.MoveButton.SetActive(true);
        if (CanSpawn)
        {
            GetComponent<Spawn>().Setting();
        }
    }

    //리셋
    public void ResetTurn()
    {
        Act = MaxAct;
    }

    //데미지받기
    [PunRPC]
    public void GetDemage(int demage, int x, int y)
    {
        UnitInfo attacker = GameData.data.FindUnit(x, y);
//        if (!photonView.isMine) return;

        //방어 무시일 경우
        if (attacker.Kinds == "Boatswain")
        {
            HP -= demage;
            if (HP <= 0) dieTrigger = true;
            return;
        }

        //방어무시가 아닐경우
        if (SHD > 0)
        {
            if (demage >= SHD)
            {
                demage -= SHD;
                SHD = 0;
            }
            else
            {
                SHD -= demage;
                demage = 0;
            }
        }
        HP -= demage;
        if (HP <= 0) dieTrigger = true;

        Debug.Log(gameObject + " " + dieTrigger);
        if (Kinds == "Guarder") SHD += 2;
    }

    //애니매이션 재생
    IEnumerator Animation(float delayTime, UnitInfo temp)
    {
        Debug.Log(temp.transform.position + " " + transform.position);
        Debug.Log(temp);

        float time = 0;
        Quaternion startRot1 = transform.rotation;
        Quaternion endRot1 = Quaternion.LookRotation(temp.transform.position - transform.position);
        Quaternion startRot2 = temp.transform.rotation;
        Quaternion endRot2 = Quaternion.LookRotation(transform.position - temp.transform.position);
        while (time<1)
        {
            transform.rotation = Quaternion.Lerp(startRot1, endRot1, time);
            temp.transform.rotation = Quaternion.Lerp(startRot2, endRot2, time);
            time += Time.deltaTime * rotSpeed;
            yield return null;
        }
        anim.Attack();//가격 애니메이션 실행
        yield return new WaitForSeconds(delayTime);
        Debug.Log(gameObject + " " + dieTrigger);
        if(temp.dieTrigger) temp.DIE();
        else
        {
            if (Kinds == "Healer") temp.anim.HEAL();
            else temp.anim.Block();
        }
    }

    //공격
    [PunRPC]
    public void Attack(int x,int y)
    {
        UnitInfo temp = GameData.data.FindUnit(x, y);
        StartCoroutine(Animation(delay, temp));
    }

    //이동
    public IEnumerator Move(List<TileInfo> path)
    {
        Vector3 endPos;
        Vector3 startPos;
        Quaternion startRot;
        Quaternion endRot;

        Act -= (path.Count - 1);

        for (int i = 1; i < path.Count; i++)
        {
            if(Kinds == "ChiefMate") GameData.data.FindTile(path[i - 1].x, path[i - 1].y).cost=0;

            float time = 0;
            endPos = path[i].transform.position;
            startPos = transform.position;

            startRot = transform.rotation;

            endRot = Quaternion.LookRotation(path[i].transform.position - transform.position);

            x = path[i].x;
            y = path[i].y;

            anim.Move();//이동 애니메이션 시작
            time = 0;
            while (time <= 1)
            {
                transform.rotation = Quaternion.Lerp(startRot, endRot, time);
                time += Time.deltaTime * rotSpeed;
                yield return null;
            }
            time = 0;
            while (time <= 1)
            {
                transform.position = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime * moveSpeed;
                yield return null;
            }
            anim.Stop();//이동 애니메이션 끝
            yield return new WaitForSeconds(0.5f);
        }
        moveTrigger = false;
    }

    //피가 0 이하일때
    public void DIE()
    {
        anim.DIE();
        if (!photonView.isMine) return;
        if (Banshee() && Kinds != "SkullKnight") 
        {
            //부활
            photonView.RPC("Rise", PhotonTargets.All);
            return;
        }
        //사망처리
        else
        {
            photonView.RPC("DestroyUnit", PhotonTargets.All);
        }
    }

    //사망처리
    [PunRPC]
    public void DestroyUnit()
    {
        GameData.data.DelUnit(this);
        Destroy(gameObject, 4f);
    }

    //부활 관련 코드
    public bool Banshee()
    {
        Debug.Log("부활확인");
        // 밴시 부활 여부 확인
        foreach (UnitInfo unit in GameData.data.Units)
        {

            Debug.Log(unit.Kinds + " : " + (unit.Owner != Owner) + " : " + unit.gameObject);
            if (Kinds == "SkullKnight") break;
            if (unit.Kinds == "Banshee" && unit.Owner != Owner)
            {

                int range = Calculator.Calc.Range(GameData.data.FindTile(x, y), GameData.data.FindTile(unit.x, unit.y), 2);

                Debug.Log(range);

                if (range <= 2 && range != -1) 
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ChangeForm()
    {
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh = GameData.data.SkullKnight;
    }

    [PunRPC]
    public void Rise()
    {
        if (GetComponent<Synchro>()==null)
        {
            gameObject.AddComponent<Synchro>();
        }
        else
        {
            Destroy(GetComponent<Synchro>());
            photonView.RequestOwnership();
            Kinds = "SkullKnight";
            Owner = PhotonNetwork.playerName;
            if (ATK == -2) ATK = -1;
            HP = 2; MaxHP = 2; SHD = 2; range = 1; AddATK = 0; MaxAct = 2;
        }
        anim.RISE();
    }

    private void OnDestroy()
    {
        Debug.Log(this + "오브젝트 사망");
    }

    //스텟 동기화
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //좌표 동기화
            stream.SendNext(x);
            stream.SendNext(y);

            //스텟 동기화
            stream.SendNext(HP);
            stream.SendNext(SHD);
            stream.SendNext(Act);
            stream.SendNext(Owner);
        }
        else
        {
            //좌표 동기화
            x = (int)stream.ReceiveNext();
            y = (int)stream.ReceiveNext();

            //스텟 동기화
            HP = (int)stream.ReceiveNext();
            SHD = (int)stream.ReceiveNext();
            Act = (int)stream.ReceiveNext();
            Owner = (string)stream.ReceiveNext();
        }
    }
}