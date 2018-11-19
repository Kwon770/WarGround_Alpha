using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfoTutorial : MonoBehaviour {

    [SerializeField] public TileInfoTutorial gotoTile;
    [SerializeField] public TileInfoTutorial myTile;

    [SerializeField] bool moving = false;

    private void Start()
    {
        transform.position = new Vector3(myTile.transform.position.x, myTile.transform.position.y, myTile.transform.position.z);
    }

    private void Update()
    {
        if(myTile != gotoTile)
        {
            if(moving == false)
            {
                moving = true;
                StartCoroutine(StopMoving());
            }

            transform.position = Vector3.MoveTowards(transform.position, gotoTile.transform.position, 8.0f * Time.deltaTime);
        }
        else
        {
            moving = false;
        }

        
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(2.0f);
        moving = false;
        myTile = gotoTile;
    }
}


