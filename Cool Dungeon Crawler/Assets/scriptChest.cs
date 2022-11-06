using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptChest : MonoBehaviour
{

    [SerializeField] Sprite chestOpened;
    [SerializeField] GameObject[] dropPool;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StatsHandler temporal = other.GetComponent<StatsHandler>();
        if (temporal != null)
        {

            SpawnItem();

        }
    }

    private void SpawnItem() 
    { 


    
    }
}
