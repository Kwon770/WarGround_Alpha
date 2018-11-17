using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfoTutorial : MonoBehaviour {

    [SerializeField] public int X = 0;
    [SerializeField] public int Y = 0;
    [SerializeField] public int cost = 1;
    [SerializeField] enum ocpyStat { team, enemy, none }
    [SerializeField] public bool selectTile = false;
    [SerializeField] public bool firstTile = false;  // 반드시 이동, 혹은 공격 후에 이것을 false 로 되돌려줘야한다.

    Renderer mat;

    private void Start()
    {
        mat = transform.GetChild(0).gameObject.GetComponent<Renderer>();


    }

    private void Update()
    {
        ChangeVisual();
    }

    void ChangeVisual() // 점령 상태에 따라 타일 색상을 변경
    {
        
        

        if( selectTile == true )
        {
            mat.material.color = Color.red;
        }
        else
        {
            mat.material.color = Color.white;
        }
    }

  


}
