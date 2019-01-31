using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCore : MonoBehaviour, IDamagable {

    public string slugName;

    public int damageMin, damageMax;
    public float attackSpeed;

    public float movementSpeed;

    public int armor;
    private int healthCur, healthMax, healthMin = 0;

    public int CurrentHealth { get; protected set; }
    public int MaxHealth;

    //public EffectCore Resist;

    public float ResistEffect { get; set; }

    private bool isDead;
    public UnityAction DeathEvent;

    // Use this for initialization
    void Start() {

        healthMax = MaxHealth;

        healthCur = healthMax;



    }

    /// <summary>
    /// Recieve action eg damage fire, pierce or health
    /// </summary>
    /// <param name="action"></param>
    public EffectCore RecieveEffect(EffectCore effectCore)
    {
        EffectCore reciveEffect = new EffectCore();
        reciveEffect.damage = Mathf.Max(0, effectCore.damage - armor);
        /*effect = action.life - (int)(action.life * Resist.life);
          effect = + action.death - (int)(action.death * Resist.death);
          effect = + action.fire - (int)(action.fire * Resist.fire);
          effect = + action.electricity - (int)(action.electricity * Resist.electricity);
          effect = + action.cold - (int)(action.cold * Resist.cold);
          effect = + action.cutting - (int)(action.cutting * Resist.cutting);
          effect = + action.crushing - (int)(action.crushing * Resist.crushing);
          effect = + action.piercing - (int)(action.piercing * Resist.piercing);
          */
        // healthCur -= (int)effect - (int)(effect * ResistEffect);

        healthCur -= reciveEffect.damage;

        if (healthCur < healthMin)
        {
            isDead = true;
            OnDied();
        }

       
        Debug.Log(slugName + " recieve " + effectCore.damage + " apply " + reciveEffect.damage + ". Health " + healthCur);

        return reciveEffect;
    }
    

    public float GetPercentageOfHealth()
    {
        return (float)healthCur / (float)healthMax;
    }
    
    public int GetDamage()
    {
        return damageMin + (int)(((float)(damageMax - damageMin)) * UnityEngine.Random.value);
    }

    void OnDied()
    {
        if(DeathEvent != null)
        {
            DeathEvent();
        }
    }

}
