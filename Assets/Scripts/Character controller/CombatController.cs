using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    protected Animator animator;
    protected SkeletHolder skeletHolder;

    protected float attackColddown = 0;
    protected bool canAttack = false;

    // Use this for initialization
    protected virtual void Start () {
        animator = GetComponentInChildren<Animator>();

        skeletHolder = GetComponentInChildren<SkeletHolder>();

        skeletHolder.StartAttackEvent += DealAttack;
        skeletHolder.EndAttackEvent += EndDealAttack;
    }

    protected virtual void CooldownTimer()
    {
        if (attackColddown > 0)
        {
            attackColddown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
    }

    public virtual void DealAttack()
    {
        
    }

    public virtual void EndDealAttack()
    {
       
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
