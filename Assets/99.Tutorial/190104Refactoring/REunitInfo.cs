using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REunitInfo : MonoBehaviour {


    //유닛 상태
    public int UnitHP { get; private set; }

    public int UnitATK { get; private set; }

    public int UnitSHD { get; private set; }

    public bool IsDie { get; private set; }

    public int UnitActPoint { get; private set; }

    //시스템 관련
    public bool proceedScript { get; private set; }

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
