using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSet : MonoBehaviour {

    public static MapSet instance;

    [Serializable]
    public struct MapSetting
    {
        [SerializeField] public int Number;
        [SerializeField] public string Name;
        [SerializeField] public Material skybox;
        [SerializeField] public Color color;
        [SerializeField] public float lightPower;
        [SerializeField] public float shadowStrength;
    }
    [SerializeField] MapSetting[] Env;

    [SerializeField] public GameObject[] SpawnPoint;
    [SerializeField] public Quaternion[] SpawnRotation;

    [SerializeField] Light light;
    
    static int count=0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        int count = UnityEngine.Random.Range(1, 5);
        SetEnv(count);
    }
    void SetEnv(int index)
    {
        light.intensity = Env[index].lightPower;
        light.shadowStrength = Env[index].shadowStrength;
        light.color = Env[index].color;
        RenderSettings.skybox= Env[index].skybox;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SetEnv((++count)%5);
        }
    }
}
