using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeSpawner : MonoBehaviour
{

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StatsHandler temporal = other.GetComponent<StatsHandler>();
        if (temporal != null)
        {
            
        }


    }




}




