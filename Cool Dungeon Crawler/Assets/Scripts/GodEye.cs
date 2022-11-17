using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class GodEye
{
    #region variables
    private static bool hasInitialized;
    //Global/Multipurpuse/Dungeon variables
    private static int worldDifficulty;
    private static List<Trail> trailInfo = new List<Trail>();



    //Player variables for Player spawning 
    private static float playerSpeed;
    private static int playerHPMax;
    private static int playerHPCurrent;
    private static int playerAttack;
    private static float playerAttackSpeed;

    //Weapon variables for current Weapon
    private static float weaponSpeed;
    private static int weaponCount;
    private static string weaponCurrentActive;
    private static string weaponPreviousActive;
    private static float weaponLifetime;

    //Aura variables for Current Aura
    private static string auraCurrentActive;

    //Pickup Variables for pickup buffs
    private static string pickupBuff;
    private static float pickupLifetime;

    #endregion

    #region GlobalMethods
    public static bool GetHasInitialized() { return hasInitialized; }
    public static void SetHasInitialized(bool value) { hasInitialized = value; }
    
    public static int GetWorldDifficulty() { return worldDifficulty; }
    public static void SetWorldDifficulty(int value) { worldDifficulty += value; }

    public static List<Trail> GetTrailInfo() { return trailInfo; }
    public static void SetTrailInfo(int posX, int posY, int[] currentBounding) 
    {
        trailInfo.Add(new Trail { XPos = posX, YPos = posY, BoundingBox = currentBounding });
    }

    #endregion

    #region PlayerMethods
    public static float GetPlayerSpeed(){return playerSpeed;}
    public static void SetPlayerSpeed(float value){ playerSpeed = value; }

    public static int GetPlayerHPMax(){return playerHPMax;}
    public static void SetPlayerHPMax(int value){playerHPMax = value;}

    public static int GetPlayerHPCurrent(){return playerHPCurrent;}
    public static void SetPlayerHPCurrent(int value){playerHPCurrent = value;}

    public static int GetPlayerAttack(){return playerAttack;}
    public static void SetPlayerAttack(int value){playerAttack = value;}

    public static float GetPlayerAttackSpeed(){return playerAttackSpeed;}
    public static void SetPlayerAttackSpeed(float value){playerAttackSpeed = value;}
    #endregion

    #region WeaponMethods
    public static float GetWeaponSpeed(){return weaponSpeed;}
    public static void SetWeaponSpeed(float value){weaponSpeed = value;}

    public static int GetWeaponCount(){return weaponCount;}
    public static void SetWeaponCount(int value){weaponCount = value;}

    public static string GetWeaponPreviousActive() { return weaponPreviousActive; }
    public static string GetWeaponCurrentActive(){return weaponCurrentActive;}
    public static void SetWeaponCurrentActive(string value)
    {
        weaponPreviousActive = weaponCurrentActive;
        weaponCurrentActive = value;
    }

    public static float GetWeaponLifetime(){return weaponLifetime;}
    public static void SetWeaponLifetime(float value){weaponLifetime = value;}
    #endregion

    # region AuraMethods
    public static string GetAuraCurrentActive(){return auraCurrentActive;}
    public static void SetAuraCurrentActive(string value){auraCurrentActive = value;}

    //Pickup Variables for pickup buffs
    public static void SetPickupBuff(string buff,float lifetime)
    {
        pickupBuff = buff;
        pickupLifetime = lifetime;
    }
    #endregion








}
