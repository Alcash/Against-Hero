using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : WeaponController {

    public GameObject shell;
    public Transform shellSocket;


    protected override void InitWeapon()
    {
        
    }

    public override void StartAttack()
    {
        //base.StartAttack();

        var instanceShell = Instantiate(shell, shellSocket.position, Quaternion.identity);

        ShellController shellController = instanceShell.GetComponent<ShellController>();
        
        shellController.HitColliderEvent += ContactCollider;

    }

    protected override void ContactCollider(GameObject gameobject)
    {
        HitCollider(gameobject);
    }
    

}
