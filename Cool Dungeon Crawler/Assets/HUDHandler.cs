using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour
{
    [SerializeField] private Texture2D heartFull;
    [SerializeField] private Texture2D heartEmpty;
    [SerializeField] private GameObject heartMini;
    [SerializeField] private GameObject heartMain;
    [SerializeField] private GameObject heartContainer;

    [SerializeField] private GameObject weaponCurrent;

    private GameObject Apostle;
    private scriptApostle scriptApostle;

    private void Awake()
    {
        Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;
        scriptApostle = Apostle.GetComponent<scriptApostle>();

    }

    public bool UIUpdate()
    {
        heartMain.GetComponent<Image>().fillAmount = GodEye.GetPlayerHPCurrent();
        for (int i = 1; i < GodEye.GetPlayerHPMax(); i++)
        {
            _ = Instantiate(heartMini, heartContainer.transform);
        }

        for (int i = 0; i < scriptApostle.WeaponList.Length; i++)
        {
            if (scriptApostle.WeaponList[i].WeaponName == GodEye.GetWeaponCurrentActive().WeaponName)
            {
                weaponCurrent.GetComponent<Image>().sprite = scriptApostle.WeaponList[i].WeaponObject.GetComponent<scriptAttackHandler>().transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }


      


        return true;
    }










}
