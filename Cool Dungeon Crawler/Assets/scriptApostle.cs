using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptApostle : MonoBehaviour
{
    #region CurrentActiveReferences
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject SceneLoader;
    private StatsHandler playerStats;
    
    public int SceneDestination;
    
    public void SetPlayerObject(GameObject value) 
    { 
        player = value; 
        playerStats = player.GetComponent<StatsHandler>();
    }


    #endregion

    public bool SceneEnterInitiated() 
    { 




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





}
