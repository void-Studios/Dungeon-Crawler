using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptAttackHandler : MonoBehaviour
{
    private bool isDirectionSet;
    private Vector2 destination;
    public PolygonCollider2D attackCollider;
    public Rigidbody2D attackRigid2d;
    public float projectileSpeed;
    public Vector3 object_pos;



    public Vector3 SetDestination(Vector3 destination) {

        this.destination = destination;
        isDirectionSet = true;
        return destination;
    }
    public void SetRotation(float rotation)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    public Vector3 GetDestination() { return destination; }
    public Quaternion GetRotation() { return this.transform.rotation; }

    private void Awake()
    {

        attackCollider = this.gameObject.transform.GetChild(0).GetComponent<PolygonCollider2D>();
        attackRigid2d = this.gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
        isDirectionSet = false;

    }




    // Physics Update is called once per frame
    void FixedUpdate()
    {
        if (isDirectionSet)
        {
            //object_pos = transform.position;
            


            attackRigid2d.MovePosition(attackRigid2d.position + destination.normalized * projectileSpeed * Time.fixedDeltaTime);



        }
    }
}
