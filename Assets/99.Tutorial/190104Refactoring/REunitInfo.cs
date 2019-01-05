using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REunitInfo : MonoBehaviour {


    public REtileInfo unitTile;

    //유닛 상태
    public int UnitHP;

    public int UnitATK;

    public int UnitSHD;

    public bool IsDie;

    public int UnitActPoint;
    public bool proceedScript { get; private set; }


    private void Start()
    {
        transform.position = unitTile.transform.position;
    }




    public void GetDamage(int damage)
    {
        UnitHP -= damage;
    }

    public int Damage()
    {
        return UnitATK;
    }

    public void Die(bool die)
    {
        IsDie = die;
    }

    public void ConsumeActPoint(int cost)
    {
        UnitActPoint -= cost;
    }

}
