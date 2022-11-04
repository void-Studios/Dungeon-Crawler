using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptDoorStatusExit : MonoBehaviour
{
    public GameObject sceneLoader;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(sceneLoader.GetComponent<scriptSceneLoader>().LoadLevel(1));

        }
    }
}
