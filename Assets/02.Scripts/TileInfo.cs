using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {

    [SerializeField] public int x;
    [SerializeField] public int y;

    [SerializeField] public int cost;
    [SerializeField] public int idlecost;

    [SerializeField] public int occupyPoint;

    [SerializeField] float ChangeSpeed;

    Coroutine coroutine;

    public void GetOcccupy()
    {
        occupyPoint = occupyPoint < 2 ? occupyPoint + 1 : occupyPoint;
        if (occupyPoint != 2) return;
        
        if (coroutine != null) StopCoroutine(coroutine);
        Color currentColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        coroutine = StartCoroutine(changeColor(currentColor, GameData.data.teamColor));
    }
    public void LoseOcccupy()
    {
        occupyPoint = occupyPoint > -2 ? occupyPoint - 1 : occupyPoint;
        if (occupyPoint != -2) return;

        if (coroutine != null) StopCoroutine(coroutine);
        Color currentColor = GetComponent<Renderer>().material.color;
        coroutine = StartCoroutine(changeColor(currentColor, GameData.data.teamColor));
    }

    IEnumerator changeColor(Color startColor, Color endColor)
    {
        float time = 0;
        while (time <= 1) {
            Color.Lerp(startColor,endColor,time);
            time += Time.deltaTime * ChangeSpeed;
            yield return null;
        }
        yield break;
    }



    public void CanUse()
    {
        //타일 하이라이트 표시
    }
    public void ResetUse()
    {
        //타일 하이라이트 제거
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
