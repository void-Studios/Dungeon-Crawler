using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 mouse_pos;
    public GameObject attack;
    public GameObject attackLocation;
    public Vector3 attackLocationPosition;
    public GameObject AttackHolder;
    public Transform target;
    public Vector3 targetPosition;
    public GameObject object_pos;
    private float angle;
    private float currentAngle;
    public Vector3 targetDestination;
    public Vector3 shootDirection;

    private bool isAttackCooldown; 


    [SerializeField] private GameObject Apostle;

    private scriptApostle scriptApostle;
    private void Start()
    {
        Apostle = FindObjectOfType<scriptApostle>().transform.gameObject;
        scriptApostle = Apostle.GetComponent<scriptApostle>();

        AttackHolder = GameObject.FindGameObjectWithTag("attackHolder");
        object_pos = GameObject.FindGameObjectWithTag("attackShooter");

        isAttackCooldown = false;
    }
    void FixedUpdate()
    {
        attackLocationPosition = object_pos.transform.position;
        targetPosition = target.position;
        mouse_pos =Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_pos.x = transform.position.x - mouse_pos.x;
        mouse_pos.y = transform.position.y - mouse_pos.y;

        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x);//*180/Mathf.PI;
        currentAngle = angle * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        if (Input.GetMouseButton(0))
        {
            if (!isAttackCooldown)
            {
                float x = 2 * Mathf.Cos(currentAngle);
                float y = 2 * Mathf.Sin(currentAngle);
                targetDestination = new Vector3(x, y, 0);

                StartCoroutine(Shoot(targetDestination, currentAngle));
            }
        }
    }


    public IEnumerator Shoot(Vector3 destination,float rotation)
    {
        isAttackCooldown = true;
        
        attack = GodEye.GetWeaponCurrentActive().WeaponObject;
        GameObject tempAttack = Instantiate(attack, attackLocation.transform.position, Quaternion.identity,AttackHolder.transform);
        tempAttack.GetComponent<scriptAttackHandler>().SetRotation(rotation);
        tempAttack.GetComponent<scriptAttackHandler>().SetDestination(destination - object_pos.transform.position);
        yield return new WaitForSeconds(GodEye.GetPlayerAttackSpeed());
        isAttackCooldown = false;
    }
}
