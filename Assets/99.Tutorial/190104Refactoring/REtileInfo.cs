using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REtileInfo : MonoBehaviour {

    [SerializeField]
    private int tileX;
    [SerializeField]
    private int tileY;

    public int tileCost = 0;

    Renderer render;
    Color originColor;
    Color nowColor;

    private void Start()
    {
        originColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        nowColor = originColor;
        render = transform.GetChild(0).GetComponent<Renderer>();
    }

    public int occupyStatus;

    public bool NoTouch;

    public REunitInfo OnUnit;

    public bool Selecting;

    public REtileInfo RouteTile;

    public int TileCost { get; private set; }

    // noooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

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

    public void CheckOccupied(List<REtileInfo> tileList)
    { 
        if(OnUnit != null)
        {
            for (int i = 0; i < tileList.Count; i++)
            {
                if (GetTileX() % 2 == 0)
                {
                    if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }

                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                }
                else
                {
                    if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }

                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (tileList[i].OnUnit.gameObject.tag == "enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                }

            }
        }
    }

    public void NullUnit()
    {
        OnUnit = null;
    }
    
    public IEnumerator ChangeColor()
    {
        if(Selecting == true)
        {
            float time = 0;

            while ( time <= 1)
            {
                time += 2f * Time.deltaTime;
                render.material.color = Color.Lerp(originColor, new Color(0f, 0.6f, 0f, 1f), time);
                yield return null;
            }
            nowColor = render.material.color;
            yield return null;
        }
        else
        {
            float time = 0;


            while (time <= 1)
            {
                time += 2f * Time.deltaTime;
                render.material.color = Color.Lerp(nowColor, originColor, time);
                yield return null;
            }
            nowColor = render.material.color;
            yield return null;
        }
    }
    
}
