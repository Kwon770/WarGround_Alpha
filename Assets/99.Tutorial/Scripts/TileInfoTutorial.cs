using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfoTutorial : MonoBehaviour {

    [SerializeField] public int X = 0;
    [SerializeField] public int Y = 0;
    [SerializeField] public int cost = 1;
    [SerializeField] enum ocpyStat { team, enemy, none }
    [SerializeField] public bool selectTile = false;
    [SerializeField] public bool firstTile = false; // 당연히 행동대상 유닛이 있느 타일은 활성화가 되면 안되잖냐

    //이동관련
    [SerializeField] public TileInfoTutorial path;
    [SerializeField] public int stage;
    [SerializeField] public UnitInfoTutorial selectUnit;

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
