using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;

    public int currentHealth { get; private set; }

    public UnityAction<int, int> OnHealthChanged;

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    protected void Update()
    {
        CharacterUpdateMethod();
    }

    protected virtual void CharacterUpdateMethod()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        Debug.Log(name + " takes " + damage + " damage");

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

   public virtual void Die()
    {
        Debug.Log(name + " died");
        Destroy(gameObject);
    }
}
