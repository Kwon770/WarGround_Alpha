using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfoTutorial : MonoBehaviour {

    [SerializeField] public int X = 0;
    [SerializeField] public int Y = 0;
    [SerializeField] public int cost = 1;
    [SerializeField] enum ocpyStat { team, enemy, none }
    [SerializeField] bool selectTile = false;

    void ChangeVisual( ocpyStat stat, bool select ) // 점령 상태에 따라 타일 색상을 변경
    {
        if( stat == ocpyStat.team )
        {

        }
        else if( stat == ocpyStat.enemy )
        {

        }
        else
        {

        }

        if( select == false )
        {
            //투명도 50%적용
        }
        else
        {
            //투명도 100% 적용
        }
    }

  


}
