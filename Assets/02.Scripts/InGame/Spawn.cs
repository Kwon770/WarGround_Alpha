using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : Photon.MonoBehaviour {

    public static Spawn spawn;
    public UnitInfo self;

    [SerializeField] string[] Units;
    [SerializeField] int[] Cost;
    [SerializeField] Sprite[] Icon;

    public string unitName;
    public int cost;

    private void Awake()
    {
        self = GetComponent<UnitInfo>();
    }

    public void Setting()
    {
        spawn = this;
        for(int i = 5 - Units.Length; i < 5; i++)
        {
            Button temp;
            temp = GameManager.manager.SpawnButton.GetChild(i).GetComponent<Button>();
            GameManager.manager.SpawnButton.GetChild(i).gameObject.SetActive(true);
            temp.transform.GetChild(1).GetComponent<Text>().text = Cost[i - 5 + Units.Length].ToString();
            GameManager.manager.SpawnButton.GetChild(i).GetComponent<Image>().sprite = Icon[i - 5 + Units.Length];

            temp.onClick.RemoveAllListeners();
            var cachedI = i- 5 + Units.Length; // Cache for Lambda
            temp.onClick.AddListener(new UnityEngine.Events.UnityAction(() => spawn.SetName(Units[cachedI])));
            temp.onClick.AddListener(new UnityEngine.Events.UnityAction(() => spawn.SetCost(Cost[cachedI])));
            temp.onClick.AddListener(new UnityEngine.Events.UnityAction(() => GameManager.manager.SetTrigger("Spawn")));
            temp.onClick.AddListener(new UnityEngine.Events.UnityAction(() => SoundManager.soundmanager.clickIngameButton()));
        }
    }

    public void UnitSpawn(TileInfo tile)
    {
        if (self.Act <= 0 || GameData.data.bitinium < cost) return;

        if (Calculator.Calc.Range(tile, GameData.data.FindTile(self.x, self.y), 1) == -1) return;

        Vector3 spawnPos=tile.transform.position;
        UnitInfo unit = PhotonNetwork.Instantiate(unitName, spawnPos, transform.rotation, 0).GetComponent<UnitInfo>();

        GameData.data.SetBitinium(-cost);

        self.Act--;

        unit.SetOwner(PhotonNetwork.playerName);
        unit.x = tile.x;
        unit.y = tile.y;

        if (self.Kinds == "Mars")
        {
            unit.SHD += 1;
        }
    }

    public void SetName(string name)
    {
        Debug.Log(name);
        unitName = name;
    }
    public void SetCost(int cost)
    {
        Debug.Log(cost);
        this.cost = cost;
    }
}
