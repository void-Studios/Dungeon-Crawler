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
    #region GlobalUsed
    private bool hasInitialized;
    
    public bool GetHasInitialized() { return hasInitialized; }
    /*
    public void SetHasInitiliazed(bool value) 
    {
        hasInitialized = value;
    }*/
    #endregion

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
    private WeaponListEntry projectileCurrent;
    private WeaponListEntry projectilePrevious;
    private float projectileLifetime;

    public float GetProjectileSpeed() { return projectileSpeed; }
    public void SetProjectileSpeed(float value) { projectileSpeed = value; }
    public int GetProjectileCount() { return projectileCount; }
    public void SetProjectileCount(int value) { projectileCount = value; }
    public WeaponListEntry GetProjectileCurrent() { return projectileCurrent; }
    public void SetProjectileCurrent(WeaponListEntry value) { projectileCurrent = value; }
    public WeaponListEntry GetProjectilePrevious() { return projectilePrevious; }
    public void SetProjectilePrevious(WeaponListEntry value) { projectilePrevious = value; }
    public float GetProjectileLifetime() { return projectileLifetime; }
    public void SetProjectileLifetime(float value) { projectileLifetime = value; }
    #endregion

    #region Buffs
    public List<Buff> activeBuffs = new List<Buff>();
    private string activeAura;
    public string GetActiveAura() { return activeAura; }
    public void SetActiveAura(string value) { activeAura = value; }


    #endregion

    [SerializeField] private GameObject Apostle;
    private scriptApostle scriptApostle;

    public Stat damage;
    public Stat health;

    public int antiMaxHealth { get; private set; }
    public int antiDamage;
    public int antiHealth;

    public GameObject Fing;



    void Start() 
    {

        //release plug for BetterChanges
        //Initiate();
        #region Garbarge
        Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;
        scriptApostle = Apostle.GetComponent<scriptApostle>();
        if (!GodEye.GetHasInitialized())
        {
            //Default Values, could be optimized based on Player Selection
            DefaultPlayer();
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
        else //Has Initialized therefore load values
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
            scriptApostle.UIUpdate();
        }
        #endregion
    }
    public void Initiate()
    {
        Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;
        scriptApostle = Apostle.GetComponent<scriptApostle>();

        if (!GodEye.GetHasInitialized())
        {
            BetterDefault();
        }

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
        scriptApostle.UIUpdate();


        hasInitialized = true;
    }
    public bool BetterDefault()
    {
        int v = Random.Range(0, scriptApostle.WeaponList.Length);
        int tempRandom = v;
        GodEye.SetWeaponCurrentActive(scriptApostle.WeaponList[tempRandom]);
        scriptApostle.SetPlayerObject(this.gameObject);
        scriptApostle.SetWeaponCurrent();

        return true;
    }

    public bool DefaultPlayer()
    {
        if (Apostle==null)
        {
            Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;
            scriptApostle = Apostle.GetComponent<scriptApostle>();
        }

        GodEye.SetPlayerHPMax(1);
        GodEye.SetPlayerHPCurrent(1);
        GodEye.SetPlayerSpeed(2);
        GodEye.SetPlayerAttack(1);
        GodEye.SetPlayerAttackSpeed(1);
        int v = Random.Range(0, scriptApostle.WeaponList.Length);
        int tempRandom = v;
        GodEye.SetWeaponCurrentActive(scriptApostle.WeaponList[tempRandom]);
        GodEye.SetWeaponSpeed(1);
        GodEye.SetWeaponCount(1);
        GodEye.SetWeaponLifetime(1);
        GodEye.SetAuraCurrentActive("null");

        scriptApostle.SetPlayerObject(this.gameObject);
        scriptApostle.UIUpdate();
        scriptApostle.SetWeaponCurrent();

        return true;
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

        }
        else if(orb == "damage")
        {
            currentAttack+=1;
            
        }





    }
    
    public virtual void Die() 
    {
        //Dieded by death
    }
}
