using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {

    public static Calculator Calc;
	// Use this for initialization
	void Awake () {
        Calc = this;
	}

    //이동 행동력계산,path 반환
    public List<TileInfo> Move(TileInfo EP, TileInfo SP, int Act)
    {
        int s=0, e=0, minAct=Act+1,minActIndex=0;
        TileInfo tile, temp;
        List<TileInfo> TileQueue = new List<TileInfo>();
        List<int> ActQueue = new List<int>();
        List<int> PathQueue = new List<int>();
        TileQueue.Add(SP);
        ActQueue.Add(0);
        PathQueue.Add(0);
        

        while (s <= e)
        {
            tile = TileQueue[s];
            int k = tile.x % 2 == 1 ? 1 : 0;
            
            if (tile.gameObject.Equals(EP.gameObject))
            {
                Debug.Log(ActQueue[s] + " " + minAct);
                if (minAct > ActQueue[s])
                {
                    minAct = ActQueue[s];
                    minActIndex = s;
                }
                //최소값 비교
            }

            temp = GameData.data.FindTile(tile.x, tile.y - 1);
            if (temp != null && ActQueue[s]+temp.cost<=Act)
            {
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x, tile.y + 1);
            if (temp != null && ActQueue[s] + temp.cost <= Act)
            {
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act)
            {
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y + 1 - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act)
            {
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act)
            {
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y + 1 - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act)
            {
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            s++;
        }
        if (minActIndex != 0)
        {
            int index = minActIndex;
            List<TileInfo> path = new List<TileInfo>();
            path.Add(TileQueue[index]);
            while (index != 0)
            {
                index = PathQueue[index];
                path.Add(TileQueue[index]);
            }
            return path;
        }
        return null;
    }

    public bool Attack(TileInfo EP, TileInfo SP, int range)
    {
        int s = 0, e = 0;
        TileInfo tile, temp;
        List<TileInfo> TileQueue = new List<TileInfo>();
        List<int> RangeQueue = new List<int>();
        TileQueue.Add(SP);
        RangeQueue.Add(0);
        
        while (s <= e)
        {
            tile = TileQueue[s];
            int k = tile.x % 2 == 1 ? 1 : 0;
            if (tile.gameObject.Equals(EP.gameObject))
            {
                return true;
            }
            temp = GameData.data.FindTile(tile.x, tile.y - 1);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x, tile.y + 1);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y + 1 - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y + 1 - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            s++;
        }
        return false;
    }

    //공격 가능 여부 계산, 가능 : true, 불가능 : false
    public bool Attack()
    {
        return false;
    }
}
