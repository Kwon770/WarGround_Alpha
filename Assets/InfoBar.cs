using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour {

    public static InfoBar bar;

    [SerializeField] Transform upPos;
    [SerializeField] Transform downPos;

    [SerializeField] ActBar act;
    [SerializeField] Leadership leadership;
    [SerializeField] Bitinium bitinium;

    [SerializeField] Text name;
    [SerializeField] Text ATK;
    [SerializeField] Text HP;
    [SerializeField] Text SHD;

    private void Awake()
    {
        bar = this;
    }
    public void Setting(int bit,int leadership)
    {
        bitinium.Setting(bit);
        this.leadership.Setting(leadership);
        act.Setting();
    }
    public void SetUI(string name, int ATK, int HP, int SHD, int ACT)
    {
        StartCoroutine(_SetUI(name, ATK, HP, SHD, ACT));
    }
    public void ResetUI()
    {
        StartCoroutine(_ResetUI());
    }
    public void SetBit(int point)
    {
        bitinium.SetUI(point);
    }
    public void SetLeadership()
    {
        leadership.SetUI(GameData.data.LeaderShip);
    }

    IEnumerator _SetUI(string name, int ATK, int HP, int SHD, int ACT)
    {
        float time = 0f;
        while (time < 1f)
        {
            transform.position = Vector3.Lerp(upPos.position, downPos.position, time);
            time += Time.deltaTime * 2;
            yield return null;
        }

        this.name.text = name;
        this.ATK.text = ATK.ToString();
        this.HP.text = HP.ToString();
        this.SHD.text = SHD.ToString();
        this.act.SetUI(ACT);

        time = 0f;
        while (time < 1f)
        {
            transform.position = Vector3.Lerp(downPos.position, upPos.position, time);
            time += Time.deltaTime * 2;
            yield return null;
        }
        yield return null;
    }

    IEnumerator _ResetUI()
    {
        float time = 0f;

        while (time < 1f)
        {
            transform.position = Vector3.Lerp(upPos.position, downPos.position, time);
            time += Time.deltaTime * 2;
            yield return null;
        }

        this.name.text = "-";
        this.ATK.text = "-";
        this.HP.text = "-";
        this.SHD.text = "-";
        this.act.ResetUI();
    }

}
