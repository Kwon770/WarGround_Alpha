using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour {

    public static InfoBar bar;

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
        this.name.text = name;
        this.ATK.text = ATK.ToString();
        this.HP.text = HP.ToString();
        this.SHD.text = SHD.ToString();
        this.act.SetUI(ACT);
    }
    public void ResetUI()
    {
        this.name.text = "-";
        this.ATK.text = "-";
        this.HP.text = "-";
        this.SHD.text = "-";
        this.act.ResetUI();
    }
    public void SetBit()
    {
        bitinium.SetUI(GameData.data.bitinium);
    }
    public void SetLeadership()
    {
        leadership.SetUI(GameData.data.LeaderShip);
    }

}
