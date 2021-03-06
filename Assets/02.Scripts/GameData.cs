﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    [SerializeField] public Mesh SkullKnight;

    public static GameData data;
    
    [SerializeField] Transform Map;
    
    public TileInfo[] Tiles;
    public List<UnitInfo> Units;

    //초기화
	void Awake () {
        data = this;
        
        Tiles = new TileInfo[Map.childCount];
        Units = new List<UnitInfo>();

        //타일 정보 불러오기
        for (int i = 0; i < Map.childCount; i++)
        {
            Tiles[i] = Map.GetChild(i).GetComponent<TileInfo>();
        }
	}


    //유닛 데이터에 추가
    public void AddUnit(UnitInfo unit)
    {
        Units.Add(unit);
    }
    //유닛 데이터에서 삭제
    public void DelUnit(UnitInfo unit)
    {
        Units.Remove(unit);
    }

    //좌표로 유닛찾기
    public UnitInfo FindUnit(int x, int y)
    {
        foreach(UnitInfo unit in Units)
        {
            if (unit.x == x && unit.y == y) return unit;
        }
        return null;
    }

    //좌표로 타일찾기
    public TileInfo FindTile(int x, int y)
    {
        foreach(TileInfo tile in Tiles)
        {
            if (tile.x == x && tile.y == y) return tile;
        }
        return null;
    }
}
