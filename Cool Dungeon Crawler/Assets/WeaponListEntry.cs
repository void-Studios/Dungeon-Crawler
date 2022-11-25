using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponListEntry
{
    public WeaponListEntry(string weaponName, GameObject weaponObject)
    {
        WeaponName = weaponName;
        WeaponObject = weaponObject;
    }

    public string WeaponName { get; set; }
    public GameObject WeaponObject { get; set; }
}
