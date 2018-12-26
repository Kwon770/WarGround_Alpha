using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour {

    public static InfoBar bar;

    [SerializeField] bool active;

    [SerializeField] Transform upPos;
    [SerializeField] Transform downPos;

    [SerializeField] ActBar act;
    [SerializeField] Leadership leadership;
    [SerializeField] Bitinium bitinium;

    [SerializeField] Text name;
    [SerializeField] Text ATK;
    [SerializeField] Text HP;
    [SerializeField] Text SHD;

    [SerializeField] Image UnitIcon;
    [SerializeField] Image SkillIcon;

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
    public void SetUI(Sprite unitIcon, Sprite skillIcon, string name, int ATK, int HP, int SHD, int ACT)
    {
        StartCoroutine(_SetUI(unitIcon, skillIcon, name, ATK, HP, SHD, ACT));
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

    IEnumerator _SetUI(Sprite unitIcon,Sprite skillIcon,string name, int ATK, int HP, int SHD, int ACT)
    {
        float time = 0f;
        while (time < 1f && active)
        {
            transform.position = Vector3.Lerp(upPos.position, downPos.position, time);
            time += Time.deltaTime * 4;
            yield return null;
        }

        this.name.text = name;
        this.ATK.text = ATK.ToString();
        this.HP.text = HP.ToString();
        this.SHD.text = SHD.ToString();
        this.act.SetUI(ACT);

        Debug.Log(unitIcon + " " + skillIcon);

        UnitIcon.sprite = unitIcon;
        SkillIcon.sprite = skillIcon;

        time = 0f;
        while (time < 1f)
        {
            transform.position = Vector3.Lerp(downPos.position, upPos.position, time);
            time += Time.deltaTime * 4;
            yield return null;
        }
        active = true;
        yield return null;
    }

    IEnumerator _ResetUI()
    {
        float time = 0f;

        while (time < 1f)
        {
            transform.position = Vector3.Lerp(upPos.position, downPos.position, time);
            time += Time.deltaTime * 4;
            yield return null;
        }

        this.name.text = "-";
        this.ATK.text = "-";
        this.HP.text = "-";
        this.SHD.text = "-";
        this.act.ResetUI();
        UnitIcon.sprite = null;
        SkillIcon.sprite = null;

        active = false;
    }

}
