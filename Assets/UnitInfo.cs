using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : Photon.MonoBehaviour {

    public string Owner;

    [SerializeField] int ATK;
    [SerializeField] int MaxHP;
    [SerializeField] int HP;
    [SerializeField] int SHD;
    [SerializeField] int MaxAct;
    [SerializeField] int Act;
    [SerializeField] int range;
    // Use this for initialization

    //초기화
    void Awake () {
        DontDestroyOnLoad(gameObject);
	}
    public void SetOwner(string name)
    {
        Owner = name;
        if (PhotonNetwork.playerName == name)
        {
            if (!photonView.isMine)
            {
                photonView.RequestOwnership();
            }
        }
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

    //스텟 동기화
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(HP);
            stream.SendNext(SHD);
            stream.SendNext(Act);
        }
        else
        {
            HP = (int)stream.ReceiveNext();
            SHD = (int)stream.ReceiveNext();
            Act = (int)stream.ReceiveNext();
        }
    }
}
