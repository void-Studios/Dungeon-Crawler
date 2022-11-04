using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptSceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
   public IEnumerator LoadLevel(int levelIndex)
    {

        //transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(levelIndex);
        Time.timeScale=1f;
    }
}
