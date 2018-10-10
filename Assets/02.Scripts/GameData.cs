using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    public static GameData data;

    [SerializeField] Transform Map;
    
    public TileInfo[] Tiles;

    //초기화
	void Awake () {
        data = this;

        //타일 정보 불러오기
        Tiles = new TileInfo[Map.childCount];
        for(int i = 0; i < Map.childCount; i++)
        {
            Tiles[i] = Map.GetChild(i).GetComponent<TileInfo>();
        }
	}

    public TileInfo FindTile(int x,int y)
    {
        foreach(TileInfo tile in Tiles)
        {
            if (tile.x == x && tile.y == y) return tile;
        }
        return null;
    }
}
