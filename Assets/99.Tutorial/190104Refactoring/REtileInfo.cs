using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REtileInfo : MonoBehaviour {

    [SerializeField]
    private int tileX;
    [SerializeField]
    private int tileY;

    public bool NoTouch;

    public bool Selecting { get; private set; }

    public REtileInfo RouteTile { get; private set; }

    public int TileCost { get; private set; }

    public void SetTileCost(int value)
    {
        TileCost = value;
    }

    public void SetSelecting(bool value)
    {
        Selecting = value;
    }

    public void SetRouteTile(REtileInfo value)
    {
        RouteTile = value;
    }

    public int GetTileX()
    {
        return tileX;
    }

    public int GetTileY()
    {
        return tileY;
    }
}
