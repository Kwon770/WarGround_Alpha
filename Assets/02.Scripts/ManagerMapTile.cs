using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMapTile : MonoBehaviour {

    [SerializeField] GameObject Tiles;
    [SerializeField] TileInfo[] Tile;

    TileInfo Temp;
    private void Start()
    {
        Temp = null;
        Tile = new TileInfo[transform.childCount];
       for(int i = 0; i < transform.childCount; i++)
        {
            Tile[i] = Tiles.transform.GetChild(i).GetComponent<TileInfo>();
        }
    }

    void CheckMoveTile(int x,int y,int cost)
    {

        Queue<TileInfo> Tile = new Queue<TileInfo>();
        Queue<int> Cost = new Queue<int>();

        if (FindTile(x, y) == null) return;

        Tile.Enqueue(FindTile(x, y));
        Cost.Enqueue(cost);
        while (true)
        {
            if (Tile.Count == 0) break;

            x = Tile.Peek().GetX();
            y = Tile.Peek().GetY();

            int k = x % 2 == 1 ? 1 : 0;

            Temp = FindTile(x, y - 1);
            if (Temp != null && Cost.Peek() - Temp.GetCost() >= 0)
            {
                Cost.Enqueue(Cost.Peek() - Temp.GetCost());
                Tile.Enqueue(Temp);
            }
            Temp = FindTile(x, y + 1);
            if (Temp != null && Cost.Peek() - Temp.GetCost() >= 0)
            {
                Cost.Enqueue(Cost.Peek() - Temp.GetCost());
                Tile.Enqueue(Temp);
            }
            Temp = FindTile(x + 1, y - k);
            if (Temp != null && Cost.Peek() - Temp.GetCost() >= 0)
            {
                Cost.Enqueue(Cost.Peek() - Temp.GetCost());
                Tile.Enqueue(Temp);
            }
            Temp = FindTile(x + 1, y + 1 - k);
            if (Temp != null && Cost.Peek() - Temp.GetCost() >= 0)
            {
                Cost.Enqueue(Cost.Peek() - Temp.GetCost());
                Tile.Enqueue(Temp);
            }
            Temp = FindTile(x - 1, y - k);
            if (Temp != null && Cost.Peek() - Temp.GetCost() >= 0)
            {
                Cost.Enqueue(Cost.Peek() - Temp.GetCost());
                Tile.Enqueue(Temp);
            }
            Temp = FindTile(x - 1, y + 1 - k);
            if (Temp != null && Cost.Peek() - Temp.GetCost() >= 0)
            {
                Cost.Enqueue(Cost.Peek() - Temp.GetCost());
                Tile.Enqueue(Temp);
            }
            Tile.Dequeue().CanUse();
            Cost.Dequeue();
        }
    }
    private TileInfo FindTile(int x, int y)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Tile[i].GetX() == x && Tile[i].GetY() == y) return Tile[i];
        }
        return null;
    }
}