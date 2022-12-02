using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptApostle : MonoBehaviour
{
    #region CurrentActiveReferences
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject SceneLoader;
    [SerializeField] private GameObject HUD;

    [SerializeField] public WeaponListEntry[] WeaponList;
    [SerializeField] private string[] WeaponListString;
    [SerializeField] private GameObject[] WeaponListObject;
    [SerializeField] private GameObject GameController;
    private GameObject EnemyHolder;
    private int EnemyHolderCount;
    private bool isLevelCompleted;


    private GameObject WeaponCurrent;

    private StatsHandler playerStats;

    public int SceneDestination;


    private void Awake()
    {
        HUD = FindObjectOfType<HUDHandler>().transform.gameObject;
        WeaponList = new WeaponListEntry[WeaponListObject.Length];

        for (int i = 0; i < WeaponListObject.Length; i++)
        {
            WeaponList[i] = new WeaponListEntry(WeaponListString[i], WeaponListObject[i]); ;
        }
        EnemyHolder = GameObject.FindGameObjectWithTag("enemyHolder");
        EnemyHolderCount = EnemyHolder.transform.childCount;

    }

    public GameObject GetWeaponCurrent() { return WeaponCurrent; }
    public void SetWeaponCurrent()
    {
        for (int i = 0; i < WeaponList.Length - 1; i++)
        {
            if (WeaponList[i].WeaponName == GodEye.GetWeaponCurrentActive().WeaponName)
            {
                WeaponCurrent= WeaponList[i].WeaponObject;
                GodEye.SetWeaponCurrentActive(WeaponList[i]);
            }
        }
    }

    public GameObject GetPlayerObject() { return player; }
    public StatsHandler GetPlayerStats() { return playerStats; }
    public void SetPlayerObject(GameObject value) 
    { 
        player = value; 
        playerStats = player.GetComponent<StatsHandler>();
    }

    public bool GetIsLevelCompleted() { return isLevelCompleted; }
    public void SetIsLevelCompleted(bool value) { isLevelCompleted = value; }

    #endregion

    public bool SceneEnterInitiated() 
    {
        //controller activate and then activate the rest from there
        //this ensures that the apostle will load first and then levelgen
        //then the rest of the information that may be required. 





        return true; 
    }
    public bool SceneExitRequested() 
    {
        GodEye.SetPlayerHPMax(playerStats.GetMaxHealth());
        GodEye.SetPlayerHPCurrent(playerStats.GetCurrentHealth());
        GodEye.SetPlayerSpeed(playerStats.GetMovementSpeed());
        GodEye.SetPlayerAttack(playerStats.GetCurrentAttack());
        GodEye.SetPlayerAttackSpeed(playerStats.GetAttackSpeed());
        GodEye.SetWeaponSpeed(playerStats.GetProjectileSpeed());
        GodEye.SetWeaponCount(playerStats.GetProjectileCount());
        GodEye.SetWeaponLifetime(playerStats.GetProjectileLifetime());
        GodEye.SetAuraCurrentActive(playerStats.GetActiveAura());
        Debug.Log("GodEye Has been updated by Apostle");
        StartCoroutine(SceneLoader.GetComponent<scriptSceneLoader>().LoadLevel(SceneDestination));
        return true; 
    }

    public bool UIUpdate()
    {
        HUD.GetComponent<HUDHandler>().UIUpdate();
        return true;
    }

    public void UpdateEnemyDeath()
    {
        StartCoroutine(UpdateEnemyDeathDelay());
    }

    private IEnumerator UpdateEnemyDeathDelay()
    {
        yield return new WaitForSeconds(1f);
        EnemyHolderCount = EnemyHolder.transform.childCount;
        if (EnemyHolderCount <= 0)
        {
            isLevelCompleted = true;
            GameController.SetActive(true);
        }
    }



}
