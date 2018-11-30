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
    [SerializeField] TutorialManager tutorialManager;

    [SerializeField] public int ATK;
    [SerializeField] public int HP;
    [SerializeField] public int SHD;
    [SerializeField] public string unitName;


    private void Start()
    {
        transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z);
        
    }
    public IEnumerator UnitMove(List<TileInfoTutorial> path, int tileStage, UnitInfoTutorial moveUnit)   //유닛이 이동하는데 딜레이 있게 이동한다
    {
        movingEnd = false;

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

                transform.rotation = Quaternion.Lerp(startRot, endRot, time);
                time += Time.deltaTime * 5f;
                yield return null;
            }
            time = 0;
            while (time <= 1)
            {
              
                transform.position = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime * 1.8f;
                yield return null;
            }
            anim.SetBool("MOVE", false);
            yield return new WaitForSeconds(0.2f);
        }
        movingEnd = true;

        if (tutorialManager.selectUnit == tutorialManager.enemy)
        {
            tutorialManager.selectUnit = tutorialManager.unit;
           
        }
       
        if (scriptManager.textNumber == 14)
        {
            tutorialManager.unit.actPoint = 3;
        }

        //일단은 이동 튜토리얼 끝나면 바로 메세지 넘어가게 하기는 했지만 추후 조치가 필요해보임 임시 땜빵
        if (scriptManager.textNumber == 6)
        {
            

            tutorialManager.tileSaveInfo[16].occupation = 2;
            tutorialManager.tileSaveInfo[15].occupation = 2;
            tutorialManager.tileSaveInfo[8].occupation = 2; 
            tutorialManager.tileSaveInfo[9].occupation = 2;
            tutorialManager.tileSaveInfo[24].occupation = 2;

            tutorialManager.tileSaveInfo[18].occupation = 0;

            tutorialManager.tileSaveInfo[18].occupation = -2;
            tutorialManager.tileSaveInfo[19].occupation = -2;
            tutorialManager.tileSaveInfo[11].occupation = -2;
            tutorialManager.tileSaveInfo[25].occupation = -2;
            tutorialManager.tileSaveInfo[26].occupation = -2;
        }
        else if (scriptManager.textNumber == 14)
        {
           

            tutorialManager.tileSaveInfo[16].occupation = 2;
            tutorialManager.tileSaveInfo[8].occupation = 2;
            
          
            tutorialManager.tileSaveInfo[24].occupation = 2;

            tutorialManager.tileSaveInfo[9].occupation = 0;
            tutorialManager.tileSaveInfo[24].occupation = 0;

            tutorialManager.tileSaveInfo[17].occupation = -2;


        }

        scriptManager.canSkip = true;
        scriptManager.textNumber++;
        scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
        scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;
    }
    
    public IEnumerator Attack()
    {
        float time = 0;
        while (time <= 1)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 87f, transform.rotation.z), time);
            time += Time.deltaTime * 5;
            yield return null;
        }
        
        anim.SetTrigger("ATTACK");
        tutorialManager.unit.actPoint = 0;
        yield return new WaitForSeconds(3.5f);
        scriptManager.canSkip = true;
        scriptManager.textNumber++;
        scriptManager.StartCoroutine(scriptManager.MessagePrint(scriptManager.boxIndex));
        scriptManager.boxIndex = (scriptManager.boxIndex - 1) * -1;
        yield return null;
    }

    public IEnumerator Damaged()
    {
        yield return new WaitForSeconds(1.4f);
        anim.SetTrigger("DIE");
        Debug.Log("공격당함");
        yield return null;
    }
}


