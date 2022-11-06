using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptMovement : MonoBehaviour
{
    
    public float moveSpeed=5f;
    Vector2 movement;
    public PolygonCollider2D Collider2d;
    public Rigidbody2D Rigid2d;


   

    // Update is called once per frame
    private void Awake()
    {
        Collider2d = this.gameObject.transform.GetChild(1).GetComponent<PolygonCollider2D>();
        Rigid2d = this.gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>();
    }
    void  Update() {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");

        

    }
    void FixedUpdate()
    {
         

       
        Rigid2d.MovePosition(Rigid2d.position+movement*moveSpeed*Time.fixedDeltaTime);


    }

    

    
 
}
