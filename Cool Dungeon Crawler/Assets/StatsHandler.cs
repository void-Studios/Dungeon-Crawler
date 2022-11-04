using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsHandler : MonoBehaviour
{
    
    public int currentHealth{get; private set;}
    public int currentDamage{get; private set;}
    public int maxHealth{get; private set;}

    public Stat damage;
    public Stat health;


    public int antiMaxHealth { get; private set; }
    public int antiDamage;
    public int antiHealth;

    public GameObject healthLabel;
    public GameObject damageLabel;
    public GameObject Fing;



    void Awake() 
    {

        healthLabel = GameObject.Find("Canvas/CurrentHealth/healthVar");
        damageLabel = GameObject.Find("Canvas/CurrentDamage/damageVar");

        maxHealth =1;
        currentHealth=1; 
        currentDamage=1;

        antiMaxHealth = maxHealth;
        antiHealth = currentHealth;
        antiDamage = currentDamage;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth<=0)
        {
            Die();
        }
    }

    public void Upgrade(string orb)
    {
        if (orb == "health")
        {
            maxHealth+=1;
            currentHealth=maxHealth;
            UpdateLabels(orb,currentHealth);

        }
        else if(orb == "damage")
        {
            currentDamage+=1;
            UpdateLabels(orb,currentDamage);
            
        }
    }
    public void UpdateLabels(string obj, int value)
    {
        if (obj == "health")
        {
            healthLabel.GetComponent<TextMeshProUGUI>().text=value.ToString();
        }
        else if (obj == "damage")
        {
            damageLabel.GetComponent<TextMeshProUGUI>().text=value.ToString();
        }

    }
    public virtual void Die() 
    {
        //Dieded by death
    }
}
