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

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 본게임

    [SerializeField] public int occupation;

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

        else if (occupation == 2)
        {
            mat.material.color = new Color(255f / 240f, 255f / 20f, 255f / 35f, 1f);
            
        }
        else if (occupation == 1)
        {
            mat.material.color = new Color(255f / 125f, 255f / 255f, 255f / 1f, 1f);
        }
        else if (occupation == 0)
        {
            mat.material.color = Color.white;
        }
        else if (occupation == -1)
        {
            mat.material.color = new Color(255f / 1f, 255f / 255f, 255f / 125f, 1f);
        }
        else if (occupation == -2)
        {
            mat.material.color = new Color(255f / 35f, 255f / 20f, 255f / 240f, 1f);
        }
        else
        {
           // mat.material.color = Color.white;
        }
    }

  


}
