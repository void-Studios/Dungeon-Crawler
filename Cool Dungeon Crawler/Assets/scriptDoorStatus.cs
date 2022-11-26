using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scriptDoorStatus : MonoBehaviour
{
    private GameObject Apostle;
    void Awake()
    {
        Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Apostle.GetComponent<scriptApostle>().SceneExitRequested();
        }
    }
}
