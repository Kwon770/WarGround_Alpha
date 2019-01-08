using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REunitBehaviour : MonoBehaviour {

    public float MoveSpeed;
    [SerializeField] REunitInfo unitInfo;
 

    public IEnumerator MovePlayer(REtileInfo startPos, REtileInfo endPos, int cost)
    {
        
        REtileInfo RouteFinder = endPos;
        REtileInfo Start = startPos;
        REtileInfo End = endPos;

        Stack<REtileInfo> tileStack = new Stack<REtileInfo>();
        tileStack.Push(RouteFinder);

        for (int i = 0; i < cost - 1; i++)
        {
            tileStack.Push(RouteFinder.RouteTile);
            RouteFinder = RouteFinder.RouteTile;
        }

        for (int i = 0; i < cost; i++)
        {
            float time = 0;

            End = tileStack.Pop();
            Quaternion endRot = Quaternion.LookRotation(End.transform.position - transform.position);
            Quaternion startRot = transform.rotation;

            while (time <= 1)
            {
                transform.rotation = Quaternion.Lerp(startRot, endRot, time);
                time += 5 * Time.deltaTime;

                yield return null;
            }

            time = 0;

            
            
            while(time <= 1)
            {
                transform.position = Vector3.Lerp(Start.transform.position, End.transform.position, time);
                time += MoveSpeed * Time.deltaTime;

                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
            Start = End;
        }

        GetComponent<REunitInfo>().unitTile.NullUnit();
        GetComponent<REunitInfo>().unitTile = End;
        End.OnUnit = GetComponent<REunitInfo>();
        yield return null;
    }
}
