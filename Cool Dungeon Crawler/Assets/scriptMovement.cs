using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptMovement : MonoBehaviour
{
    
    public float moveSpeed=5f;
    Vector2 movement;
    

    public Rigidbody2D playerRigid2d;

    // Update is called once per frame
    
    void  Update() {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");

        

    }
    void FixedUpdate()
    {
         

       
        playerRigid2d.MovePosition(playerRigid2d.position+movement*moveSpeed*Time.fixedDeltaTime);


    }

    

    
 
}
