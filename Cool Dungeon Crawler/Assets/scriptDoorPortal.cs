using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scriptDoorPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sceneLoader;
    public GameObject statusObject;
    private void Awake()
    {
        statusObject = transform.Find("spriteStatus").gameObject;
    }


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        StatsHandler temporal = other.GetComponent<StatsHandler>();
        if (temporal != null)
        {

            statusObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StatsHandler temporal = other.GetComponent<StatsHandler>();
        if (temporal != null)
        {

            statusObject.SetActive(false);

        }
    }
}
