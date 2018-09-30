using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {

    [SerializeField] int x;
    [SerializeField] int y;

    [SerializeField] int cost;
    [SerializeField] int idlecost;
    public bool Switch;

    private void OnMouseDown()
    {
        if (!Switch) return;
    }

    public void CanUse()
    {
        Switch = true;
        //타일 하이라이트 표시
    }
    public void ResetUse()
    {
        Switch = false;
    }

    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }
    public int GetCost()
    {
        return cost;
    }
}
