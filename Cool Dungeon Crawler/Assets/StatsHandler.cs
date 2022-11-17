using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buff
{
    public float BuffDuration { get; set; }
    public string BuffName { get; set; }
    public float BuffValue { get; set; }
    
}

public class StatsHandler : MonoBehaviour
{
    #region PlayerUsed
    private int maxHealth;
    private int currentHealth;
    private int currentAttack;
    private float attackSpeed;
    private float movementSpeed;
    public int GetCurrentHealth() { return currentHealth; }
    public void SetCurrentHealth(int value) { currentHealth = value; }
    public int GetCurrentAttack() { return currentAttack; }
    public void SetCurrentAttack(int value) { currentAttack = value; }
    public float GetAttackSpeed() { return attackSpeed; }
    public void SetAttackSpeed(float value) { attackSpeed = value; }
    public int GetMaxHealth() { return maxHealth; }
    public void SetMaxHealth(int value) { maxHealth = value; }
    public float GetMovementSpeed() { return movementSpeed; }
    public void SetMovementSpeed(float value) { movementSpeed = value; }
    #endregion

    #region WeaponUsed

    private float projectileSpeed;
    private int projectileCount;
    private string projectileCurrent;
    private string projectilePrevious;
    private float projectileLifetime;

    public float GetProjectileSpeed() { return projectileSpeed; }
    public void SetProjectileSpeed(float value) { projectileSpeed = value; }
    public int GetProjectileCount() { return projectileCount; }
    public void SetProjectileCount(int value) { projectileCount = value; }
    public string GetProjectileCurrent() { return projectileCurrent; }
    public void SetProjectileCurrent(string value) { projectileCurrent = value; }
    public string GetProjectilePrevious() { return projectilePrevious; }
    public void SetProjectilePrevious(string value) { projectilePrevious = value; }
    public float GetProjectileLifetime() { return projectileLifetime; }
    public void SetProjectileLifetime(float value) { projectileLifetime = value; }
    #endregion

    #region Buffs
    public List<Buff> activeBuffs = new List<Buff>();
    private string activeAura;
    public string GetActiveAura() { return activeAura; }
    public void SetActiveAura(string value) { activeAura = value; }


    #endregion



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

        if (!GodEye.GetHasInitialized())
        {
            maxHealth = 1;
            currentHealth = 1;
            movementSpeed = 2;
            currentAttack = 1;
            attackSpeed = 1;

            projectileSpeed = 1;
            projectileCount = 1;
            projectileCurrent = GodEye.GetWeaponCurrentActive();
            projectileLifetime = 1;

            activeAura = GodEye.GetAuraCurrentActive();

            antiMaxHealth = maxHealth;
            antiHealth = currentHealth;
            antiDamage = currentAttack;
            healthLabel.GetComponent<TextMeshProUGUI>().text = currentHealth.ToString();
            damageLabel.GetComponent<TextMeshProUGUI>().text = currentAttack.ToString();

        }
        else
        {
            maxHealth = GodEye.GetPlayerHPMax();
            currentHealth = GodEye.GetPlayerHPCurrent();
            movementSpeed = GodEye.GetPlayerSpeed();
            currentAttack = GodEye.GetPlayerAttack();
            attackSpeed = GodEye.GetPlayerAttackSpeed();

            projectileSpeed = GodEye.GetWeaponSpeed();
            projectileCount = GodEye.GetWeaponCount();
            projectileCurrent = GodEye.GetWeaponCurrentActive();
            projectileLifetime = GodEye.GetWeaponLifetime();

            activeAura = GodEye.GetAuraCurrentActive();

        }
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
            currentAttack+=1;
            UpdateLabels(orb,currentAttack);
            
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
