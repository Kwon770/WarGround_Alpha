using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

    ParticleSystem particleSys;
    ParticleSystemRenderer particle;

    [SerializeField] Material Demage;
    [SerializeField] Material Heal;
    [SerializeField] Material Dead;
    [SerializeField] Material Gaurd;


    void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        particle = GetComponent<ParticleSystemRenderer>();
    }

    public void DemagePlay()
    {
        particle.material = Demage;
        particleSys.Play();
    }
    public void HealPlay()
    {
        particle.material = Heal;
        particleSys.Play();
    }
    public void DeadPlay()
    {
        particle.material = Dead;
        particleSys.Play();
    }
    public void GaurdPlay()
    {
        particle.material = Gaurd;
        particleSys.Play();
    }
}
