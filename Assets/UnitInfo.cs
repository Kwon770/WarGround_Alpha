using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : Photon.MonoBehaviour {

    public Vector3 currrentPos;
    public Quaternion currentQuater;

    public bool moveTrigger;
    public string Owner;
    public int x;
    public int y;

    public Coroutine move;

    [SerializeField] public int ATK;

    [SerializeField] public int MaxHP;
    [SerializeField] public int HP;

    [SerializeField] public int SHD;

    [SerializeField] public int MaxAct;
    [SerializeField] public int Act;

    [SerializeField] public int range;

    [SerializeField] float moveSpeed;
    // Use this for initialization

    //초기화
    void Awake () {
        DontDestroyOnLoad(gameObject);
        moveTrigger = false;
        currrentPos = transform.position;
        currentQuater = transform.rotation;
        if (!photonView.isMine)
        {
            gameObject.AddComponent<Synchro>();
        }
    }
    public void SetOwner(string name)
    {
        Owner = name;
        Debug.Log("mine? : " + photonView.isMine);
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
        //방어 무시일 경우
        if (ATK < 0)
        {
            HP += ATK;
            return;
        }

        //방어무시가 아닐경우
        if (SHD > 0)
        {
            ATK -= SHD;
            if (ATK <= 0) return;
        }
        HP -= ATK;
    }

    //이동
    public IEnumerator Move(List<TileInfo> path)
    {
        Debug.Log("이동시작");
        int pathindex = 0;
        Vector3 pos=path[pathindex].GetComponent<Transform>().position;
        while (true)
        {
            Debug.Log("이동중");
            if (pos == transform.position)
            {
                x = path[pathindex].x;
                y = path[pathindex].y;
                if (path.Count<=++pathindex)break;
                pos = path[pathindex].GetComponent<Transform>().position;
            }
            transform.position = Vector3.Lerp(transform.position, path[pathindex].GetComponent<Transform>().position, Time.deltaTime * moveSpeed);
            yield return null;
        }
        moveTrigger = false;
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
            
            //움직임 동기화
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
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

            currrentPos = (Vector3)stream.ReceiveNext();
            currentQuater = (Quaternion)stream.ReceiveNext();
//            transform.position = (Vector3)stream.ReceiveNext();
//            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
