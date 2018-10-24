using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : Photon.MonoBehaviour {

    Anim anim;

    public Vector3 currrentPos;
    public Quaternion currentQuater;

    public bool moveTrigger;
    public string Owner;
    public int x;
    public int y;

    [SerializeField] float delay;

    public Coroutine move;

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
    IEnumerator Setting()
    {
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
    void Awake()
    {
        Debug.Log("셋팅하기" + " " + photonView.isMine);
        StartCoroutine("Setting");
    }
    public void SetOwner(string name)
    {
        Owner = name;
    }

    //리셋
    public void ResetTurn()
    {
        Act = MaxAct;
    }

    //데미지받기
    [PunRPC]
    public void GetDemage(int ATK)
    {
        if (!photonView.isMine) return;

        //방어 무시일 경우
        if (ATK < 0)
        {
            HP += ATK;
            return;
        }

        //방어무시가 아닐경우
        if (SHD > 0)
        {
            if (ATK >= SHD)
            {
                ATK -= SHD;
                SHD = 0;
            }
            else
            {
                SHD -= ATK;
                ATK = 0;
            }
        }
        HP -= ATK;
    }

    //애니매이션 재생
    IEnumerator Animation(float delayTime, UnitInfo temp)
    {
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
        if (temp.HP > 0) temp.anim.Block();
        else temp.DIE();
        
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

        for (int i = 1; i < path.Count; i++)
        {
            float time = 0;
            endPos = path[i].transform.position;
            startPos = transform.position;

            startRot = transform.rotation;

            endRot = Quaternion.LookRotation(path[i].transform.position - transform.position);

            x = path[i].x;
            y = path[i].y;

            anim.Move();//이동 애니메이션 시작
            time = 0;
            while (Quaternion.Angle(endRot , transform.rotation)>5)
            {
                transform.rotation = Quaternion.Lerp(startRot, endRot, time);
                time += Time.deltaTime * rotSpeed;
                yield return null;
            }
            time = 0;
            while (Vector3.Magnitude(transform.position-endPos)>0.1)
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

    //사망
    public void DIE()
    {
        GameData.data.DelUnit(this);
        anim.DIE();
        Destroy(gameObject,4f);
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
