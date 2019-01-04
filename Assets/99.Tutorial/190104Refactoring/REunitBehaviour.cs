using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REunitBehaviour : MonoBehaviour {

    public float MoveSpeed { get; private set; }

    public IEnumerator MovePlayer(REtileInfo startPos, REtileInfo endPos, int cost)
    {
        
        REtileInfo RouteFinder = endPos;
        REtileInfo Start = startPos;
        REtileInfo End = endPos;

        Stack<REtileInfo> tileStack = new Stack<REtileInfo>();
        for (int i = 0; i < cost - 1; i++)
        {
            tileStack.Push(RouteFinder.RouteTile);
            RouteFinder = RouteFinder.RouteTile;
        }

        for (int i = 0; i < cost; i++)
        {
            float time = 0;

            End = tileStack.Pop();
            Quaternion startRot = transform.rotation;
            Quaternion endRot = End.transform.rotation;

            while(time <= 1)
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

            Start = End;
        }

        yield return null;
    }
}
