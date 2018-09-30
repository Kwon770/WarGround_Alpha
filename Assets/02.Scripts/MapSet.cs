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
    

    private void Awake()
    {/*
        int EnvIndex = UnityEngine.Random.Range(1,6);
        light.intensity = Env[EnvIndex].lightPower;
        light.shadowStrength = Env[EnvIndex].shadowStrength;
        light.color = Env[EnvIndex].color;
        RenderSettings.skybox= Env[EnvIndex].skybox;*/
        instance = this;
    }
}
