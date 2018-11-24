using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : Photon.MonoBehaviour {

    public static Spawn spawn;
    public UnitInfo self;

    [SerializeField] string[] Units;
    [SerializeField] int[] Cost;

    public string unitName;
    public int cost;

    private void Awake()
    {
        self = GetComponent<UnitInfo>();
    }

    public void Setting()
    {
        spawn = this;
        for(int i = 0; i < Units.Length; i++)
        {
            Debug.Log(Units[i] + " " + i);
            Button temp;
            temp = GameManager.manager.SpawnButton.GetChild(i).GetComponent<Button>();

            temp.onClick.RemoveAllListeners();
            var cachedI = i; // Cache for Lambda
            temp.onClick.AddListener(new UnityEngine.Events.UnityAction(() => spawn.SetName(Units[cachedI])));
            temp.onClick.AddListener(new UnityEngine.Events.UnityAction(() => GameManager.manager.SetTrigger("Spawn")));
            cost = Cost[i];
        }
    }

    public void UnitSpawn(TileInfo tile)
    {
        if (Calculator.Calc.Range(tile, GameData.data.FindTile(self.x, self.y), 1) == -1) return;
        if (cost > GameData.data.bitinium && GameData.data.LeaderShip >= GameData.data.MaxLeaderShip) return;

        Vector3 spawnPos=tile.transform.position;
        UnitInfo unit = PhotonNetwork.Instantiate(unitName, spawnPos, transform.rotation, 0).GetComponent<UnitInfo>();

        self.Act--;
        GameData.data.LeaderShip++;
        GameData.data.bitinium -= cost;
        InfoBar.bar.SetLeadership();
        InfoBar.bar.SetBit();

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
}
