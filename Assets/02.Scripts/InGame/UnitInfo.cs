using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : Photon.MonoBehaviour {

    public Anim anim;

    public Vector3 currrentPos;
    public Quaternion currentQuater;

    UnitInfo temp;

    public bool moveTrigger;
    public string Owner;
    public int x;
    public int y;
    
    public Coroutine move;

    public ParticleScript particle;

    [SerializeField] bool dieTrigger;

    [SerializeField] public string Kinds;

    [SerializeField] bool CanSpawn;

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

    [SerializeField] public Sprite UnitIcon;
    [SerializeField] public Sprite SkillIcon;

    // Use this for initialization

    //초기화
    void Awake()
    {
        StartCoroutine("Setting");
    }
    IEnumerator Setting()
    {
        dieTrigger = false;
        particle = GetComponent<ParticleScript>();
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

        //방어 무시일 경우
        if (attacker.Kinds == "Boatswain")
        {
            HP -= demage;
            if (HP <= 0) dieTrigger = true;
            return;
        }

        //무녀 데미지 경감
        if (Calculator.Calc.UnitInRange(Owner, "Miko", 1, GameData.data.FindTile(this.x, this.y)))
        {
            particle.HealPlay();
            demage--;
        }

        //힐일때
        if (attacker.Kinds == "Healer")
        {
            HP += demage;
            HP = MaxHP < HP ? HP : MaxHP;
            return;
        }

        //힘줄끊기
        if(attacker.Kinds == "Dokugawa")
        {
            ATK = 1;
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
        if (HP <= 0)
        {
            //attacker가 워락인 경우
            if (attacker.Kinds == "Warlock")
            {
                attacker.AddATK += 1;
                if (attacker.AddATK > 5) attacker.AddATK = 5;
                //attacker.공격력 강화 이펙트
            }
            //defender가 버서커일 경우
            if(this.Kinds == "Berserker")
            {
                Debug.Log("ddddddddddddddddddddddddddddddddddddddddddddddddddd");
                HP = 1;
                ATK += 3;
                this.Kinds = " Berserker ";
            }
            dieTrigger = true;
        }
        if (Kinds == "Guarder") SHD += 2;
    }

    //애니매이션 재생
    IEnumerator Animation(UnitInfo temp)
    {
        float time = 0;

        this.temp = temp;

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
    }

    public void hit()
    {
        //피격음
        if (Kinds == "Healer")
        {
            SoundManager.soundmanager.heal();
        }
        else if(Kinds == "Warlock")
        {
            SoundManager.soundmanager.magicAttack();
        }
        else if(Kinds == "Banshee" || Kinds == "Archer")
        {
            SoundManager.soundmanager.archerAttack();
        }
        else if(Kinds == "BadOne" || Kinds == "GoodOne" || Kinds == "StrangeOne" || Kinds == "Winvelt" || Kinds == "Boatswain")
        {
            SoundManager.soundmanager.shootGun();
        }
        else
        {
            SoundManager.soundmanager.hammer();
        }

        //애니메이션
        if (temp.dieTrigger)
        {
            temp.particle.DemagePlay();
            temp.DIE();
        }
        else
        {
            if (Kinds != "Healer")
            {
                temp.particle.DemagePlay();
                temp.anim.Block();
            }
            else
            {
                temp.particle.HealPlay();
                temp.anim.HEAL();
            }
        }
    }

    public void Step()
    {
        SoundManager.soundmanager.planeFootStep();
    }

    //공격
    [PunRPC]
    public void Attack(int x,int y)
    {
        UnitInfo temp = GameData.data.FindUnit(x, y);
        StartCoroutine(Animation(temp));
    }

    //이동
    public IEnumerator Move(List<TileInfo> path)
    {
        Vector3 endPos;
        Vector3 startPos;
        Quaternion startRot;
        Quaternion endRot;

        Act -= (path.Count - 1);

        anim.Move();//이동 애니메이션 시작

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
         
            yield return new WaitForSeconds(0.5f);
        }
        anim.Stop();//이동 애니메이션 끝
        moveTrigger = false;
    }

    //피가 0 이하일때
    public void DIE()
    {
        anim.DIE();

        if (!photonView.isMine) return;

        if (HP == 1 && Kinds == " Berserker ")
        {
            Debug.Log("부활");
            //부활
            photonView.RPC("Rise", PhotonTargets.All);
            return;
        }
        if (Calculator.Calc.UnitInRange(GameData.data.EnemyName, "Banshee", 2, GameData.data.FindTile(x, y)) && Kinds != "SkullKnight")
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

    public void ChangeForm()
    {
        Debug.Log(HP);
        if (HP != 2) return;
        particle.DeadPlay();
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh = GameData.data.SkullKnight;
    }

    [PunRPC]
    public void Rise()
    {
        if (HP == 1)
        {
            anim.RISE();
            return;
        }
        if (GetComponent<Synchro>() == null)
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