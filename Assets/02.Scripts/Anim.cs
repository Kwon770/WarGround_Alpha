using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : Photon.MonoBehaviour {
    
    public Animator anim;
    
	void Awake ()
    {
        anim = GetComponent<Animator>();
	}
    public void Attack()
    {
        anim.SetTrigger("ATTACK");
    }
    public void Block()
    {
        anim.SetTrigger("BLOCK");
    }
    public void Move()
    {
        anim.SetBool("MOVE", true);
    }
    public void Stop()
    {
        anim.SetBool("MOVE", false);
    }
    public void DIE()
    {
        anim.SetTrigger("DIE");
    }


    //특수 애니메이션

    //닌자 회피 애니메이션
    [PunRPC]
    public void EVASION()
    {
        anim.SetTrigger("EVASION");
    }
}
