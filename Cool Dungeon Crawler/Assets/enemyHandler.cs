using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth { get; private set; }
    public int currentDamage { get; private set; }
    public int maxHealth { get; private set; }

    public Stat damage;
    public Stat health;

    void OnEnable()
    {
        maxHealth = 1;
        currentHealth = 1;
        currentDamage = 1;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Upgrade(string orb)
    {
        if (orb == "health")
        {
            maxHealth += 1;
            currentHealth = maxHealth;
        }
        else if (orb == "damage")
        {
            currentDamage += 1;
        }
    }

    public virtual void Die()
    {
        //Dieded by death
    }

}
