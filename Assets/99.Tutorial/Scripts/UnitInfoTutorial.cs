using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfoTutorial : MonoBehaviour {

    [SerializeField] public TileInfoTutorial endPoint;
    [SerializeField] public TileInfoTutorial startPoint; // 이동용
    

    [SerializeField] public int actPoint;

    [SerializeField] bool moving = false;   //애니메이션 전용
    [SerializeField] public bool movingEnd = true;    //이동 중 명력 중복 수행 방지 전용, true= 버튼 누를수잇음, false = 못 누름

    private void Start()
    {
        transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z);
        
    }

    private void Update()
    {
        endPoint.firstTile = true;

        if (startPoint != endPoint)
        {
            if(moving == false)
            {
                moving = true;
                StartCoroutine(StopMoving());
            }

            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, 8.0f * Time.deltaTime);
        }
        else
        {
            moving = false;
            
        }

        
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(0.5f);
        moving = false;
        startPoint = endPoint;
    }
}


