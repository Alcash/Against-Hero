using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombatController : CombatController {

    CharacterCore core;

    
    float attackColddown = 0;
    bool canAttack = false;
    SkeletHolder skeletHolder;
    WeaponType weaponType = WeaponType.Sword;

    WeaponController weaponController;


    List<string> weaponList = new  List<string>(){ "Unarmed","Sword","Wand","Offhand"};

    // Use this for initialization
    protected override void Start () {

        base.Start();
        core = GetComponent<CharacterCore>();
        skeletHolder = GetComponentInChildren<SkeletHolder>();

        skeletHolder.StartAttackEvent += DealAttack;
        skeletHolder.EndAttackEvent += EndDealAttack;

        InitWeapon();
        
    }
	
	// Update is called once per frame
	void Update () {

        if(attackColddown > 0 )
        {
            attackColddown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
            
        }

        
    }

    void DealAttack()
    {
        weaponController.StartAttack();
    }

    void EndDealAttack()
    {
        weaponController.EndAttack();
    }

    public override void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            
            animator.SetTrigger("AttackTrigger");

            attackColddown = 1/core.attackSpeed;

        }
    }

    void HitCollider(GameObject _gameobject)
    {
        
        var damagable = _gameobject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            EffectCore effect = new EffectCore();
            effect.damage = (int)(core.GetDamage() * weaponController.m_DamageScale);

            var recievedDamage = damagable.RecieveEffect(effect);
        }
    }

    void InitWeapon()
    {
        if(weaponController != null)
        {
            
            if (weaponController.HitCollider!= null)
                weaponController.HitCollider -= HitCollider;

            Destroy(weaponController.gameObject);

            Destroy(weaponController.gameObject);

            weaponController = null;
        }
        var weapon = Instantiate(Resources.Load<GameObject>("Weapons/" + weaponList[(int)weaponType]), skeletHolder.m_RightHandSocket);
        weaponController = weapon.GetComponent<WeaponController>();
        weaponController.HitCollider += HitCollider;
        animator.SetInteger("Weapon", (int)weaponType);
    }
}
