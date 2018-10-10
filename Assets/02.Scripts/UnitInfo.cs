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
        
        Vector3 endPos;
        Vector3 startPos;

        for (int i = 0; i < path.Count; i++)
        {
            float time = 0;
            endPos = path[i].transform.position;
            startPos = transform.position;
            
            x = path[i].x;
            y = path[i].y;

            while (transform.position!=endPos)
            {
                transform.position = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime * moveSpeed;
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
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
