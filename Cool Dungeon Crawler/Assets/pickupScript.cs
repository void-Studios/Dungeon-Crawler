using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour
{
    public string boostName;
    private GameObject gameHandler;
    public List<GameObject> temp = new List<GameObject>();

    private void Start()
    {
        gameHandler = GameObject.FindObjectOfType<scriptApostle>().gameObject;
        temp.AddRange(gameHandler.GetComponent<scriptLevelGenerator>().entityList);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        StatsHandler temporal = other.GetComponent<StatsHandler>();
        if (temporal!=null)
        {
            temporal.Upgrade(boostName);
            for(int i = 0; i < temp.Count;i++)
            {
                Destroy(temp[i]);
            }
            gameHandler.GetComponent<scriptLevelGenerator>().entityList.Clear();
            gameHandler.GetComponent<scriptLevelGenerator>().Defaulting();
            gameHandler.GetComponent<scriptLevelGenerator>().StartGame();
        }
            
    }
}
