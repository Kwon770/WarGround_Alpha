using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : Photon.MonoBehaviour {

    public static Spawn spawn;

    [SerializeField] string[] Units;

    public string unitName;

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
        }
    }

    public void UnitSpawn(TileInfo tile)
    {
        Vector3 spawnPos=tile.transform.position;
        GameObject unit = PhotonNetwork.Instantiate(unitName, spawnPos, transform.rotation, 0);

        unit.GetComponent<UnitInfo>().SetOwner(PhotonNetwork.playerName);
        unit.GetComponent<UnitInfo>().x = tile.x;
        unit.GetComponent<UnitInfo>().y = tile.y;
    }
    
    public void SetName(string name)
    {
        Debug.Log(name);
        unitName = name;
    }
}
