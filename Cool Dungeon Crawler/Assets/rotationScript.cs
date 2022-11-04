using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 mouse_pos;
    public GameObject attack;
    public GameObject attackLocation;
    public GameObject AttackHolder;
    public Transform target;
    public GameObject object_pos;
    private float angle;
    private float currentAngle;
    public Vector3 targetDestination;

    // Update is called once per frame
    private void Awake()
    {
        AttackHolder = GameObject.FindGameObjectWithTag("attackHolder");
        object_pos = GameObject.FindGameObjectWithTag("attackShooter");
    }
    void Update()
    {
        // mouse_pos = Input.mousePosition;
        // mouse_pos.z=-10f;
        mouse_pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        mouse_pos.x = transform.position.x - mouse_pos.x;
        mouse_pos.y = transform.position.y - mouse_pos.y;

        //mouse_pos.Normalize();

        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x);//*180/Mathf.PI;
        currentAngle = angle * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        
        if (Input.GetMouseButtonDown(0))
        {
            /*
            Vector3 aim= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aim.z=0;
            float angleAim;
            angleAim = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            */

            float x = 2 * Mathf.Cos(currentAngle);
            float y = 2 * Mathf.Sin(currentAngle);
            
            
            targetDestination = new Vector3(x,y, 0);
            
            Shoot(targetDestination, currentAngle);
        }
    }
    private void Shoot(Vector3 destination,float rotation)
    {
        GameObject tempAttack = Instantiate(attack, attackLocation.transform.position, Quaternion.identity,AttackHolder.transform);
        tempAttack.GetComponent<scriptAttackHandler>().SetRotation(rotation);
        tempAttack.GetComponent<scriptAttackHandler>().SetDestination(destination - object_pos.transform.position);

    }
}
