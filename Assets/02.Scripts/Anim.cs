using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

    [SerializeField] float delay;

    Animator anim;
    
	void Awake ()
    {
        anim = GetComponent<Animator>();
	}

    /*public IEnumerator Attack(GameObject Target)
    {
        Anim target=Target.GetComponent<Anim>();
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(delay);
        target.Block();
    }*/
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
}
