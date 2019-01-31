using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponController {
    BoxCollider weaponCollider;

    

    protected override void InitWeapon()
    {
        base.InitWeapon();
        weaponCollider = GetComponent<BoxCollider>();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        
        weaponCollider.enabled = true;
    }

    public override void EndAttack()
    {
        base.EndAttack();
        weaponCollider.enabled = false;
    }

    
}
