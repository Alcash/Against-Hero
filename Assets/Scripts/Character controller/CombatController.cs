using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    protected Animator animator;

    // Use this for initialization
    protected virtual void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	

    public virtual void Attack()
    {

    }

    public enum WeaponType
    {
        Unarmed,
        Sword,
        Wand,
        OffHand
    }
}
