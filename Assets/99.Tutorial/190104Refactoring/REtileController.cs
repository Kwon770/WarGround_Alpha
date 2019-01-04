using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REtileController : MonoBehaviour {

    private List<REtileInfo> tileList;

    void GetChildTile()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            tileList.Add(transform.GetChild(i).GetComponent<REtileInfo>());
        }
    }

	void GetMovableTile(REtileInfo playerTile, int cost)    //B.F.S
    {
        Queue<REtileInfo> BFSsaver = new Queue<REtileInfo>();
        BFSsaver.Enqueue(playerTile);

        for(int i = 0; i < cost; i++)
        {
            int queueCount = BFSsaver.Count;

            for (int j = 0; j < queueCount; j++)
            {
                REtileInfo centerTile = BFSsaver.Dequeue();
                for (int k = 0; k < transform.childCount; k++)
                {
                    int optimize = 0;

                    if(centerTile.GetTileX() % 2 == 0)
                    {
                        if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() - 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() + 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
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
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() + 1 && tileList[i].GetTileY() == centerTile.GetTileY() - 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() && tileList[i].GetTileY() == centerTile.GetTileY() - 1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() -1 && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
                            BFSsaver.Enqueue(tileList[i]);

                            optimize++;
                        }
                        else if (tileList[i].GetTileX() == centerTile.GetTileX() - 1 && tileList[i].GetTileY() == centerTile.GetTileY() && tileList[i].Selecting == false && tileList[i].NoTouch == false)
                        {
                            tileList[i].SetSelecting(true);
                            tileList[i].SetRouteTile(centerTile);
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
    }

    void ResetSelectingTile()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tileList[i].SetSelecting(false);
        }
    }
}
