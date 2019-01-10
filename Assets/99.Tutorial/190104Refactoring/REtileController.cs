using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REtileController : MonoBehaviour {

    public List<REtileInfo> tileList;

    public void GetChildTile()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            tileList.Add(transform.GetChild(i).GetComponent<REtileInfo>());
        }
    }

	public void GetMovableTile(REtileInfo playerTile, int cost)    //B.F.S
    {
        Queue<REtileInfo> BFSsaver = new Queue<REtileInfo>();
        BFSsaver.Enqueue(playerTile);

        int tileCost = 0;
      
        for(int k = 0; k < cost; k++)
        {
            int queueCount = BFSsaver.Count;

            tileCost++;

            for (int j = 0; j < queueCount; j++)
            {
                REtileInfo centerTile = BFSsaver.Dequeue();

                for (int i = 0; i < transform.childCount; i++)
                {
                    int optimize = 0;
                  
                    if (centerTile.GetTileX() % 2 == 0)
                    {
                        
                        if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);
                           
                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() - 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                    }
                    else
                    {

                        if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);
                            
                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() - 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() - 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() -1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            tileList[i].tileCost = tileCost;
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                    }
                    

                    if(optimize >= 6)
                    {
                        break;
                    }
                }

            }
        }
        ResetExceptEventTile();
    }

    public void Occupy()
    {
        for(int i = 0; i < tileList.Count; i++)
        {
            tileList[i].CheckOccupied(tileList);
            Debug.Log("아이들");
        }
    }

    public void ResetSelectingTile()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

             tileList[i].SetSelecting(false);

        }
    }

    public void ResetExceptEventTile()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != 16)
            {
                tileList[i].SetSelecting(false);
            }
        }
    }
}
