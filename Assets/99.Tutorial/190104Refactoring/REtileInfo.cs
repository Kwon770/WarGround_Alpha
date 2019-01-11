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

    public bool spawnSelecting;

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
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }

                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
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
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }

                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() + 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() + 1 && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() - 1 && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
                        {
                            if (tileList[i].occupyStatus != -1)
                            {
                                tileList[i].occupyStatus--;
                            }
                        }
                    }
                    else if (tileList[i].GetTileX() == GetTileX() - 1 && tileList[i].GetTileY() == GetTileY() && tileList[i].NoTouch == false)
                    {
                        if (OnUnit.gameObject.tag == "Player")
                        {
                            if (tileList[i].occupyStatus != 1)
                            {
                                tileList[i].occupyStatus++;
                            }
                        }

                        if (OnUnit.gameObject.tag == "Enemy")
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

    public void OccupyColor()
    {
        if(occupyStatus == 1)
        {
            render.material.color = new Color(0f, 0.2f, 0.8f, 1f);
        }
        else if(occupyStatus == 0)
        {
            render.material.color = originColor;
        }
        else
        {
            render.material.color = new Color(0.8f, 0.2f, 0f, 1f);
        }
    }
    
    public IEnumerator ChangeColor()
    {
        if(Selecting == true || spawnSelecting == true)
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
