using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synchro : MonoBehaviour {
    UnitInfo unitinfo;
    Anim anim;

    [SerializeField] int posX;
    [SerializeField] int posY;

    [SerializeField] float synchroMoveSpeed;
    [SerializeField] float synchroRotSpeed;

    Coroutine coroutine;

    bool moveTrigger;
    private void Awake()
    {
        anim = GetComponent<Anim>();
        unitinfo = GetComponent<UnitInfo>();

        synchroMoveSpeed = unitinfo.moveSpeed;
        synchroRotSpeed = unitinfo.rotSpeed;
        moveTrigger = false;
    }
    void Update ()
    {
        if (GameData.data == null) return;
        if((posX!=unitinfo.x || posY!=unitinfo.y) && !moveTrigger)
        {
            moveTrigger = true;
            if(coroutine!=null) StopCoroutine(coroutine);

            coroutine = StartCoroutine(Move(GameData.data.FindTile(unitinfo.x, unitinfo.y)));
            return;
        }
        if(coroutine == null)anim.Stop();//이동 애니메이션 끝
    }
    public IEnumerator Move(TileInfo EndPos)
    {
        if(posX == 0 && posY == 0)
        {
            moveTrigger = false;
            posX = unitinfo.x;
            posY = unitinfo.y;
            yield break;
        }
        if(posX == EndPos.x && posY == EndPos.y) 
        {
            moveTrigger = false;
            yield break;
        }
        Debug.Log("이동시작" + posX + "," + posY + " : " + unitinfo.x + "," + unitinfo.y);

        Vector3 endPos;
        Vector3 startPos;
        Quaternion startRot;
        Quaternion endRot;

        float time = 0;
        endPos = EndPos.transform.position;
        startPos = transform.position;

        startRot = transform.rotation;

        endRot = Quaternion.LookRotation(EndPos.transform.position - transform.position);


        anim.Move();//이동 애니메이션 시작

        time = 0;
        while (Quaternion.Angle(endRot, transform.rotation) > 5)
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, time);
            time += Time.deltaTime * synchroRotSpeed;
            yield return null;
        }
        time = 0;
        while (Vector3.Magnitude(transform.position - endPos) > 0.1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time);
            time += Time.deltaTime * synchroMoveSpeed;
            yield return null;
        }

        posX = EndPos.x;
        posY = EndPos.y;

//        anim.Stop();//이동 애니메이션 끝
        moveTrigger = false;
        coroutine = null;
    }
}
