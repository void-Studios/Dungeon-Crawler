using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamera : MonoBehaviour
{
    public GameObject playerObject;
    public Transform playerRoot;


    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRoot = playerObject.transform.GetChild(1);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerRoot.position.x, playerRoot.position.y,-10);
    }
}
