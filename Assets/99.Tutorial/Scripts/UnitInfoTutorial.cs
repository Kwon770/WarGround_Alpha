using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfoTutorial : MonoBehaviour {

    [SerializeField] public TileInfoTutorial endPoint;
    [SerializeField] public TileInfoTutorial startPoint; // 이동용
    

    [SerializeField] public int actPoint;

    [SerializeField] bool moving = false;   //애니메이션 전용
    [SerializeField] public bool movingEnd = true;    //이동 중 명력 중복 수행 방지 전용, true= 버튼 누를수잇음, false = 못 누름


    [SerializeField] Animator anim;

    [SerializeField] ScriptManager scriptManager;

    private void Start()
    {
        transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z);
        
    }
    public IEnumerator UnitMove(List<TileInfoTutorial> path, int tileStage, UnitInfoTutorial moveUnit)   //유닛이 이동하는데 딜레이 있게 이동한다
    {
        movingEnd = false;
        Debug.Log(path.Count);
        path.Reverse();
        Vector3 endPos;
        Vector3 startPos;
        Quaternion startRot;
        Quaternion endRot;

        for (int i = 0; i < path.Count; i++)
        {
            float time = 0;
            endPos = path[i].transform.position;
            startPos = transform.position;

            endRot = Quaternion.LookRotation(path[i].transform.position - transform.position);
            startRot = transform.rotation;

            startPoint = path[i];

            anim.SetBool("MOVE", true);
            while (time <= 1)
            {
                Debug.Log("회전중");
                transform.rotation = Quaternion.Lerp(startRot, endRot, time);
                time += Time.deltaTime * 5;
                yield return null;
            }
            time = 0;
            while (time <= 1)
            {
                Debug.Log("이동중");
                transform.position = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime * 1f;
                yield return null;
            }
            anim.SetBool("MOVE", false);
            yield return new WaitForSeconds(0.5f);
        }
        movingEnd = true;
        scriptManager.canSkip = true;
        scriptManager.textNumber++;
        scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
        scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;
    }
    
}


