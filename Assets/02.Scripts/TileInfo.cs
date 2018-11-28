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

    MeshRenderer currentColor;
    Color ResetColor;

    Coroutine coroutine;

    private void Awake()
    {
        currentColor = transform.GetChild(0).GetComponent<MeshRenderer>();
        ResetColor = currentColor.material.color;
    }

    public void GetOcccupy()
    {
        occupyPoint++;

        if (occupyPoint >= 1)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            Color startColor = currentColor.material.color;
            coroutine = StartCoroutine(changeColor(startColor, GameData.data.teamColor));
        }
        else
        {
            if (coroutine != null) StopCoroutine(coroutine);
            Color startColor = currentColor.material.color;
            coroutine = StartCoroutine(changeColor(startColor, ResetColor));
        }
    }
    public void LoseOcccupy()
    {
        occupyPoint--;
        if (occupyPoint <= -1)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            Color startColor = currentColor.material.color;
            coroutine = StartCoroutine(changeColor(startColor, GameData.data.enemyColor));
        }
        else
        {
            if (coroutine != null) StopCoroutine(coroutine);
            Color startColor = currentColor.material.color;
            coroutine = StartCoroutine(changeColor(startColor, ResetColor));
        }
    }

    public void CanUse()
    {
        //타일 하이라이트 표시

        Color useColor = GameData.data.CanUseColor;
        ResetColor = currentColor.material.color;

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(changeColor(ResetColor, useColor));
    }
    public void ResetUse()
    {
        //타일 하이라이트 제거

        if (coroutine != null) StopCoroutine(coroutine);
        Color startColor = ResetColor;
        coroutine = StartCoroutine(changeColor(startColor, ResetColor));
    }

    IEnumerator changeColor(Color startColor, Color endColor)
    {
        float time = 0;
        while (time <= 1) {
            currentColor.material.color = Color.Lerp(startColor,endColor,time);
            time += Time.deltaTime * ChangeSpeed;
            yield return null;
        }
        yield break;
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
