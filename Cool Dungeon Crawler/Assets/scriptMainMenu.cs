using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scriptMainMenu : MonoBehaviour
{
    public GameObject sceneLoader;
    public bool isAboutOpened;
    public GameObject aboutPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isAboutOpened)
            {
                isAboutOpened = false;
                aboutPanel.SetActive(false);
            }
            else
            {
                isAboutOpened = true;
                aboutPanel.SetActive(true);
            }
        }
    }

    public void menuStart()
    {
        StartCoroutine(sceneLoader.GetComponent<scriptSceneLoader>().LoadLevel(1));

    }

    public void menuAbout()
    {
        if (isAboutOpened)
        {
            isAboutOpened = false;
            aboutPanel.SetActive(false);
        }
        else
        {
            isAboutOpened = true;
            aboutPanel.SetActive(true); 
        }
    }

    public void menuExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
        #endif
        Application.Quit();
    }


    
}
