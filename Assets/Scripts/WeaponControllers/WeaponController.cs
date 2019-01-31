using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WeaponController : MonoBehaviour {

    public float m_DamageScale = 1;
    
    public UnityAction<GameObject> HitCollider;
    // Use this for initialization
    void Start () {

        InitWeapon();
    }
	
    protected virtual void InitWeapon()
    {
       
    }

	public virtual void StartAttack()
    {
      
    }

    public virtual void EndAttack()
    {
        
    }

    protected virtual void ContactCollider(GameObject _gameobject)
    {
        if (HitCollider != null)
        {
            HitCollider(_gameobject);
        }
       
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
        ContactCollider(other.gameObject);
    }
    
}
